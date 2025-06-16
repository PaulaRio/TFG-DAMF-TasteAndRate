using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TasteAndRateAPI.Attributes
{
    public class EmailValidationAttribute : ValidationAttribute
    {
        private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public override bool IsValid(object value)
        {
            if (value is not string email)
                return false;

            return Regex.IsMatch(email, EmailPattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a valid email address.";
        }
    }
}

