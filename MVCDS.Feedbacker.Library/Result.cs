using System;

namespace MVCDS.Feedbacker.Library
{
    public class Result
    {
        public string Message { get; }
        public DateTime Date { get; }
        public bool TriggersFailure { get; }
    }

    public interface IValue<T>
    {
        T Value { get; }
    }
}
