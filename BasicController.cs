﻿using Microsoft.AspNetCore.Mvc;
using NetCore.Simple.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Simple
{

    public abstract class BasicController<TModel> : CustomController<TModel, BasicModelHandler<TModel>>
        where TModel : IBasicModel, new()
    {



        //private string _ViewName;
        //protected string ViewName
        //{
        //    get { return _ViewName ?? "index"; }
        //    set { _ViewName = value; }
        //}

        //[NonAction]
        //protected TEditor GetEditor<TEditor>(TModel model)
        //    where TEditor : Editor, new()
        //{
        //    var editor = new TEditor
        //    {
        //        SubmitURL = ControllerContext.RouteData.Values["controller"].ToString()
        //    };
        //    if (model.Get())
        //    {
        //        editor.Data = model;
        //    }
        //    return editor;
        //}

        //[HttpGet]
        //public virtual IActionResult Index(TModel model)
        //{
        //    var editor = GetEditor<Editor>(model);
        //    return View(ViewName, editor);
        //}

        //[HttpPost]
        //[ValidateModel]
        //public virtual void Add(TModel model) => model.Add();

        //[HttpPut]
        //[ValidateModel]
        //public virtual void Update(TModel model) => model.Update();

        //[HttpDelete]
        //public virtual void Delete(TModel model) => model.Delete();

    }
}
