using System;

namespace MVCDS.Feedbacker.Library
{
    public class Information : IResult
    {
        internal Information(string message)
        {
            MessageValidator.Assert(message);

            Message = message.Trim();
        }

        public DateTime Date { get; private set; }

        public string Message { get; private set; }

        public bool TriggersFailure
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

        public T Value { get; private set; }
    }
}
