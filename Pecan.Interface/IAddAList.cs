using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Interface
{
    public interface IAddAList<T>
    {
       string Add(T model);
       IEnumerable<T> GetAll();
       T GetById(int id);

    }
}
