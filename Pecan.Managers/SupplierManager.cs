using Pecan.Data.DataModel;
using Pecan.Entities;
using Pecan.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Managers
{
    public class SupplierManager : ICrd<SupplierModel>, IModify<SupplierModel>
    {
        SupplierData supplierData = new SupplierData();
        public string Add(SupplierModel model)
        {
            return supplierData.Add(model);
        }

        public string Delete(SupplierModel model)
        {
            return supplierData.Delete(model);
        }

        public IEnumerable<SupplierModel> GetAll()
        {
            return supplierData.GetAll();
        }

        public SupplierModel GetById(int id)
        {
            return supplierData.GetById(id);
        }

        public string Update(SupplierModel model)
        {
            return supplierData.Update(model);
        }
    }
}
