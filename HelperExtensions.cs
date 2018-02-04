using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NetCore.Simple
{

    public static class HelperExtensions
    {

        public static string IfEmpty(this string str, string alternate)
        {
            return str == string.Empty ? str : alternate;
        }

        public static InputGenerator<T> GetInputGenerator<T>(this IHtmlHelper htmlHelper, string Template = null)
            where T : new()
        {
            var im = new InputGenerator<T>((t, i) => htmlHelper.Partial(t, i));
            if (Template != null)
                im.Template = Template;
            return im;
        }

        public static InputGenerator<T> GetInputGenerator<T>(this IHtmlHelper<T> htmlHelper, string Template = null)
            where T : new()
        {
            var im = new InputGenerator<T>((t, i) => htmlHelper.Partial(t, i));
            if (Template != null)
                im.Template = Template;
            return im;
        }

        public static IHtmlContent toJS(this IHtmlHelper htmlHelper, object obj)
        {
            return htmlHelper.Raw(JsonConvert.SerializeObject(obj));
        }

        public static string FormData(this IHtmlHelper Html, object Value)
        {

            var key = Guid.NewGuid().ToString("N");
            Dictionary<string, object> dictionary;
            if (Html.TempData["simpleFormData"] == null)
            {
                Html.TempData["bats"] = "Batman";
                Html.ViewContext.ViewData["pancrea"] = "batman";
                Html.ViewData["stringy"] = "batman";
                dictionary = new Dictionary<string, object>();
                Html.TempData["simpleFormData"] = dictionary;
            }
            else
                dictionary = (Dictionary <string, object>)Html.TempData["simpleFormData"];
            dictionary.Add(key, Value);
            return key;
        }

        public static IHtmlContent SimpleScripts(this IHtmlHelper Html)
        {
            string Scripts = string.Empty;

            if (Html.TempData["simpleFormData"] != null)
            {
                Dictionary<string, object> dictionary = (Dictionary<string, object>) Html.TempData["simpleFormData"];
                Scripts += string.Concat(dictionary.Select(kv => $"addFormData('{kv.Key}', {Html.toJS(kv.Value)});"));
            }

            return Html.Raw($"<script>{Scripts}</script>");
        }

        //private class Helper<TModel>
        //    where TModel : new()
        //{
        //    public string Fetch<T>(Expression<Func<TModel, T>> lamda)
        //    {
        //        return ((MemberExpression)lamda.Body).Member.Name;
        //    }
        //}

    }
}
