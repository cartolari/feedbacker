using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using lib = MVCDS.Feedbacker.Library;
using System.Linq;

namespace MVCDS.Feedbacker.UnitTest
{
    [TestClass]
    public class Feedback_Test
    {
        [TestMethod]
        public void Checks_Empty_Feedback()
        {
            lib.Feedback feedback = new lib.Feedback();
            Assert.IsFalse(feedback.Success);

            lib.IResult warning = feedback.Results
                .FirstOrDefault();
            Assert.IsNotNull(warning);
        }
    }
}
