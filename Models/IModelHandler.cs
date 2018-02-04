using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Simple.Models
{

    public interface IModelHandler<TModel>
    {

        IBasicModel GetBasicModel(TModel model);

    }
}
