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
    public class StockManager : ICrd<StockModel>, IModify<StockModel>
    {
        StockData stockData = new StockData();
        public string Add(StockModel model)
        {
            return stockData.Add(model); 
        }

        public string Delete(StockModel model)
        {
            return stockData.Delete(model);
        }

        public IEnumerable<StockModel> GetAll()
        {
            return stockData.GetAll();
        }

        public StockModel GetById(int id)
        {
            return stockData.GetById(id);
        }

        public string Update(StockModel model)
        {
            return stockData.Update(model);
        }
    }
}
