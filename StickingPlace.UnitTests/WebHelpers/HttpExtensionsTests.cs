using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using NUnit.Framework;
using StickingPlace.WebHelpers;
using Telerik.JustMock;

namespace StickingPlace.UnitTests.WebHelpers
{
    [TestFixture]
    public class HttpExtensionsTests
    {
        [Test]
        public void You_can_put_an_arbitrary_object_into_the_auth_cookie()
        {
            // create an arbitrary object
            const string arbUsername = "aturing";
            const bool rememberMe = true;
            var arbObj = new MyArbitraryClass { UserId = 12, TenantId = 50, TenantName = "Foo, LLC." };

            // mock the http response, and make sure Cookies is ready to go
            var mockHttpResponse = Mock.Create<HttpResponseBase>();
            Mock.Arrange(() => mockHttpResponse.Cookies).Returns(new HttpCookieCollection());

            // set the auth cookie with arbitrary object
            mockHttpResponse.SetAuthCookie(arbUsername, rememberMe, arbObj);

            // it should now be in there!
            Assert.That(mockHttpResponse.Cookies.Count, Is.EqualTo(1));

            // pull it out with mirrored decoding to make sure it matches up
            var cookieArbObj = MyArbitraryClass.Decode(mockHttpResponse);

            Assert.That(arbObj.TenantId, Is.EqualTo(cookieArbObj.TenantId));
            Assert.That(arbObj.UserId, Is.EqualTo(cookieArbObj.UserId));
            Assert.That(arbObj.TenantName, Is.EqualTo(cookieArbObj.TenantName));
        }

        [Test]
        public void You_can_get_an_arbitrary_object_out_of_the_auth_cookie()
        {
            // create an arbitrary object
            const string arbUsername = "aturing";
            const bool rememberMe = true;
            var arbObj = new MyArbitraryClass { UserId = 12, TenantId = 50, TenantName = "Foo, LLC." };

            // mock the request base, and make sure Cookies is ready to go
            var mockHttpRequest = Mock.Create<HttpRequestBase>();
            Mock.Arrange(() => mockHttpRequest.Cookies).Returns(new HttpCookieCollection());

            // stick the object into the cookie, which ASP.NET would normally do for us
            MyArbitraryClass.Encode(mockHttpRequest, arbObj, arbUsername, rememberMe);

            // pull the cookie out using the helper extension
            var decodedArbObj = mockHttpRequest.GetAuthCookie<MyArbitraryClass>();

            // make sure it matches what we put in
            Assert.That(arbObj.TenantId, Is.EqualTo(decodedArbObj.TenantId));
            Assert.That(arbObj.UserId, Is.EqualTo(decodedArbObj.UserId));
            Assert.That(arbObj.TenantName, Is.EqualTo(decodedArbObj.TenantName));
        }

        [Test]
        public void If_theres_no_auth_cookie_then_GetAuthCookie_will_return_null()
        {
            // mock the request base, and make sure Cookies is ready to go
            var mockHttpRequest = Mock.Create<HttpRequestBase>();
            Mock.Arrange(() => mockHttpRequest.Cookies).Returns(new HttpCookieCollection());

            // pull the cookie out using the helper extension
            var decodedArbObj = mockHttpRequest.GetAuthCookie<MyArbitraryClass>();

            Assert.That(decodedArbObj, Is.Null);
        }
        
        [Test]
        public void If_a_null_cookie_is_put_in_for_some_reason_GetAuthCookie_will_return_null()
        {
            // create an arbitrary object
            const string arbUsername = "aturing";
            const bool rememberMe = true;

            // mock the request base, and make sure Cookies is ready to go
            var mockHttpRequest = Mock.Create<HttpRequestBase>();
            Mock.Arrange(() => mockHttpRequest.Cookies).Returns(new HttpCookieCollection());

            // stick the object into the cookie, which ASP.NET would normally do for us
            MyArbitraryClass.Encode(mockHttpRequest, null, arbUsername, rememberMe);

            // pull the cookie out using the helper extension
            var decodedArbObj = mockHttpRequest.GetAuthCookie<MyArbitraryClass>();

            Assert.That(decodedArbObj, Is.Null);
        }

        public class MyArbitraryClass
        {
            public int UserId { get; set; }
            public int TenantId { get; set; }
            public string TenantName { get; set; }

            // the code in Decode/Encode methods is essentially a mirror of the extensions under test
            // but that "mirror" is not necessary in real code, just for unit testing
            public static MyArbitraryClass Decode(HttpResponseBase mockHttpResponse)
            {
                var json = new JavaScriptSerializer();
                var cookie = mockHttpResponse.Cookies[FormsAuthentication.FormsCookieName];

                if (cookie == null)
                    return null;

                var decrypted = FormsAuthentication.Decrypt(cookie.Value);

                if (decrypted != null && !string.IsNullOrEmpty(decrypted.UserData))
                    return json.Deserialize<MyArbitraryClass>(decrypted.UserData);
                return null;
            }

            public static void Encode(HttpRequestBase request, MyArbitraryClass arbObj, string username, bool rememberMe)
            {
                var cookie = FormsAuthentication.GetAuthCookie(username, rememberMe);
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var json = new JavaScriptSerializer();

                string jsonObj;
                if (arbObj != null)
                    jsonObj = json.Serialize(arbObj);
                else
                    return;

                var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration,
                    ticket.IsPersistent, jsonObj, ticket.CookiePath);
                var encTicket = FormsAuthentication.Encrypt(newTicket);

                cookie.Value = encTicket;

                request.Cookies.Add(cookie);
            }
        }    
    }
}