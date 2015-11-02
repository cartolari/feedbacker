using System;

namespace MVCDS.Feedbacker.Library
{
    /// <summary>
    /// It triggers failure for a feedback
    /// </summary>
    public class Error : Result
    {
        internal Error(string message)
        {
            MessageValidator.Assert(message);

            Information = new Exception(message.Trim());
        }

        internal Error(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException("Exception cannot be null");

            MessageValidator.Assert(exception.Message);

            Information = exception;
        }
        
        /// <summary>
        /// All exceptions messages gathered on a string
        /// </summary>
        public override string Message
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

        /// <summary>
        /// As an error, it always triggers the feedback's failure
        /// </summary>
        public override bool TriggersFailure
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// All exceptions gathered for dig more information
        /// </summary>
        public Exception Information { get; private set; }
    }

    /// <summary>
    /// It triggers failure for a feedback and holds a value for some aditional information
    /// </summary>
    public class Error<T>: Error, IValue<T>
    {
        internal Error(string message, T value)
            : base(message)
        {
            Value = value;
        }

        internal Error(Exception exception, T value)
            : base(exception)
        {
            Value = value;
        }

        /// <summary>
        /// Information about the error
        /// </summary>
        public T Value { get; private set; }
    }
}
