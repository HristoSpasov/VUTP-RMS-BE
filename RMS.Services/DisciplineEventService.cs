namespace RMS.Services
{
    using Microsoft.EntityFrameworkCore;
    using RMS.Repositories.Contracts;
    using RMS.Services.Contracts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DisciplineEventService : IDisciplineEventService
    {
        private readonly IDisciplineEventRepository disciplineEventRepository;
        private readonly IEventService eventService;

        public DisciplineEventService(IDisciplineEventRepository disciplineEventRepository, IEventService eventService)
        {
            this.disciplineEventRepository = disciplineEventRepository;
            this.eventService = eventService;
        }

        public async Task DeleteDisciplinesEventsByDisciplineIdAsync(Guid disciplineId)
        {
            // Check if this is the only discipline assigned to the relating events
            var eventsByDiscipline = await this.eventService.GetEventsByDisciplineIdAsync(disciplineId);

            foreach (var ev in eventsByDiscipline)
            {
                if (ev.Disciplines.Count <= 1)
                {
                    // This is the only discipline for this event
                    // Delete the event
                    await this.eventService.DeleteEventAsync(ev.Id);
                }
                else
                {
                    // There are other disciplines for this event, so the event is preserved
                    // Delete only the records for the current discipline
                    var disciplineEvents = await this.disciplineEventRepository.FindAllAsync(predicate: t => t.DisciplineId == disciplineId);

                    disciplineEvents.ToList().ForEach(e =>
                    {
                        e.IsDeleted = true;
                    });

                    await this.disciplineEventRepository.SaveAsync();
                }
            }
        }
    }
}
