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
                        new EmptyFeedbackException()
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
    }
}
