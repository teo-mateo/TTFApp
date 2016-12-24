using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTFApp.Models.Decide;
using TTFApp.Models;
using TTFApp.Models.Compute;

namespace TTFApp.Tests.Decide
{
    [TestClass]
    public class ABCDecision_Tests
    {
        private ABCDecision dec_base;
        private ABCDecision dec_s2;

        /// <summary>
        /// Creates an ABCDecision with the base rules
        /// </summary>
        /// <returns></returns>
        private ABCDecision CreateBaseDecisionObject()
        {
            var d = new ABCDecision();
            d.AddRule(SRT.S, (a, b, c) => a && b && !c);
            d.AddRule(SRT.R, (a, b, c) => a && b && c);
            d.AddRule(SRT.T, (a, b, c) => !a && b && c);
            return d;
        }

        [TestInitialize]
        public void Init()
        {
            //base rules
            dec_base = CreateBaseDecisionObject();
            
            //2nd specialization rules
            dec_s2 = CreateBaseDecisionObject();
            dec_s2.AddRule(SRT.T, (a, b, c) => a && b && !c);
            dec_s2.AddRule(SRT.S, (a, b, c) => a && !b && c);
        }

        /// <summary>
        /// Tests the happy cases of the base scenario
        /// </summary>
        [TestMethod]
        public void ABCDecision_BaseScenario_Test()
        {
            Assert.AreEqual(SRT.S, dec_base.Decide(true, true, false));
            Assert.AreEqual(SRT.R, dec_base.Decide(true, true, true));
            Assert.AreEqual(SRT.T, dec_base.Decide(false, true, true));
        }

        /// <summary>
        /// Tests that when the input doesn't match any of the rules, a proper exception is thrown
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(TTFException))]
        public void ABCDecision_Exception_Test()
        {
            //none should fit, a TTFException will be raised
            dec_base.Decide(false, false, false);
        }

        /// <summary>
        /// Tests the happy cases of the 2nd specialized scenario
        /// </summary>
        [TestMethod]
        public void ABCDecision_Specialized2_Test()
        {
            Assert.AreEqual(SRT.S, dec_s2.Decide(true, false, true));
            Assert.AreEqual(SRT.T, dec_s2.Decide(true, true, false));
            Assert.AreEqual(SRT.R, dec_s2.Decide(true, true, true));
        }
    }
}
