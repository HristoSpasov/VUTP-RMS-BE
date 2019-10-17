namespace RMS.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class EntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpecialtyEvent>()
                .HasKey(x => new { x.EventId, x.SpecialtyId});

            modelBuilder.Entity<SpecialtyDiscipline>()
                .HasKey(x => new { x.SpecialtyId, x.DisciplineId });

        }
    }
}