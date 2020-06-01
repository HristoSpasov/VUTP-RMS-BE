namespace RMS.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Specialty : BaseEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public int Grade { get; set; }

        public ICollection<SpecialtyDiscipline> SpecialtyDisciplines { get; set; }

        public ICollection<SpecialtyEvent> SpecialtyEvents { get; set; }
    }
}