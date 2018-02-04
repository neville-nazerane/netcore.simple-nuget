using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NetCore.Simple.Core;

namespace NetCore.Simple
{
    public static class ModelStateExtensions
    {

        public static IEnumerable<ErrorModel> Errors(this ModelStateDictionary ModelState)
        {
            return ModelState.Select(ms =>
                new ErrorModel()
                {
                    Key = ms.Key,
                    Errors = ms.Value.Errors.Select(e => e.ErrorMessage)
                }
            );
        }

        //public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        //{
        //    var attrType = typeof(T);
        //    var property = instance.GetType().GetProperty(propertyName);
        //    return (T)property.GetCustomAttributes(attrType, false).First();
        //}


    }
}
