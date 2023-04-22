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
    public class PurchaseManagers 
    {
        private PurchaseData data = new PurchaseData();
        public string Add(PurchaseModel model)
        {
            return data.Add(model);
        }

        public string Delete(PurchaseModel model)
        {
            return data.Delete(model);
        }

        public IEnumerable<PurchasesXCommoditiesModel> GetAll()
        {
            var result = data.GetAll();
            if (result.Any())
                return result;
            else
                throw new Exception("No hay compras cargadas");
        }

        public IEnumerable<PurchasesXCommoditiesModel> GetById(int id)
        {
            var result = data.GetById(id);
            if (result != null)
                return result;
            else
                throw new Exception("No se encontro los detalles de compra");
        }        
    }
}
