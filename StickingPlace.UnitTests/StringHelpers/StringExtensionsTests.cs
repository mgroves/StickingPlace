using System;
using NUnit.Framework;

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

        [Test]
        public void ToSHA256_returns_null_and_empty_string_when_given_the_same()
        {
            var nullResult = (null as string).ToSHA256();
            var emptyResult = string.Empty.ToSHA256();

            Assert.That(nullResult, Is.Null);
            Assert.That(emptyResult, Is.Empty);
        }

        [Test]
        public void ToSHA256_returns_the_SHA256_has_of_a_given_string()
        {
            var result = "test".ToSHA256();

            // this SHA256 string generated at http://online-encoder.com/sha256-encoder-decoder.html 
            Assert.That(result, Is.EqualTo("9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"));
        }
    }
}