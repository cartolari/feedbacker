using Microsoft.VisualStudio.TestTools.UnitTesting;
using lib = MVCDS.Feedbacker.Library;
using System.Linq;

namespace MVCDS.Feedbacker.UnitTest
{
    [TestClass]
    public class Feedback_Test
    {
        [TestMethod]
        public void Checks_Empty_On_Forced_Feedback()
        {
            lib.Feedback feedback = new lib.Feedback(lib.Feedback.EmptyResult.Forbid);
            Assert.IsFalse(feedback.Success);

            lib.Result result = feedback.Results
                .FirstOrDefault();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Checks_Empty_On_Non_Forced_Feedback()
        {
            lib.Feedback feedback = new lib.Feedback();
            Assert.IsTrue(feedback.Success);

            lib.Result result = feedback.Results
                .FirstOrDefault();
            Assert.IsNull(result);
        }
    }
}
