using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTFApp.Models.Compute;
using TTFApp.Models;

namespace TTFApp.Tests.Compute
{
    [TestClass]
    public class TTFCompute_Tests
    {
        [TestMethod]
        public void TTFCompute_Test()
        {
            var r1 = new TTFCompute().Compute(new TTFInput(true, true, false, 8, 9, 10));
            Assert.AreEqual(r1.X, SRT.S);
            Assert.AreEqual(r1.Y, (decimal)(8.0m + (8.0m * 9.0m / 100.0m)));

            var r2 = new TTFCompute().Compute(new TTFInput(true, true, true, 8, 9, 10));
            Assert.AreEqual(r2.X, SRT.R);
            Assert.AreEqual(r2.Y, (decimal)(8.0m + (8.0m * (9.0m - 10.0m) / 100.0m)));

            var r3 = new TTFCompute().Compute(new TTFInput(false, true, true, 8, 9, 10));
            Assert.AreEqual(r3.X, SRT.T);
            Assert.AreEqual(r3.Y, 8.0m - (8.0m * 10.0m / 100.0m));
        }

        [TestMethod]
        public void TTFComputeSpecialized1_Test()
        {
            var r1 = new TTFComputeSpecialized1().Compute(new TTFInput(true, true, true, 8, 9, 10));
            Assert.AreEqual(r1.X, SRT.R);
            Assert.AreEqual(r1.Y, (2 * 8.0m + (8.0m * 9.0m / 100.0m)));
        }

        [TestMethod]
        public void TTFComputeSpecialized2_Test()
        {
            var r1 = new TTFComputeSpecialized2().Compute(new TTFInput(true, true, false, 8, 9, 10));
            Assert.AreEqual(r1.X, SRT.T);
            Assert.AreEqual(r1.Y, 8.0m - (8.0m * 10.0m / 100.0m));

            var r2 = new TTFComputeSpecialized2().Compute(new TTFInput(true, false, true, 8, 9, 10));
            Assert.AreEqual(r2.X, SRT.S);
            Assert.AreEqual(r2.Y, 10.0m + 8.0m + (8.0m * 9.0m / 100.0m));
        }
    }
}
