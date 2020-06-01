namespace RMS.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class EntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpecialtyEvent>()
                .HasKey(x => new { x.EventId, x.SpecialtyId });

            modelBuilder.Entity<SpecialtyDiscipline>()
                .HasKey(x => new { x.SpecialtyId, x.DisciplineId });

            modelBuilder.Entity<DisciplineEvent>()
                .HasKey(x => new { x.EventId, x.DisciplineId });

            modelBuilder.Entity<TeacherEvent>()
                .HasKey(x => new { x.EventId, x.TeacherId });

            modelBuilder.Entity<RoomEvent>()
                .HasKey(x => new { x.EventId, x.RoomId });
        }
    }
}