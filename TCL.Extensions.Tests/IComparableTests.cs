using NUnit.Framework;
using System;
using TCL.Extensions;

namespace TCL.Extensions.Tests
{
    [TestFixture]
    public class IComparableTests
    {
        [TestCase(5, 3, 7, true, false, true)]
        [TestCase("bbb", "aaa", "ccc", false, false, true)]
        [TestCase(5, 5, 5, true, false, false)]
        [TestCase(5, 5, 8, true, false, true)]
        [TestCase(8, 5, 8, false, true, true)]
        [TestCase(5, 5, 8, true, true, true)]
        [TestCase(8, 5, 8, true, true, true)]
        public void Between_FullOptions<T>(T input, T min, T max, bool minInclusive, bool maxInclusive, bool expected) where T : IComparable<T>
        {
            bool actual = input.Between(min, max, minInclusive, maxInclusive);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(4, 2, 6, true)]
        [TestCase("bbb", "aaa", "ccc", true)]
        [TestCase(4.5, 2, 4.2, false)]
        public void Between_ShortOptions<T>(T input, T min, T max, bool expected) where T : IComparable<T>
        {
            bool actual = input.Between(min, max);
            Assert.AreEqual(expected, actual);
        }

    }
}
