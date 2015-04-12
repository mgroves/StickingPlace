using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace StickingPlace
{
    public static class HttpExtensions
    {
        public static void SetAuthCookie<T>(this HttpResponseBase responseBase, string name, bool rememberMe, T userData) where T : class, new()
        {
            string jsonObj;
            if (userData != null)
            {
                var json = new JavaScriptSerializer();
                jsonObj = json.Serialize(userData);
            }
            else
                return;

            // create a default cookie and use its values to create a new one.
            var cookie = FormsAuthentication.GetAuthCookie(name, rememberMe);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);

            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                ticket.Expiration,
                ticket.IsPersistent, jsonObj, ticket.CookiePath);
            var encTicket = FormsAuthentication.Encrypt(newTicket);

            // reuse existing cookie
            cookie.Value = encTicket;

            responseBase.Cookies.Add(cookie);
        }

        public static T GetAuthCookie<T>(this HttpRequestBase requestBase) where T : class, new()
        {
            var cookie = requestBase.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
                return default(T);

            var decrypted = FormsAuthentication.Decrypt(cookie.Value);

            if (decrypted != null && !string.IsNullOrEmpty(decrypted.UserData) && decrypted.UserData != "null")
            {
                var json = new JavaScriptSerializer();
                return json.Deserialize<T>(decrypted.UserData);
            }
            return default(T);
        }
    }
}