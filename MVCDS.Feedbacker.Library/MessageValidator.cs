using System;

namespace MVCDS.Feedbacker.Library
{
    internal static class MessageValidator
    {
        internal static void Assert(string message)
        {
            if (message == null)
                throw new ArgumentNullException("The message cannot be null");

            if (string.IsNullOrWhiteSpace(message.Trim()))
                throw new ArgumentException("The message cannot be empty");
        }
    }
}
