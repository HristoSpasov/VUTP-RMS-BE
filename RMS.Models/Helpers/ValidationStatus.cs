namespace RMS.API.Models.Helpers
{
    using System.Collections.Generic;

    public class ValidationStatus
    {
        public bool Success { get; set; }

        public HashSet<string> Errors { get; set; }
    }
}
