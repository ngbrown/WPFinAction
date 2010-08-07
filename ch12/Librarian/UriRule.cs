using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Librarian
{
    public class UriRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            bool uriIsValid = false;
            string message;

            try
            {
                Uri uri = new Uri((string)value, UriKind.Absolute);
                uriIsValid = true;
                message = "URI is valid";
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return new ValidationResult(uriIsValid, message);
        }
    }
}
