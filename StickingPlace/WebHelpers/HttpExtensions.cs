using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace StickingPlace.WebHelpers
{
    public static class HttpExtensions
    {
        public static void SetAuthCookie<T>(this HttpResponseBase responseBase, string name, bool rememberMe, T userData)
        {
            // create a default cookie and use its values to create a new one.
            var cookie = FormsAuthentication.GetAuthCookie(name, rememberMe);
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var json = new JavaScriptSerializer();

            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate,
                ticket.Expiration,
                ticket.IsPersistent, json.Serialize(userData), ticket.CookiePath);
            var encTicket = FormsAuthentication.Encrypt(newTicket);

            // reuse existing cookie
            cookie.Value = encTicket;

            responseBase.Cookies.Add(cookie);
        }

        public static T GetAuthCookie<T>(this HttpRequestBase requestBase)
        {
            var json = new JavaScriptSerializer();
            var cookie = requestBase.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie == null)
                return default(T);

            var decrypted = FormsAuthentication.Decrypt(cookie.Value);

            if (decrypted != null && !string.IsNullOrEmpty(decrypted.UserData))
                return json.Deserialize<T>(decrypted.UserData);
            return default(T);
        }
    }
}