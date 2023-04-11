using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Interface
{
    public interface IModify<T>
    {
        string Delete(T model);
        string Update(T model);
    }
}
