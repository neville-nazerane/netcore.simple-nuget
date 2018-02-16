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

        static bool Revalidate(this Controller controller, object model)
        {
            controller.ModelState.Clear();
            controller.TryValidateModel(model);
            return controller.ModelState.IsValid;
        }
        
        /// <summary>
        /// For void call back or no call back
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="OnSuccess">callback</param>
        /// <param name="additionalErrors"></param>
        /// <param name="manualValidate"></param>
        /// <returns></returns>
        public static IActionResult ValidatedResult(this Controller controller, object model, Action OnSuccess = null, 
                                    IEnumerable<ErrorModel> additionalErrors = null,
                                    Func<List<ErrorModel>> manualValidate = null)
        {
            if (controller.Revalidate(model))
            {
                OnSuccess?.Invoke();
                return controller.Ok();
            }
            else return controller.BadRequest(controller.ModelState.Errors(additionalErrors, manualValidate));
        }

        /// <summary>
        /// Call back to directly return the validation result
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="OnSuccess"></param>
        /// <param name="additionalErrors"></param>
        /// <param name="manualValidate"></param>
        /// <returns></returns>
        public static IActionResult ValidatedResult(this Controller controller, object model,
                                    Func<IActionResult> OnSuccess, IEnumerable<ErrorModel> additionalErrors = null,
                                    Func<List<ErrorModel>> manualValidate = null)
        {

            if (controller.Revalidate(model)) return OnSuccess();
            return controller.BadRequest(controller.ModelState.Errors(additionalErrors, manualValidate));
        }

        /// <summary>
        /// Call back to return any object (will return Ok(obj))
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="OnSuccess"></param>
        /// <param name="additionalErrors"></param>
        /// <param name="manualValidate"></param>
        /// <returns></returns>
        public static IActionResult ValidatedResult(this Controller controller, object model,
                                    Func<object> OnSuccess, IEnumerable<ErrorModel> additionalErrors = null,
                                    Func<List<ErrorModel>> manualValidate = null)
                => controller.ValidatedResult(model, () => controller.Ok(OnSuccess()), additionalErrors, manualValidate);

        /// <summary>
        /// async call back with direct action result
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="OnSuccess"></param>
        /// <param name="additionalErrors"></param>
        /// <param name="manualValidate"></param>
        /// <returns></returns>
        public static async Task<IActionResult> ValidateResultAsync(this Controller controller, object model,
                                    Func<Task<IActionResult>> OnSuccess, IEnumerable<ErrorModel> additionalErrors = null,
                                    Func<List<ErrorModel>> manualValidate = null)
        {
            if (controller.Revalidate(model)) return await OnSuccess();
            return controller.BadRequest(controller.ModelState.Errors(additionalErrors, manualValidate));
        }

        /// <summary>
        /// Async call back with any object
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="model"></param>
        /// <param name="OnSuccess"></param>
        /// <param name="additionalErrors"></param>
        /// <param name="manualValidate"></param>
        /// <returns></returns>
        public static async Task<IActionResult> ValidateResultAsync(this Controller controller, object model,
                                    Func<Task<object>> OnSuccess, IEnumerable<ErrorModel> additionalErrors = null,
                                    Func<List<ErrorModel>> manualValidate = null)
            => await controller.ValidateResultAsync(model, async () => await OnSuccess(), additionalErrors, manualValidate);


    }
}
