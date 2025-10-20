using System.ComponentModel.DataAnnotations;

namespace ManageEventBackend.Domains.Validator
{
    public class IsGuid : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            return Guid.TryParse(value.ToString(), out _);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a valid GUID.";
        }
    }
}
