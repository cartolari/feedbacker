using System;
using System.Runtime.Serialization;

namespace MVCDS.Feedbacker.Library
{
    internal class EmptyFeedbackException : Exception, IResult
    {
        internal EmptyFeedbackException(): base("The feedback cannot be empty")
        {
        }

        string IResult.Message
        {
            get
            {
                return Message;
            }
        }

        public DateTime Date { get; private set; }

        public bool TriggersFailure
        {
            get
            {
                return true;
            }
        }
    }
}