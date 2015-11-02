using System;

namespace MVCDS.Feedbacker.Library
{
    /// <summary>
    /// A feedback part
    /// </summary>
    public abstract class Result
    {
        protected Result()
        {
            Date = DateTime.Now;
        }

        public virtual string Message { get; }

        /// <summary>
        /// When the result was created
        /// </summary>
        public DateTime Date { get; }

        public abstract bool TriggersFailure { get; }
    }

    public interface IValue<T>
    {
        T Value { get; }
    }
}
