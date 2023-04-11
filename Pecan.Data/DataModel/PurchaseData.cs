using Dapper;
using MySql.Data.MySqlClient;
using Pecan.Entities;
using Pecan.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Data.DataModel
{
    internal class PurchaseData : IAddAList<PurchaseModel>, IModify<PurchaseModel>
    {
        public string Add(PurchaseModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "INSERT INTO Purchases(PurchaseDate, IdSupplier, Total)" +
                        " Values(@Purchase, @IdSupplier, @Total)";
                    var result = db.Execute(mySql,
                    new
                    {
                        PurchaseDate = model.PurchaseDate,
                        IdSupplier = model?.IdSupplier,
                        Total = model?.Total                        
                    });
                }
                return "Se guardo correctamente el producto nuevo";
            }
            catch (Exception)
            {
                return "No se pudo guardar el producto, Fijese que esten bien los Formatos de textos y numeros";
            }
        }

        public string Delete(PurchaseModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"DELETE FROM Purchases Where({model.Id} = Id)";
                    return "Se elimino correctamente";
                };
            }
            catch (Exception)
            {
                return "No se pudo eliminar";
            }
        }

        public IEnumerable<PurchaseModel> GetAll()
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT CommodityName, CodBar, CostPrice, PricePublic, IdStock FROM Purchases";
                    var result = db.Query<CommodityModel>(mySql);
                    return result.ToList();
                }
            }
            catch (Exception)
            {
                List<CommodityModel> _lstCommodity = new List<CommodityModel>();
                return _lstCommodity;
            }
        }

        public PurchaseModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public string Update(PurchaseModel model)
        {
            throw new NotImplementedException();
        }
    }
}
