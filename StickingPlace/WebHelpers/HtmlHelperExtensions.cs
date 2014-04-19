using System.Web.Mvc;

namespace StickingPlace.WebHelpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString IsSelected(this HtmlHelper @this, string controllerName, string cssSelectedClass = "active")
        {
            string currentControllerName = @this.ViewContext.RouteData.Values["controller"].ToString();

            if (currentControllerName == controllerName)
                return MvcHtmlString.Create(" class=\"" + cssSelectedClass + "\"");
            return MvcHtmlString.Empty;
        }         
    }
}