using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using Telerik.JustMock;

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

        [Test]
        public void ActionImage_creates_an_anchor_with_an_image_inside()
        {
            var viewContext = ArrangeViewContextRigamaroleSoUrlHelperWorks();
            var html = new HtmlHelper(viewContext, new ViewPage());

            var resultHtml = html.ActionImage("Index", "Test", "/Content/Images/foo.png", "click me!", new {id = 99}).ToHtmlString();

            var expectedHtml = @"<a href=""/Test/Index/99""><img alt=""click me!"" src=""/Content/Images/foo.png"" /></a>";
            Assert.That(resultHtml, Is.EqualTo(expectedHtml));

            // cleanup the route(s) that were added for this test
            RouteTable.Routes.Clear();
        }

        ViewContext ArrangeViewContextRigamaroleSoUrlHelperWorks()
        {
            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Test", action = "Index", id = UrlParameter.Optional }
            );

            var response = Mock.Create<HttpResponseBase>();
            Mock.Arrange(() => response.ApplyAppPathModifier(Arg.AnyString)).Returns(p => p);

            var context = Mock.Create<HttpContextBase>();
            Mock.Arrange(() => context.Response).Returns(response);
            var workerrequest = Mock.Create<HttpWorkerRequest>(Behavior.Loose);
            Mock.Arrange(() => context.GetService(typeof(HttpWorkerRequest))).Returns(workerrequest);

            var viewContext = new ViewContext();
            viewContext.RequestContext = new RequestContext(context, new RouteData());
            return viewContext;
        }

        public class SomeModel
        {
            public string TestProperty { get; set; }
        }

        [Test]
        public void StateDropDownListFor_should_create_a_dropdown_with_only_USStates_and_Washington_DC()
        {
            var html = CreateHtmlHelper<SomeModel>();

            var stateDropDownListHtml = html.StateDropDownListFor(x => x.TestProperty).ToHtmlString();

            foreach (var stateKey in StatesProvincesTerritories.StatesAndDC.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", stateKey,StatesProvincesTerritories.StatesAndDC[stateKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
            foreach (var provinceKey in StatesProvincesTerritories.Provinces.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", provinceKey, StatesProvincesTerritories.Provinces[provinceKey]);
                Assert.That(stateDropDownListHtml, !Contains.Substring(expectedOption));
            }
            foreach (var territoryKey in StatesProvincesTerritories.Territories.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", territoryKey, StatesProvincesTerritories.Territories[territoryKey]);
                Assert.That(stateDropDownListHtml, !Contains.Substring(expectedOption));
            }
        }
        
        [Test]
        public void ProvicesDropDownListFor_should_create_a_dropdown_with_only_Canadian_provinces()
        {
            var html = CreateHtmlHelper<SomeModel>();

            var stateDropDownListHtml = html.ProvicesDropDownListFor(x => x.TestProperty).ToHtmlString();

            foreach (var provinceKey in StatesProvincesTerritories.Provinces.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", provinceKey, StatesProvincesTerritories.Provinces[provinceKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
            foreach (var stateKey in StatesProvincesTerritories.StatesAndDC.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", stateKey, StatesProvincesTerritories.StatesAndDC[stateKey]);
                Assert.That(stateDropDownListHtml, !Contains.Substring(expectedOption));
            }
            foreach (var territoryKey in StatesProvincesTerritories.Territories.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", territoryKey, StatesProvincesTerritories.Territories[territoryKey]);
                Assert.That(stateDropDownListHtml, !Contains.Substring(expectedOption));
            }
        } 
       
        [Test]
        public void TerritoriesDropDownListFor_should_create_a_dropdown_with_only_Canadian_provinces()
        {
            var html = CreateHtmlHelper<SomeModel>();

            var stateDropDownListHtml = html.TerritoriesDropDownListFor(x => x.TestProperty).ToHtmlString();

            foreach (var territoryKey in StatesProvincesTerritories.Territories.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", territoryKey, StatesProvincesTerritories.Territories[territoryKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
            foreach (var stateKey in StatesProvincesTerritories.StatesAndDC.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", stateKey, StatesProvincesTerritories.StatesAndDC[stateKey]);
                Assert.That(stateDropDownListHtml, !Contains.Substring(expectedOption));
            }
            foreach (var provinceKey in StatesProvincesTerritories.Provinces.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", provinceKey, StatesProvincesTerritories.Provinces[provinceKey]);
                Assert.That(stateDropDownListHtml, !Contains.Substring(expectedOption));
            }
        }
        
        [Test]
        public void StatesAndTerritoriesDropDownListFor_should_create_a_dropdown_with_only_Canadian_provinces()
        {
            var html = CreateHtmlHelper<SomeModel>();

            var stateDropDownListHtml = html.StatesAndTerritoriesDropDownListFor(x => x.TestProperty).ToHtmlString();

            foreach (var territoryKey in StatesProvincesTerritories.Territories.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", territoryKey, StatesProvincesTerritories.Territories[territoryKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
            foreach (var stateKey in StatesProvincesTerritories.StatesAndDC.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", stateKey, StatesProvincesTerritories.StatesAndDC[stateKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
            foreach (var provinceKey in StatesProvincesTerritories.Provinces.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", provinceKey, StatesProvincesTerritories.Provinces[provinceKey]);
                Assert.That(stateDropDownListHtml, !Contains.Substring(expectedOption));
            }
        }
        
        [Test]
        public void StatesAndProvincesDropDownListFor_should_create_a_dropdown_with_only_Canadian_provinces()
        {
            var html = CreateHtmlHelper<SomeModel>();

            var stateDropDownListHtml = html.StatesAndProvincesDropDownListFor(x => x.TestProperty).ToHtmlString();

            foreach (var territoryKey in StatesProvincesTerritories.Territories.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", territoryKey, StatesProvincesTerritories.Territories[territoryKey]);
                Assert.That(stateDropDownListHtml, !Contains.Substring(expectedOption));
            }
            foreach (var stateKey in StatesProvincesTerritories.StatesAndDC.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", stateKey, StatesProvincesTerritories.StatesAndDC[stateKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
            foreach (var provinceKey in StatesProvincesTerritories.Provinces.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", provinceKey, StatesProvincesTerritories.Provinces[provinceKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
        }
        
        [Test]
        public void StatesAndProvincesAndTerritoriesDropDownListFor_should_create_a_dropdown_with_only_Canadian_provinces()
        {
            var html = CreateHtmlHelper<SomeModel>();

            var stateDropDownListHtml = html.StatesAndProvincesAndTerritoriesDropDownListFor(x => x.TestProperty).ToHtmlString();

            foreach (var territoryKey in StatesProvincesTerritories.Territories.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", territoryKey, StatesProvincesTerritories.Territories[territoryKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
            foreach (var stateKey in StatesProvincesTerritories.StatesAndDC.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", stateKey, StatesProvincesTerritories.StatesAndDC[stateKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
            foreach (var provinceKey in StatesProvincesTerritories.Provinces.Keys)
            {
                var expectedOption = string.Format("<option value=\"{0}\">{1}</option>", provinceKey, StatesProvincesTerritories.Provinces[provinceKey]);
                Assert.That(stateDropDownListHtml, Contains.Substring(expectedOption));
            }
        }

        // this is a helper to new up an HtmlHelper for unit tests. A helper to create a helper to test helpers!
        HtmlHelper<T> CreateHtmlHelper<T>()
        {
            var viewData = new ViewDataDictionary();

            var mockControllerContext = Mock.Create<ControllerContext>(
                Mock.Create<HttpContextBase>(),
                new RouteData(),
                Mock.Create<ControllerBase>());

            var mockViewContext = Mock.Create<ViewContext>(
                mockControllerContext,
                Mock.Create<IView>(),
                viewData,
                new TempDataDictionary(),
                TextWriter.Null);

            var mockViewDataContainer = Mock.Create<IViewDataContainer>();

            Mock.Arrange(() => mockViewDataContainer.ViewData).Returns(viewData);

            return new HtmlHelper<T>(mockViewContext, mockViewDataContainer);
        }
    }
}