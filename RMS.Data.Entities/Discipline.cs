﻿namespace RMS.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Discipline : BaseEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<SpecialtyDiscipline> DisciplineSpecialties { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}