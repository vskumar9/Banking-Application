using System.Text.RegularExpressions;

namespace BankApplicationAPI.Exceptions
{
    public class ApplicationUtil
    {
        public void ValidateEmail(string email)
        {
            
            var emailPattern = @"^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                throw new InvalidException($"Invalid Mail ID: {email}");
            }

        }

        public void ValidatePhoneNumber(string phoneNumber)
        {
            
            var phonePattern = @"^[6-9][0-9]{9}$";

            if (!Regex.IsMatch(phoneNumber, phonePattern))
            {
                throw new InvalidException($"Invalid Phone number: {phoneNumber}");
            }

        }

        public void ValidatePassword(string password)
        {

            var passwordPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$";

            if (!Regex.IsMatch(password, passwordPattern))
            {
                throw new InvalidException("Password must be at least 8 characters long, include an uppercase letter, a lowercase letter, a digit, and a special character.");
            }
        }
    }
}
