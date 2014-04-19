using System;
using NUnit.Framework;
using StickingPlace.StringHelpers;

namespace StickingPlace.UnitTests.StringHelpers
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ToMD5_should_return_null_if_given_null()
        {
            string nullString = null;
            nullString.ToMD5();
        }

        [Test]
        public void ToMD5_returns_the_MD5_hash_of_a_given_string()
        {
            // hash of "test" obtained from md5.net
            var md5OfTest = "098f6bcd4621d373cade4e832627b4f6";

            Assert.That(md5OfTest, Is.EqualTo("test".ToMD5()));
        }
    }
}