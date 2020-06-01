namespace RMS.Services
{
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;
    using RMS.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading.Tasks;
    using RMS.API.Models.RequestModels;
    using RMS.API.Models.ResponseModels;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using RMS.API.Models.Helpers;
    using RMS.Services.Strategies.EventFilterStrategies;

    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;
        private readonly IEventFilterFactory eventFilterFactory;

        public EventService(IEventRepository eventRepository, IMapper mapper, IEventFilterFactory eventFilterFactory)
        {
            this.eventRepository = eventRepository;
            this.mapper = mapper;
            this.eventFilterFactory = eventFilterFactory;
        }

        public async Task<ICollection<EventResponseModel>> GetAllEventsAsync()
        {
            var events = await this.eventRepository.FindAllAsync(
               include: src => src
                       .Include(er => er.EventRooms).ThenInclude(r => r.Room)
                       .Include(et => et.EventTeachers).ThenInclude(t => t.Teacher)
                       .Include(ed => ed.EventDisciplines).ThenInclude(d => d.Discipline)
                       .Include(es => es.EventSpecialties).ThenInclude(s => s.Specialty));

            var mappedEvents = this.mapper.Map<ICollection<EventResponseModel>>(events);

            return mappedEvents;
        }

        public async Task<ICollection<EventResponseModel>> GetAllFilteredEventsAsync(EventFilterRequestModel eventFilterRequestModel)
        {
            var applicableFilterStrategy = this.eventFilterFactory.GetEventFilterStrategy(eventFilterRequestModel.Type);

            if (applicableFilterStrategy == null)
            {
                throw new InvalidOperationException("Invalid serach filter type.");
            }

            return await applicableFilterStrategy.GetFilteredEvents(eventFilterRequestModel.Id);
        }

        public async Task<ValidationStatus> CreateEventAsync(BaseEventRequestModel createEventRequest)
        {
            var validationResult = await this.ValidateEventsStateAsync(createEventRequest);

            if (!validationResult.Success)
            {
                return validationResult;
            }

            var mappedEvent = this.mapper.Map<Event>(createEventRequest);
            await this.eventRepository.AddAsync(mappedEvent);
            await this.eventRepository.SaveAsync();

            return validationResult;
        }

        public async Task<ValidationStatus> UpdateEventAsync(BaseEventRequestModel updateEventRequest)
        {
            var validationResult = new ValidationStatus() { Success = true, Errors = new HashSet<string>() };

            var existingEvent = await this.eventRepository.GetIncludingAsync(
                    id: updateEventRequest.Id,
                    include: src => src
                       .Include(er => er.EventRooms).ThenInclude(r => r.Room)
                       .Include(et => et.EventTeachers).ThenInclude(t => t.Teacher)
                       .Include(ed => ed.EventDisciplines).ThenInclude(d => d.Discipline)
                       .Include(es => es.EventSpecialties).ThenInclude(s => s.Specialty));

            if (existingEvent == null)
            {
                validationResult.Success = false;
                validationResult.Errors.Add("Event to edit not found!");

                return validationResult;
            }

            var eventValidationStatus = await this.ValidateEventsStateAsync(updateEventRequest, validationResult);

            validationResult.Success = eventValidationStatus.Success;
            validationResult.Errors.UnionWith(eventValidationStatus.Errors);

            if (!validationResult.Success)
            {
                return validationResult;
            }

            this.mapper.Map<BaseEventRequestModel, Event>(updateEventRequest, existingEvent);
            await this.eventRepository.SaveAsync();

            return validationResult;
        }

        public async Task<ICollection<EventResponseModel>> GetEventsBySpecialtyIdAsync(Guid specialtyId, Guid updateEventId = default)
        {
            var eventsBySpecialty = await this.eventRepository.FindAllAsync(
                include: src => src
                        .Include(er => er.EventRooms).ThenInclude(r => r.Room)
                        .Include(et => et.EventTeachers).ThenInclude(t => t.Teacher)
                        .Include(ed => ed.EventDisciplines).ThenInclude(d => d.Discipline)
                        .Include(es => es.EventSpecialties).ThenInclude(s => s.Specialty),
                predicate: e => e.Id != updateEventId && e.EventSpecialties.Any(eId => eId.SpecialtyId == specialtyId));

            var mappedEvents = this.mapper.Map<ICollection<EventResponseModel>>(eventsBySpecialty);

            return mappedEvents;
        }

        public async Task<ICollection<EventResponseModel>> GetEventsByDisciplineIdAsync(Guid disciplineId, Guid updateEventId = default)
        {
            var eventsByDiscipline = await this.eventRepository.FindAllAsync(
                include: src => src
                        .Include(er => er.EventRooms).ThenInclude(r => r.Room)
                        .Include(et => et.EventTeachers).ThenInclude(t => t.Teacher)
                        .Include(ed => ed.EventDisciplines).ThenInclude(d => d.Discipline)
                        .Include(es => es.EventSpecialties).ThenInclude(s => s.Specialty),
                predicate: e => e.Id != updateEventId && e.EventDisciplines.Any(eId => eId.DisciplineId == disciplineId));

            var mappedEvents = this.mapper.Map<ICollection<EventResponseModel>>(eventsByDiscipline);

            return mappedEvents;
        }

        public async Task<ICollection<EventResponseModel>> GetEventsByTeacherIdAsync(Guid teacherId, Guid updateEventId = default)
        {
            var eventsByTeacher = await this.eventRepository.FindAllAsync(
                include: src => src
                        .Include(er => er.EventRooms).ThenInclude(r => r.Room)
                        .Include(et => et.EventTeachers).ThenInclude(t => t.Teacher)
                        .Include(ed => ed.EventDisciplines).ThenInclude(d => d.Discipline)
                        .Include(es => es.EventSpecialties).ThenInclude(s => s.Specialty),
                predicate: e => e.Id != updateEventId && e.EventTeachers.Any(tId => tId.TeacherId == teacherId));

            var mappedEvents = this.mapper.Map<ICollection<EventResponseModel>>(eventsByTeacher);

            return mappedEvents;
        }

        public async Task<ICollection<EventResponseModel>> GetEventsByRoomIdAsync(Guid roomId, Guid updateEventId = default)
        {
            var eventsByRoom = await this.eventRepository.FindAllAsync(
                include: src => src
                    .Include(er => er.EventRooms).ThenInclude(r => r.Room)
                    .Include(et => et.EventTeachers).ThenInclude(t => t.Teacher)
                    .Include(ed => ed.EventDisciplines).ThenInclude(d => d.Discipline)
                    .Include(es => es.EventSpecialties).ThenInclude(s => s.Specialty),
                predicate: e => e.Id != updateEventId && e.EventRooms.Any(eId => eId.RoomId == roomId));

            var mappedEvents = this.mapper.Map<ICollection<EventResponseModel>>(eventsByRoom);

            return mappedEvents;
        }

        public async Task DeleteEventAsync(Guid id)
        {
            var dbEvent = await this.eventRepository.GetIncludingAsync(
               id: id,
               include: src => src
                       .Include(er => er.EventRooms)
                       .Include(et => et.EventTeachers)
                       .Include(ed => ed.EventDisciplines)
                       .Include(es => es.EventSpecialties));

            if (dbEvent == null)
            {
                throw new InvalidOperationException("Event to delete does not exist.");
            }

            foreach (var roomEvent in dbEvent?.EventRooms)
            {
                roomEvent.IsDeleted = true;
            }

            foreach (var teacherEvent in dbEvent?.EventTeachers)
            {
                teacherEvent.IsDeleted = true;
            }

            foreach (var disciplineEvent in dbEvent?.EventDisciplines)
            {
                disciplineEvent.IsDeleted = true;
            }

            foreach (var specialtyEvent in dbEvent?.EventSpecialties)
            {
                specialtyEvent.IsDeleted = true;
            }

            await this.eventRepository.Delete(id);

            await this.eventRepository.SaveAsync();
        }

        private async Task<ValidationStatus> ValidateEventsStateAsync(BaseEventRequestModel eventRequestModel, ValidationStatus validationStatus = null)
        {
            if (validationStatus == null)
            {
                validationStatus = new ValidationStatus
                {
                    Success = true,
                    Errors = new HashSet<string>()
                };
            }

            if (DateTime.Parse(eventRequestModel.StartTime) >= DateTime.Parse(eventRequestModel.EndTime))
            {
                validationStatus.Success = false;
                validationStatus.Errors.Add("Start date cannot be after end date.");
            }

            var eventSpan = DateTime.Parse(eventRequestModel.EndTime) - DateTime.Parse(eventRequestModel.StartTime);

            if (eventSpan.TotalMinutes < 15)
            {
                validationStatus.Success = false;
                validationStatus.Errors.Add("Event cannot be shorter than 15 minutes.");
            }

            // Validate rooms occupation
            foreach (var roomId in eventRequestModel?.Rooms)
            {
                var roomEvents = await this.GetEventsByRoomIdAsync(roomId, eventRequestModel.Id);
                var validationResult = this.ValidateOverlapingEvents(roomEvents, DateTime.Parse(eventRequestModel.StartTime), DateTime.Parse(eventRequestModel.EndTime), validationStatus);

                validationStatus.Success = validationResult.Success != false;
                validationStatus.Errors.UnionWith(validationResult.Errors);
            }

            // Validate teachers occupation
            foreach (var teacherId in eventRequestModel?.Teachers)
            {
                var teacherEvents = await this.GetEventsByTeacherIdAsync(teacherId, eventRequestModel.Id);
                var validationResult = this.ValidateOverlapingEvents(teacherEvents, DateTime.Parse(eventRequestModel.StartTime), DateTime.Parse(eventRequestModel.EndTime), validationStatus);

                validationStatus.Success = validationResult.Success != false;
                validationStatus.Errors.UnionWith(validationResult.Errors);
            }

            // Validate specialty occupation
            foreach (var specialtyId in eventRequestModel?.Specialties)
            {
                var specialtiesEvents = await this.GetEventsBySpecialtyIdAsync(specialtyId, eventRequestModel.Id);
                var validationResult = this.ValidateOverlapingEvents(specialtiesEvents, DateTime.Parse(eventRequestModel.StartTime), DateTime.Parse(eventRequestModel.EndTime), validationStatus);

                validationStatus.Success = validationResult.Success != false;
                validationStatus.Errors.UnionWith(validationResult.Errors);
            }

            // Validate disciplines occupation
            foreach (var disciplineId in eventRequestModel?.Disciplines)
            {
                var disciplineEvents = await this.GetEventsByDisciplineIdAsync(disciplineId, eventRequestModel.Id);
                var validationResult = this.ValidateOverlapingEvents(disciplineEvents, DateTime.Parse(eventRequestModel.StartTime), DateTime.Parse(eventRequestModel.EndTime), validationStatus);

                validationStatus.Success = validationResult.Success != false;
                validationStatus.Errors.UnionWith(validationResult.Errors);
            }

            return validationStatus;
        }

        private ValidationStatus ValidateOverlapingEvents(ICollection<EventResponseModel> events, DateTime starTime, DateTime endTime, ValidationStatus validationStatus = null)
        {
            if (validationStatus == null)
            {
                validationStatus = new ValidationStatus
                {
                    Success = true,
                    Errors = new HashSet<string>()
                };
            }

            var overlapingEvents = events
                    .Where(re => (DateTime.Parse(re.StartTime) > starTime && DateTime.Parse(re.StartTime) < endTime) ||
                                 (DateTime.Parse(re.EndTime) > starTime && DateTime.Parse(re.EndTime) < endTime))
                    .ToList();

            if (overlapingEvents.Count > 0)
            {
                // We have already defined events in this time slot.
                overlapingEvents.ForEach(e =>
                {
                    var currentError = $"Existing event => Starting at: {DateTime.Parse(e.StartTime).ToLocalTime()} End at: {DateTime.Parse(e.EndTime).ToLocalTime()}; " +
                    $"Rooms: '{string.Join(", ", e.Rooms.Select(num => num.Number))}'; " +
                    $"Teachers: '{string.Join(", ", e.Teachers.Select(t => $"{t.AcademicTitle} {t.FirstName} {t.LastName}"))}'; " +
                    $"Disciplines: '{string.Join(", ", e.Disciplines.Select(n => n.Name))}'; " +
                    $"Specialties: '{string.Join(", ", e.Specialties.Select(s => $"{s.Name} - {s.Grade}"))}'; ";

                    validationStatus.Errors.Add(currentError);
                });

                validationStatus.Success = false;
            }

            return validationStatus;
        }
    }
}
