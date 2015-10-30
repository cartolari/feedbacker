using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using lib = MVCDS.Feedbacker.Library;

namespace MVCDS.Feedbacker.UnitTest
{
    [TestClass]
    public class Error_Test
    {
        [TestMethod]
        public void Mimic_Message()
        {
            for (var i = 0; i < 5; i++)
            {
                Exception ex = Create(i);
                lib.Feedback feedback = new lib.Feedback()
                    .Error(ex);

                string value = feedback.Results[0]
                    .Message;
                string expected = Answer(i, i);
                Assert.AreEqual(expected, value, "Wrong at '" + Letter(i) + "' ("+ i +")");
            }
        }

        private string Letter(int n)
        {
            //int a = 97;
            return char.ConvertFromUtf32(n + 97);
        }

        private string Answer(int n, int fixo)
        {
            string letter = Letter(n);
            if (n == 0)
                return letter;
            return letter 
                + Environment.NewLine 
                + new string('-', fixo - n + 1) 
                + Answer(n - 1, fixo);
        }

        private Exception Create(int n)
        {
            string letter = Letter(n);
            if (n == 0)
                return new Exception(letter);
            return new Exception(letter, Create(n - 1));
        }
    }
}
