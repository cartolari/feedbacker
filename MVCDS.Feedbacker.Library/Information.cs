﻿using System;

namespace MVCDS.Feedbacker.Library
{
    /// <summary>
    /// It doesn't trigger failure for a feedback
    /// </summary>
    public class Information : IResult
    {
        internal Information(string message)
        {
            MessageValidator.Assert(message);

            Message = message.Trim();
        }

        /// <summary>
        /// When the information was created
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// The information itself
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// As an information, it never triggers the feedback's failure
        /// </summary>
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

        /// <summary>
        /// Information about the information
        /// </summary>
        public T Value { get; private set; }
    }
}
