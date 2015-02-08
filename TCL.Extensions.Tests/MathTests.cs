using NUnit.Framework;
using System;
using TCL.Extensions;

namespace TCL.Extensions.Tests
{
    [TestFixture]
    public class MathTests
    {
        [TestCase("2.3000000E-2", 0.023)]
        [TestCase("2.3E2", 230)]
        [TestCase("2.453E-5", 0.00002453)]
        public void ConvertFromStringValid(string input, decimal expected)
        {
            var result = ExponentsHelper.ConvertFromString(input);

            Assert.AreEqual(expected, result);
        }

        [TestCase(0.00002453, "2.453E-5")]
        [TestCase(321, "3.21E+2")]
        public void ConvertFromDecimalValid(decimal input, string expected)
        {
            var result = ExponentsHelper.ConvertFromDecimal(input);

            Assert.AreEqual(expected, result);
        }
    }
}
