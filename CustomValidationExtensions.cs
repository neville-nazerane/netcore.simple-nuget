using Microsoft.AspNetCore.Mvc;
using NetCore.Simple.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Simple
{
    public static class CustomValidationExtensions
    {

        //static IActionResult ValidatedResult(this Controller Controller, object model, Func<IActionResult> Success,
        //                                                        IEnumerable<ErrorModel> additionalErrors,
        //                                                        Func<List<ErrorModel>> manualValidate)
        //{
        //    Controller.ModelState.Clear();
        //    Controller.TryValidateModel(model);
        //    if (Controller.ModelState.IsValid)
        //    {
        //        return Success();
        //    }
        //    return Controller.BadRequest(Controller.ModelState.Errors(additionalErrors));
        //}

        public static IActionResult ValidatedResult(this Controller Controller, object model, Action OnSuccess,
                                                                IEnumerable<ErrorModel> additionalErrors = null,
                                                                Func<List<ErrorModel>> manualValidate = null)
            => Controller.ValidatedResult(model, () => {
                OnSuccess();
                return Controller.Ok();
            }, additionalErrors, manualValidate);

        public static IActionResult ValidatedResult(this Controller Controller, object model, Func<object> OnSuccess,
                                                            IEnumerable<ErrorModel> additionalErrors = null,
                                                            Func<List<ErrorModel>> manualValidate = null)
            => Controller.ValidatedResult(model, () => Controller.Ok(OnSuccess()), additionalErrors, manualValidate);

        public static IActionResult ValidatedResult(this Controller Controller, object model, 
                                                                IEnumerable<ErrorModel> additionalErrors = null,
                                                                Func<List<ErrorModel>> manualValidate = null)
            => Controller.ValidatedResult(model, () => Controller.Ok(), additionalErrors, manualValidate);


        public static async Task<IActionResult> ValidatedResult(this Controller Controller, object model, 
                                    Func<Task<object>> OnSuccess, IEnumerable<ErrorModel> additionalErrors = null,
                                    Func<List<ErrorModel>> manualValidate = null)
        {
            Controller.ModelState.Clear();
            Controller.TryValidateModel(model);
            if (Controller.ModelState.IsValid)
            {
                var obj = await OnSuccess();
                return Controller.Ok(obj);
            }
            return Controller.BadRequest(Controller.ModelState.Errors(additionalErrors, manualValidate));
        }
        


    }
}
