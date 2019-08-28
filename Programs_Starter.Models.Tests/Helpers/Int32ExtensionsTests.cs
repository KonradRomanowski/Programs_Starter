using NUnit.Framework;
using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.Models.Tests.Helpers
{
    [TestFixture]
    class Int32ExtensionsTests
    {
        [TestCase(1, 5, false)]
        [TestCase(100, 0, true)]
        public void IsGreaterThanTests(int i, int value, bool expectedResult)
        {
            bool result = i.IsGreaterThan(value);
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, true)]
        [TestCase(2, true)]
        [TestCase(1, false)]
        [TestCase(10, true)]
        [TestCase(5, false)]
        public void IsEvenTests(int i, bool expectedResult)
        {
            bool result = i.IsEven();
            Assert.AreEqual(expectedResult, result);
        }
    }
}
