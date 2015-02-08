using NUnit.Framework;
using System;
using TCL.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace TCL.Tests.Extensions
{
    [TestFixture]
    public class EnumTests
    {
        public enum TestEnum
        {
            [Obsolete()]
            [MyCool("my cool", 3)]
            Home,

            [MyCool("item 2", 7)]
            Item2,

            [MyCool("third item", 12)]
            [MyCool("also third item", 15)]
            ThirdItem,

            ForthItem
        }

        [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
        public sealed class MyCoolAttribute : System.Attribute
        {
            public string Name { get; private set; }
            public int Value { get; private set; }

            public MyCoolAttribute(string name, int value)
            {
                Name = name;
                Value = value;
            }
        }

        [Test]
        public void GetAttributValue_Valid()
        {
            TestEnum enumVal = TestEnum.Home;

            var enumValAtt = enumVal.GetAttributeValue<MyCoolAttribute>();

            Assert.AreEqual(enumValAtt.Name, "my cool");
        }

        [Test]
        public void GetAttrubeValues_Valid()
        {
            TestEnum enumVal = TestEnum.ThirdItem;

            var enumNames = enumVal.GetAttributeValues<MyCoolAttribute, string>(x => x.Name).ToList();

            var expected = new List<string>()
            {
                "third item",
                "also third item",
            };

            //can show up in any order
            CollectionAssert.AreEquivalent(expected, enumNames);
        }

        [Test]
        public void GetAttrubeValue_None()
        {
            TestEnum enumVal = TestEnum.ForthItem;

            var enumAttr = enumVal.GetAttributeValue<MyCoolAttribute, string>(x => x.Name);

            Assert.IsNull(enumAttr);
        }
    }
}
