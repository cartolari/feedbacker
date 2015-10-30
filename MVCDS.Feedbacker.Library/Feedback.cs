using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDS.Feedbacker.Library
{
    public class Feedback
    {
        public enum EmptyResult
        {
            Allow,
            Forbid
        }

        public Feedback(EmptyResult force = EmptyResult.Allow)
        {
            this.force = force;
            results = new List<IResult>();
        }

        private readonly List<IResult> results;
        public IResult[] Results
        {
            get
            {
                if (ErrorOnEmpty)
                {
                    return new IResult[] {
                        new Error("This feedback cannot be empty")
                    };
                }

                return results.ToArray();
            }
        }

        private readonly EmptyResult force;
        private bool ErrorOnEmpty
        {
            get
            {
                return force == EmptyResult.Forbid && !results.Any();
            }
        }

        public bool Success
        {
            get
            {
                return !Results.Any(p => p.TriggersFailure);
            }
        }

        public Feedback Error(Exception exception)
        {
            results.Add(new Error(exception));
            return this;
        }

        public Feedback Error(string message)
        {
            results.Add(new Error(message));
            return this;
        }

        public Feedback Error<T>(Exception exception, T value)
        {
            results.Add(new Error<T>(exception, value));
            return this;
        }

        public Feedback Error<T>(string message, T value)
        {
            results.Add(new Error<T>(message, value));
            return this;
        }

        public Feedback Inform(string message)
        {
            results.Add(new Information(message));
            return this;
        }

        public Feedback Inform<T>(string message, T value)
        {
            results.Add(new Information<T>(message, value));
            return this;
        }

        public Feedback Observe(string message, bool failure = false)
        {
            results.Add(new Observation(message, failure));
            return this;
        }

        public Feedback Observe(string message, Func<bool> failure)
        {
            results.Add(new Observation(message, failure));
            return this;
        }

        public Feedback Observe<T>(string message, T value, bool failure = false)
        {
            results.Add(new Observation<T>(message, failure, value));
            return this;
        }

        public Feedback Observe<T>(string message, T value, Func<bool> failure)
        {
            results.Add(new Observation<T>(message, failure, value));
            return this;
        }
    }
}
