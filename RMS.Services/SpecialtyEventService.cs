namespace RMS.Services
{
    using Microsoft.EntityFrameworkCore;
    using RMS.Repositories.Contracts;
    using RMS.Services.Contracts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SpecialtyEventService : ISpecialtyEventService
    {
        private readonly ISpecialtyEventRepository specialtyEventRepository;
        private readonly IEventService eventService;

        public SpecialtyEventService(ISpecialtyEventRepository specialtyEventRepository, IEventService eventService)
        {
            this.specialtyEventRepository = specialtyEventRepository;
            this.eventService = eventService;
        }

        public async Task DeleteSpecialtiesEventsBySpecialtyIdAsync(Guid specialtyId)
        {
            // Check if this is the only specialty assigned to the relating events
            var eventsBySpecialty = await this.eventService.GetEventsBySpecialtyIdAsync(specialtyId);

            foreach (var ev in eventsBySpecialty)
            {
                if (ev.Specialties.Count <= 1)
                {
                    // This is the only specialty for this event
                    // Delete the event
                    await this.eventService.DeleteEventAsync(ev.Id);
                }
                else
                {
                    // There are other specialties for this event, so the event is preserved
                    // Delete only the records for the current specialty
                    var specialtyEvents = await this.specialtyEventRepository.FindAllAsync(predicate: t => t.SpecialtyId == specialtyId);
                    specialtyEvents.ToList().ForEach(e =>
                    {
                        e.IsDeleted = true;
                    });

                    await this.specialtyEventRepository.SaveAsync();
                }
            }
        }
    }
}
