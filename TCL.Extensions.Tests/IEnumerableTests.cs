using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TCL.Extensions;

namespace TCL.Tests.Extensions
{
    [TestFixture]
    public class IEnumerableTests
    {
        [Test]
        public void PickRandom_Valid()
        {
            List<int> intList = Enumerable.Range(1, 10).ToList();

            var randomElement = intList.PickRandom();

            bool isInside = intList.Contains(randomElement);

            Assert.IsTrue(isInside);
        }
    }
}
