using System.Linq;
using System.Web.ModelBinding;
using NUnit.Framework;
using StickingPlace.WebHelpers;

namespace StickingPlace.UnitTests.WebHelpers
{
    [TestFixture]
    public class ModelStateDictionaryExtensionsTests
    {
        [Test]
        public void GetAllErrors_gets_all_errors_from_ModelState_into_one_collection()
        {
            var dict = new ModelStateDictionary();
            dict.AddModelError("field1","error1");
            dict.AddModelError("field1","error2");
            dict.AddModelError("field2","error3");
            dict.AddModelError("field3","error4");

            var allErrors = dict.GetAllErrors();

            Assert.That(allErrors.Count(), Is.EqualTo(4));
            Assert.That(allErrors, Contains.Item("error1"));
            Assert.That(allErrors, Contains.Item("error2"));
            Assert.That(allErrors, Contains.Item("error3"));
            Assert.That(allErrors, Contains.Item("error4"));
        }
    }
}