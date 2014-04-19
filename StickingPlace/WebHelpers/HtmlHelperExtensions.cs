using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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

        public static MvcHtmlString StateDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            var stateList = StatesProvincesTerritories.StatesAndDC;
            return html.DropDownListFor(expression, new SelectList(stateList, "key", "value"), optionLabel, htmlAttributes);
        }
        
        public static MvcHtmlString ProvicesDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            var stateList = StatesProvincesTerritories.Provinces;
            return html.DropDownListFor(expression, new SelectList(stateList, "key", "value"), optionLabel, htmlAttributes);
        }
        public static MvcHtmlString TerritoriesDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            var stateList = StatesProvincesTerritories.Territories;
            return html.DropDownListFor(expression, new SelectList(stateList, "key", "value"), optionLabel, htmlAttributes);
        }
        public static MvcHtmlString StatesAndTerritoriesDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            var stateList = StatesProvincesTerritories.StatesAndDC;
            foreach (var territoryKey in StatesProvincesTerritories.Territories.Keys)
                stateList.Add(territoryKey, StatesProvincesTerritories.Territories[territoryKey]);
            return html.DropDownListFor(expression, new SelectList(stateList, "key", "value"), optionLabel, htmlAttributes);
        }
        public static MvcHtmlString StatesAndProvincesDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            var stateList = StatesProvincesTerritories.StatesAndDC;
            foreach (var provinceKey in StatesProvincesTerritories.Provinces.Keys)
                stateList.Add(provinceKey, StatesProvincesTerritories.Provinces[provinceKey]);
            return html.DropDownListFor(expression, new SelectList(stateList, "key", "value"), optionLabel, htmlAttributes);
        }
        public static MvcHtmlString StatesAndProvincesAndTerritoriesDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string optionLabel = null, object htmlAttributes = null)
        {
            var stateList = StatesProvincesTerritories.StatesAndDC;
            foreach (var provinceKey in StatesProvincesTerritories.Provinces.Keys)
                stateList.Add(provinceKey, StatesProvincesTerritories.Provinces[provinceKey]);
            foreach (var territoryKey in StatesProvincesTerritories.Territories.Keys)
                stateList.Add(territoryKey, StatesProvincesTerritories.Territories[territoryKey]);
            return html.DropDownListFor(expression, new SelectList(stateList, "key", "value"), optionLabel, htmlAttributes);
        }

    }
}