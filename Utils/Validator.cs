using System.Text.RegularExpressions;

namespace NoteApp.Utils
{
    public static class Validator
    {
        public static bool ValidateString(string e) { 
            return !String.IsNullOrEmpty(e) && !String.IsNullOrWhiteSpace(e);
        }

        public static bool PasswordMatch(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }
    }
}
