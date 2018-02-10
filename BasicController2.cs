using NetCore.Simple.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Simple
{
    public class BasicController2<TModel> : AbstractController<TModel>
        where TModel : class, IBasicModel2<TModel>
    {

        protected override TModel Get(TModel model) => model.Get();

        protected override void OnAdd(TModel model) => model.Add();

        protected override bool OnDelete(TModel model) => model.Delete();

        protected override bool OnUpdate(TModel model) => model.Update();
    }
}
