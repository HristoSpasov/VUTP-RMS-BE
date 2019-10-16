namespace RMS.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class EntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeacherEvent>()
                .HasKey(x => new { x.EventId, x.TeacherId });

            modelBuilder.Entity<RoomEvent>()
                .HasKey(x => new { x.EventId, x.RoomId});

            modelBuilder.Entity<SpecialtyDiscipline>()
                .HasKey(x => new { x.SpecialtyId, x.DisciplineId });

        }
    }
}