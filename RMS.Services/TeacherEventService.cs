namespace RMS.Services
{
    using Microsoft.EntityFrameworkCore;
    using RMS.Repositories.Contracts;
    using RMS.Services.Contracts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeacherEventService : ITeacherEventService
    {
        private readonly ITeacherEventRepository teacherEventRepository;
        private readonly IEventService eventService;

        public TeacherEventService(ITeacherEventRepository teacherEventRepository, IEventService eventService)
        {
            this.teacherEventRepository = teacherEventRepository;
            this.eventService = eventService;
        }

        public async Task DeleteTeachersEventsByTeacherIdAsync(Guid teacherId)
        {
            // Check if this is the only teacher assigned to the relating events
            var eventsByTeacher = await this.eventService.GetEventsByTeacherIdAsync(teacherId);

            foreach (var ev in eventsByTeacher)
            {
                if (ev.Teachers.Count <= 1)
                {
                    // This is the only teacher for this event
                    // Delete the event
                    await this.eventService.DeleteEventAsync(ev.Id);
                }
                else
                {
                    // There are other teachers for this event, so the event is preserved
                    // Delete only the records for the current teacher
                    var teacherEvents = await this.teacherEventRepository.FindAllAsync(predicate: t => t.TeacherId == teacherId);
                    
                    teacherEvents.ToList().ForEach(e =>
                    {
                        e.IsDeleted = true;
                    });

                    await this.teacherEventRepository.SaveAsync();
                }
            }
        }
    }
}
