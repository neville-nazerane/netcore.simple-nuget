using Microsoft.AspNetCore.Html;
using NetCore.Simple.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Linq;

using TypeEnum = System.ComponentModel.DataAnnotations.DataType;

namespace NetCore.Simple
{
    public class InputGenerator<TModel>
            where TModel : new()
    {

        private const string _DefaultTemplate = "NetCore.Simple/_Input";

        public delegate IHtmlContent generator(string template, InputInfo info);

        private generator Generate;

        public string Template { get; set; }
        
        internal InputGenerator(generator Generate)
        {
            this.Generate = Generate;
            Template = _DefaultTemplate;
        }

        public static implicit operator Func<string>(InputGenerator<TModel> im)
        {
            return () => im.Template;
        }

        public InputGenerator()
        {
            
        }
        
        public IHtmlContent Make<T>(Expression<Func<TModel, T>> lamda, string Label = null, string Template = null,
                        string DataType = null, bool? IsRequired = null)
        {

            var member = ((MemberExpression)lamda.Body).Member;
            var template = Template ?? this.Template;
            if (Label == null)
            {
                var display = getAttribute(member, "DisplayAttribute");
                if (display != null) Label =
                        display.NamedArguments.Where(a => a.MemberName == "Name")
                        .First().TypedValue.Value.ToString();
            }
            if (DataType == null)
            {
                var dType = getAttribute(member, "DataTypeAttribute");

                if (dType != null)
                    DataType =
                        ((TypeEnum)dType.ConstructorArguments.Where(a => a.ArgumentType.Name == "DataType")
                        .First().Value).ToString();
            }
            if (IsRequired == null)
                IsRequired = getAttribute(member, "RequiredAttribute") == null;

            return Generate(template, new InputInfo()
            {
                Name = member.Name,
                DataType = DataType,
                Label = Label,
                IsRequired = (bool)IsRequired
            });
        }

        private CustomAttributeData getAttribute(MemberInfo member, string attr)
        {
            return member.CustomAttributes.Where(c => c.AttributeType.Name == attr).FirstOrDefault();
        }

    }
}
