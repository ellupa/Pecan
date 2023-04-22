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
    public class SaleManager : ICrd<SaleModel>, IModify<SaleModel>
    {
        SaleData saleData = new SaleData();
        public string Add(SaleModel model)
        {
            return saleData.Add(model);
        }

        public string Delete(SaleModel model)
        {
            return saleData.Delete(model);
        }

        public IEnumerable<SaleModel> GetAll()
        {
            var result = saleData.GetAll();
            if (result.Any())
                return result;
            else
                throw new Exception("No hay ventas cargadas");
        }

        public SaleModel GetById(int id)
        {
            var result = saleData.GetById(id);
            if (result != null)
                return result;
            else
                throw new Exception("No se encontro la venta");
        }

        public string Update(SaleModel model)
        {
            return saleData.Update(model);
        }
    }
}
