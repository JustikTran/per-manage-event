using System.ComponentModel.DataAnnotations;

namespace ManageEventBackend.Domains.Validator
{
    public class IsValidStart : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime startDate)
            {
                return startDate >= DateTime.UtcNow;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a future date and time.";
        }
    }
}
