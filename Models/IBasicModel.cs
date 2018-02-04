using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.Simple.Models
{
    public interface IBasicModel
    {

        void Add();

        bool Update();

        bool Get();

        bool Delete();

    }
}
