using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDS.Feedbacker.Library
{
    public interface IResult
    {
        string Message { get; }
        DateTime Date { get; }
        bool TriggersFailure { get; }
    }
}
