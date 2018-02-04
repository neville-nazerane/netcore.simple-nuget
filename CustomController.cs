using Microsoft.AspNetCore.Mvc;
using NetCore.Simple.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Simple
{

    [Route("[controller]/{id?}")]
    public abstract class CustomController<TModel, THandler> : Controller
        where THandler : IModelHandler<TModel>, new()
    {


        private string _ViewName;
        protected string ViewName
        {
            get { return _ViewName ?? "index"; }
            set { _ViewName = value; }
        }

        [NonAction]
        protected TEditor GetEditor<TEditor>(TModel model)
            where TEditor : Editor, new()
        {
            var editor = new TEditor
            {
                SubmitURL = ControllerContext.RouteData.Values["controller"].ToString()
            };
            if (new THandler().GetBasicModel(model).Get())
            {
                editor.Data = model;
            }
            return editor;
        }

        [HttpGet]
        public virtual IActionResult Index(TModel model)
        {
            var editor = GetEditor<Editor>(model);
            return View(ViewName, editor);
        }

        [HttpPost]
        [ValidateModel]
        public virtual void Add(TModel model) => new THandler().GetBasicModel(model).Add();

        [HttpPut]
        [ValidateModel]
        public virtual void Update(TModel model) => new THandler().GetBasicModel(model).Update();

        [HttpDelete]
        public virtual void Delete(TModel model) => new THandler().GetBasicModel(model).Delete();


    }
}
