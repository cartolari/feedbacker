using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDS.Feedbacker.Library
{
    public class Feedback
    {
        public Feedback()
        {
            results = new List<IResult>();
        }

        private readonly List<IResult> results;
        public IResult[] Results
        {
            get
            {
                if (results.Any())
                    return results.ToArray();

                return new IResult[] {
                    new EmptyFeedbackException()
                };
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
