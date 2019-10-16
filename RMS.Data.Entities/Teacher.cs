namespace RMS.Data.Entities
{
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    public class Teacher : BaseEntity
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string AcademicTitle { get; set; }
    }
}
