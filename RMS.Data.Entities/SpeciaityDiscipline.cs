namespace RMS.Data.Entities
{
    using System;

    public class SpecialtyDiscipline : BaseEntity
    {
        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public Guid DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
    }
}