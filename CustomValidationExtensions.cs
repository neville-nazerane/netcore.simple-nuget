using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Simple
{
    public static class CustomValidationExtensions
    {

        static IActionResult ValidatedResult(this Controller Controller, object model, Func<IActionResult> Success)
        {
            Controller.ModelState.Clear();
            Controller.TryValidateModel(model);
            if (Controller.ModelState.IsValid)
            {
                return Success();
            }
            return Controller.BadRequest(Controller.ModelState.Errors());
        }

        public static IActionResult ValidatedResult(this Controller Controller, object model, Action OnSuccess)
            => Controller.ValidatedResult(model, () => {
                OnSuccess();
                return Controller.Ok();
            });

        public static IActionResult ValidatedResult(this Controller Controller, object model, Func<object> OnSuccess)
            => Controller.ValidatedResult(model, () => Controller.Ok(OnSuccess()));

        public static IActionResult ValidatedResult(this Controller Controller, object model)
            => Controller.ValidatedResult(model, () => Controller.Ok());


        public static async Task<IActionResult> ValidatedResult(this Controller Controller, object model, Func<Task<object>> OnSuccess)
        {
            Controller.ModelState.Clear();
            Controller.TryValidateModel(model);
            if (Controller.ModelState.IsValid)
            {
                var obj = await OnSuccess();
                return Controller.Ok(obj);
            }
            return Controller.BadRequest(Controller.ModelState.Errors());
        }
        


    }
}
