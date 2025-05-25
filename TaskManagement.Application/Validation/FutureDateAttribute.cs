using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
               return date.Date >= DateTime.UtcNow.Date;
            }
            return true;
        }
    }
}
