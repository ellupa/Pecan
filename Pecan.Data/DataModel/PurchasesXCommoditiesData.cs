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
    public class PurchasesXCommoditiesData : IAddAList<PurchasesXCommoditiesModel>, IModify<PurchasesXCommoditiesModel>
    {
        public string Add(PurchasesXCommoditiesModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "INSERT INTO PurchasesXCommodities(IdPurchase, IdCommodity, QuantityOfProducts)" +
                        " Values(@IdPurchase, @IdCommodity, @QuantityOfProducts)";
                    var result = db.Execute(mySql,
                    new
                    {
                        IdPurchase = model?.Purchase?.Id,
                        IdCommodity = model?.Commodity?.Id,
                        QuantityOfProducts = model?.QuantityOfProducts
                    });
                }
                return "Se guardo correctamente la compra por producto nuevo";
            }
            catch (Exception)
            {
                return "No se pudo guardar la compra por producto, Fijese que esten bien los Formatos de textos y numeros";
            }
        }

        public string Delete(PurchasesXCommoditiesModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"DELETE FROM PurchasesXCommodities Where({model.Id} = Id)";
                    var result = db.Execute(mySql);                    
                    return "Se elimino correctamente";
                };
            }
            catch (Exception)
            {
                return "No se pudo eliminar";
            }
        }

        public IEnumerable<PurchasesXCommoditiesModel> GetAll()
        {
            /*try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT Purchases.*, Commodities.*, Stock.* FROM PurchasesXCommoditiesModel" +
                        "INNER JOIN Suppliers ON Suppliers.Id = Purchases.IdStock" +
                        "INNER JOIN Purchases ON Su";
                    var results = db.Query<PurchaseModel, SupplierModel, PurchaseModel>(mySql,
                        (purchase, supplier) =>
                        {
                            PurchaseModel purchaseDetails = new PurchaseModel
                            {
                                Id = purchase.Id,
                                PurchaseDate = purchase.PurchaseDate,
                                Supplier = supplier,
                                Total = purchase.Total
                            };
                            return purchaseDetails;
                        },
                        splitOn: "Id, Id"
                    );
                    return results.ToList();

                }
            }
            catch (Exception)
            {
                List<PurchaseModel> _lstPurchase = new List<PurchaseModel>();
                return _lstPurchase;
            }
            */

            throw new NotImplementedException();
        }

        public PurchasesXCommoditiesModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public string Update(PurchasesXCommoditiesModel model)
        {
            throw new NotImplementedException();
        }
    }
}
