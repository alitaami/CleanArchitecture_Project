using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> ErrorMessages { get; set; }
        public string FriendlyErrorMessage { get; set; }
        public CustomValidationException(List<string> errorMessages , string friendlyErrorMessage) : base(friendlyErrorMessage)
        {
            ErrorMessages = errorMessages;
            FriendlyErrorMessage = friendlyErrorMessage;
        }

    }
}
