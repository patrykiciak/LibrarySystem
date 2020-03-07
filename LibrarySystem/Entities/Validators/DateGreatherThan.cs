using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Entities.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private string DateToCompare { get; }

        public DateGreaterThanAttribute(string dateToCompare)
        {
            DateToCompare = dateToCompare;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime earlierDate = (DateTime)value;

            DateTime laterDate = (DateTime)validationContext.ObjectType.GetProperty(DateToCompare).GetValue(validationContext.ObjectInstance, null);

            if (laterDate < earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date to has to be later than date from");
            }
        }
    }
}
