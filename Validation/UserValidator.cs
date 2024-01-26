using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecipeSiteBackend.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;
using RecipeSiteBackend.Services;

namespace RecipeSiteBackend.Validation
{
    
    public class UserValidator
    {
        public static ValidationError? validateInput(User user)
        {
            if(string.IsNullOrEmpty(user.Firstname))
            {
                return new ValidationError
                {
                    ErrorField = "firstname",
                    Message = "Firstname is required"
                };
            }

            if(string.IsNullOrEmpty(user.Password)||!passwordIsValid(user.Password))
            {
                return new ValidationError
                {
                    ErrorField = "password",
                    Message = "Password must be at least 8 characters long and contain both alphabets and numeric characters"
                };
            }

            if (string.IsNullOrEmpty(user.Email) || !emailIsValid(user.Email))
            {
                return new ValidationError
                {
                    ErrorField= "email",
                    Message = "Invalid Email"
                };
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                return new ValidationError
                {
                    ErrorField = "username",
                    Message = "Username cannot be empty"
                };
            }

            var usernameValidation = usernameIsValid(user.Username);

            if (!usernameValidation.Item1)
            {
                return new ValidationError
                {
                    ErrorField = "username",
                    Message = usernameValidation.Item2
                };
            }



            return null;
            
        }

        private static bool passwordIsValid(string password)
        {
            // Must have at least 8 characters
            if (password.Length < 8)
            {
                return false;
            }

            // Check if the string contains at least one alphabet character
            if (!Regex.IsMatch(password, "[a-zA-Z]"))
            {
                return false;
            }

            // Check if the string contains at least one numeric character
            if (!Regex.IsMatch(password, "[0-9]"))
            {
                return false;
            }

            // If all conditions are met, the password is valid
            return true;
        }

        public static bool emailIsValid(string email)
        {
            try
            {
                // Attempt to create a MailAddress object
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                // The email address is not valid
                return false;
            }
        }

        private static (bool,string) usernameIsValid(string username)
        {
            //check if it contains any non-alphanumeric characters
            if(username.Any(c => !char.IsLetterOrDigit(c)))
            {
                return (false, "Username can only contain alphanumeric characters");
            }

            //check if it starts with an alphabet
            char firstChar = username[0];
            if (!Char.IsLetter(firstChar))
            {
                return (false, "Username must start with an alphabet");
            }

            //check if it contains at least 3 characters, but no more than 20 characters
            if(!(username.Length >= 3 && username.Length <= 20))
            {
                return (false, "Username must contain 3-20 characters");
            }

            return (true, "");


        }
    }
}
