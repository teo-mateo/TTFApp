using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTFApp.Models.Decide;

namespace TTFApp.Tests.Decide
{
    [TestClass]
    public class ABCRule_Tests
    {
        /// <summary>
        /// building various expressions and testing the results
        /// </summary>
        [TestMethod]
        public void ABCRule_Test()
        {
            ABCRule r = new ABCRule(Models.SRT.R, (a, b, c) => !a && !b && !c);
            Assert.AreEqual(true, r.Fits(false, false, false));

            r = new ABCRule(Models.SRT.R, (a, b, c) => a || b || c);
            Assert.AreEqual(false, r.Fits(false, false, false));

            //now, the rules from the requirement
            ABCRule r1 = new ABCRule(Models.SRT.S, (a, b, c) => a && b && !c);
            Assert.AreEqual(true, r1.Fits(true, true, false));
            Assert.AreEqual(false, r1.Fits(true, false, true));

            ABCRule r2 = new ABCRule(Models.SRT.R, (a, b, c) => a && b && c);
            Assert.AreEqual(true, r2.Fits(true, true, true));
            Assert.AreEqual(false, r2.Fits(false, true, true));

            ABCRule r3 = new ABCRule(Models.SRT.T, (a, b, c) => !a && b && c);
            Assert.AreEqual(true, r3.Fits(false, true, true));
            Assert.AreEqual(false, r3.Fits(true, true, true));
        }
    }
}
