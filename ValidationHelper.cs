using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCore.Simple
{

    interface IValidatableModel<TKey>
    {
        void Validate(Dictionary<TKey, string> ValidationErrors);
    }

    class ValidationHelper<TKey, TModel>
        where TModel : IValidatableModel<TKey>
    {

        //public string this[TKey Key]
        //{
        //    get
        //    {
        //        return ValidationErrors.GetValueOrDefault(Key);
        //    }
        //    set
        //    {
        //        ValidationErrors[Key] = value;
        //    }

        //}

        Dictionary<TKey, string> ValidationErrors;

        //void Validate()
        //{
        //    if (ValidationErrors == null)
        //    {
        //        ValidationErrors = new Dictionary<TKey, string>();

        //    }
        //}

        internal ValidationResult Validate(ValidationContext validationContext, TKey Key)
        {
            var model = (TModel)validationContext.ObjectInstance;
            if (ValidationErrors == null)
            {
                ValidationErrors = new Dictionary<TKey, string>();
                model.Validate(ValidationErrors);
            }
            string error = ValidationErrors[Key];
            if (error == null) return ValidationResult.Success;
            return new ValidationResult(error);
        }

    }

    abstract class CustomValidate : ValidationAttribute
    {
        protected CustomValidate()
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}
