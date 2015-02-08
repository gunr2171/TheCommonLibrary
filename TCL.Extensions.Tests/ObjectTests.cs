using NUnit.Framework;
using System;
using TCL.Extensions;

namespace TCL.Extensions.Tests
{
    [TestFixture]
    public class ObjectTests
    {
        [Serializable]
        public class MyClass
        {
            public string Val1 { get; set; }
        }

        [Test]
        public void Clone_Valid()
        {
            MyClass c1 = new MyClass();
            c1.Val1 = "wer";
            MyClass c2 = c1.Clone();

            c2.Val1 = "qwer";
            c1.Val1 = "asdf";

            Assert.AreEqual("qwer", c2.Val1);
        }

        [Test]
        public void InnerValueOrDefault_ManyLevelsNull()
        {
            Level1 l1 = null;

            var result = l1.InnerValueOrDefault(lev1 =>
                lev1.L2.InnerValueOrDefault(lev2 =>
                    lev2.L3.InnerValueOrDefault(lev3 => lev3.ABC, "lev3"),
                    "lev2"),
                "lev1");

            Assert.AreEqual("lev1", result);
        }

        [Test]
        public void InnerValueOrNull_ManyLevelsNull()
        {
            Level1 l1 = null;

            var result = l1.InnerValueOrNull(lev1 =>
                lev1.L2.InnerValueOrNull(lev2 =>
                    lev2.L3.InnerValueOrNull(lev3 => lev3.ABC)
                    )
                );

            Assert.AreEqual(null, result);
        }

        [Test]
        public void InnerValueOrDefault_ManyLevelsValid()
        {
            Level1 l1 = new Level1()
            {
                L1Name = "l1 name",
                L2 = new Level2()
                {
                    L3 = new Level3()
                    {
                        ABC = "def"
                    }
                }
            };

            var result = l1.InnerValueOrDefault(lev1 =>
                lev1.L2.InnerValueOrDefault(lev2 =>
                    lev2.L3.InnerValueOrDefault(lev3 => lev3.ABC, "lev3"),
                    "lev2"),
                "lev1");

            Assert.AreEqual("def", result);
        }

        [Test]
        public void InnerValueOrDefault_MainNull()
        {
            Level1 l1 = null;

            var result = l1.InnerValueOrDefault(x => x.L1Name, "nope");

            Assert.AreEqual("nope", result);
        }

        public class Level1
        {
            public Level2 L2 { get; set; }
            public string L1Name { get; set; }
        }

        public class Level2
        {
            public Level3 L3 { get; set; }
        }

        public class Level3
        {
            public string ABC { get; set; }
        }
    }
}
