using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Simple.Models
{
    
    public class BasicModelHandler<TModel> : IModelHandler<TModel>
        where TModel : IBasicModel
    {
        public IBasicModel GetBasicModel(TModel model) => model;

    }
}
