using System.Web.Mvc;
using NUnit.Framework;
using StickingPlace.WebHelpers;

namespace StickingPlace.UnitTests.WebHelpers
{
    [TestFixture]
    public class HtmlHelperExtensionsTests
    {
        [Test]
        public void IsSelected_will_return_an_attribute_if_the_given_controller_is_being_used()
        {
            var viewContext = new ViewContext();
            viewContext.RouteData.Values.Add("controller", "TestController");
            var html = new HtmlHelper(viewContext, new ViewPage());

            var isSelectedHtml = html.IsSelected("TestController", "isactive").ToHtmlString();

            Assert.That(isSelectedHtml, Is.EqualTo(" class=\"isactive\""));
        }

        [Test]
        public void IsSelected_will_return_empty_string_if_the_given_controller_is_not_being_used()
        {
            var viewContext = new ViewContext();
            viewContext.RouteData.Values.Add("controller", "NotTheTestController");
            var html = new HtmlHelper(viewContext, new ViewPage());

            var isSelectedHtml = html.IsSelected("TestController", "isactive").ToHtmlString();

            Assert.That(isSelectedHtml, Is.EqualTo(string.Empty));
        }
        
        [Test]
        public void IsSelected_uses_a_CSS_class_of_selected_by_default_if_you_dont_specify_one()
        {
            var viewContext = new ViewContext();
            viewContext.RouteData.Values.Add("controller", "TestController");
            var html = new HtmlHelper(viewContext, new ViewPage());

            var isSelectedHtml = html.IsSelected("TestController").ToHtmlString();

            Assert.That(isSelectedHtml, Is.EqualTo(" class=\"active\""));
        }
    }
}