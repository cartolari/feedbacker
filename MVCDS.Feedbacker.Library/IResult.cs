using System;

namespace MVCDS.Feedbacker.Library
{
    public interface IResult
    {
        string Message { get; }
        DateTime Date { get; }
        bool TriggersFailure { get; }
    }

    public interface IValue<T>
    {
        T Value { get; }
    }
}
