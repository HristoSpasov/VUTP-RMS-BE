namespace RMS.API.Models.Validators.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Not null or empty validation attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Struct, AllowMultiple = false)]
    public class GuidNotEmpty : ValidationAttribute
    {
        /// <inheritdoc/>
        public override bool IsValid(object value)
        {
            if (value == null || value.GetType() != typeof(Guid))
            {
                throw new InvalidOperationException("Attribute 'GuidNotNullOrEmpty' can be used only on 'Guid' type.");
            }

            if ((Guid)value == Guid.Empty)
            {
                return false;
            }

            return true;
        }
    }
}
