
using ListaTarefas.Exception;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ListaTarefas.Utils.validations
{
    public static class StringValidator
    {
        public static string ValidateNotEmpty(this string value, string msgError)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new TasksListExceptionValidate(new List<string> { msgError });
            return value.Trim();
        }

        public static string ValidateMinLength(this string value, int min, string msgError)
        {
            if (value == null || value.Length < min)
                throw new TasksListExceptionValidate(new List<string> { msgError });
            return value;
        }

        public static string ValidateMaxLength(this string value, int max, string msgError)
        {
            if (value != null && value.Length > max)
                throw new TasksListExceptionValidate(new List<string> { msgError });
            return value;
        }

        public static string ValidateEmail(this string value, string msgError)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(msgError);

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!regex.IsMatch(value))
                throw new ArgumentException(msgError);

            return value.Trim().ToLower();
        }



    }
}


