using Microsoft.AspNetCore.Mvc;
using NetCore.Simple.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Simple
{
    public abstract class AbstractController<TModel> : Controller
    {


        private string _ViewName;
        protected string ViewName
        {
            get { return _ViewName ?? "index"; }
            set { _ViewName = value; }
        }

        [NonAction]
        protected virtual TEditor GetEditor<TEditor>(TModel model)
            where TEditor : Editor, new()
            => new TEditor
            {
                SubmitURL = ControllerContext.RouteData.Values["controller"].ToString(),
                Data = Get(model)
            };

        [HttpGet]
        public virtual IActionResult Index(TModel model)
        {
            var editor = GetEditor<Editor>(model);
            return View(ViewName, editor);
        }

        [HttpPost]
        [ValidateModel]
        public virtual void Add(TModel model) => OnAdd(model);

        [HttpPut]
        [ValidateModel]
        public virtual void Update(TModel model) => OnUpdate(model);

        [HttpDelete]
        public virtual void Delete(TModel model) => OnDelete(model);

        [NonAction]
        protected abstract void OnAdd(TModel model);

        [NonAction]
        protected abstract bool OnUpdate(TModel model);

        [NonAction]
        protected abstract bool OnDelete(TModel model);

        [NonAction]
        protected abstract TModel Get(TModel model);

    }
}
