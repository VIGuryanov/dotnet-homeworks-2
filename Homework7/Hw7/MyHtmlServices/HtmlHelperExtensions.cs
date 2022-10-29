using Hw7.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        var htmlContentBuilder = new HtmlContentBuilder();
        var model = helper.ViewData.Model;
        foreach (var property in helper.ViewData.ModelMetadata.ModelType.GetProperties())
            htmlContentBuilder.AppendHtml(GetHtmlFormInputString(property, model));
        return htmlContentBuilder;
    }

    static string GetHtmlFormInputString(PropertyInfo property, object? model)
    {
        var propertyName = property.Name;
        var strBuilder = new StringBuilder($"<p><label for=\"{propertyName}\">{GetHtmlLabel(property)}</label><br/>{ValidateProperty(property, model)}");
        var propertyType = property.PropertyType;
        if (propertyType.IsEnum)
        {
            strBuilder.Append($"<select name=\"{propertyName}\" id=\"{propertyName}\">");
            foreach (var num in property.PropertyType.GetEnumNames())
                strBuilder.Append($"<option value=\"{num}\">{num}</option>");
            strBuilder.Append("</select>");
        }
        else if (propertyType.Name == "Int32")
            strBuilder.Append(GetDefaultInputString(propertyName, "number"));
        else
            strBuilder.Append(GetDefaultInputString(propertyName, "text"));
        strBuilder.Append("</p>");
        return strBuilder.ToString();
    }

    static string GetDefaultInputString(string name, string type) => $"<input name=\"{name}\" id=\"{name}\" type=\"{type}\">";

    static string GetHtmlLabel(PropertyInfo property)
    {
        if (property.GetCustomAttributes(typeof(DisplayAttribute)).FirstOrDefault() is DisplayAttribute nameAttr)
            if (nameAttr.Name != null)
                return nameAttr.Name;
        return SplitCamelCase(property.Name);
    }

    static string SplitCamelCase(string line) => Regex.Replace(line, "(?<=.)(?=[A-Z])", " ");

    static string ValidateProperty(PropertyInfo property, object? model)
    {
        if (model != null && property.GetCustomAttributes(typeof(ValidationAttribute)) is IEnumerable<ValidationAttribute> propertyValidAttrs)
            foreach (var validAttr in propertyValidAttrs)
                if (!validAttr.IsValid(property.GetValue(model)))
                    return $"<span>{validAttr.ErrorMessage}</span>";
        return "";
    }
}