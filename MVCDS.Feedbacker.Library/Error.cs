using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDS.Feedbacker.Library
{
    public class Error : IResult
    {
        internal Error(string message)
        {
            message = ValidateMessage(message);

            Information = new Exception(message);
            Date = DateTime.Now;
        }

        internal Error(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("Exception cannot be null");

            ValidateMessage(exception.Message);

            Information = exception;
            Date = DateTime.Now;
        }

        public DateTime Date { get; private set; }
        
        public string Message
        {
            get
            {
                return Read(Information, 0);
            }
        }

        private string Read(Exception information, int n)
        {
            if (information.InnerException == null)
                return information.Message;

            return information.Message 
                + Environment.NewLine 
                + new string('-', n + 1) 
                + Read(information.InnerException, n + 1);
        }

        public bool TriggersFailure
        {
            get
            {
                return true;
            }
        }

        public Exception Information { get; private set; }

        private string ValidateMessage(string message)
        {
            if (message == null)
                throw new ArgumentNullException("The message cannot be null");

            message = message.Trim();
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("The message cannot be empty");
            return message;
        }
    }
}
