using System;

namespace MVCDS.Feedbacker.Library
{
    /// <summary>
    /// It doesn't trigger failure for a feedback
    /// </summary>
    public class Information : Result
    {

        internal Information(string message)
        {
            MessageValidator.Assert(message);

            this.message = message.Trim();
        }

        readonly private string message;
        /// <summary>
        /// The information itself
        /// </summary>
        public override string Message
        {
            get
            {
                return message;
            }
        }

        /// <summary>
        /// As an information, it never triggers the feedback's failure
        /// </summary>
        public override bool TriggersFailure
        {
            get
            {
                return false;
            }
        }
    }

    public class Information<T> : Information, IValue<T>
    {
        internal Information(string message, T value)
            : base (message)
        {
            Value = value;
        }

        /// <summary>
        /// Information about the information
        /// </summary>
        public T Value { get; private set; }
    }
}
