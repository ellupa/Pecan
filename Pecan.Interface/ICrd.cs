using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Interface
{
    public interface ICrd<T>
    {
       string Add(T model);
       IEnumerable<T> GetAll();
       T GetById(int id);
       string Delete(T model);
    }
}
