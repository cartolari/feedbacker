using System;

namespace MVCDS.Feedbacker.Library
{
    public class Observation : IResult
    {
        internal Observation(string message, bool isFailure)
            : this(message, () => isFailure)
        {
        }

        internal Observation(string message, Func<bool> isFailure)
        {
            MessageValidator.Assert(message);

            Message = message.Trim();
            callback = isFailure;
        }

        public DateTime Date { get; private set; }

        public string Message { get; private set; }

        Func<bool> callback;
        public bool TriggersFailure
        {
            get
            {
                return callback();
            }
        }
    }

    public class Observation<T> : Observation, IValue<T>
    {
        internal Observation(string message, bool isFailure, T value)
            : base(message, isFailure)
        {
            Value = value;
        }

        internal Observation(string message, Func<bool> isFailure, T value)
            : base(message, isFailure)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
