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
                        IdSupplier = model?.Supplier?.Id,
                        Total = model?.Total                        
                    });
                }
                return "Se guardo correctamente la venta nuevo";
            }
            catch (Exception)
            {
                return "No se pudo guardar la venta, Fijese que esten bien los Formatos de textos y numeros";
            }
        }

        public string Delete(PurchaseModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"DELETE FROM PurchasesXCommodities Where({model.Id} = Id)";
                    var result = db.Execute(mySql);
                    mySql = $"DELETE FROM Purchases Where({model.Id} = Id)";
                    result = db.Execute(mySql);
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
                    var mySql = "SELECT Purchases.*, Suppliers.* FROM Purchases" +
                        "INNER JOIN Suppliers ON Suppliers.Id = Purchases.IdStock";
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
        }

        public PurchaseModel GetById(int id)
        {
            try
            {            
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT Purchases.*, Suppliers.* FROM Purchases" +
                        "INNER JOIN Suppliers ON Suppliers.Id = Purchases.IdStock" +
                        $"WHERE Purchases.Id = {id}";
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
                    ).FirstOrDefault();
                    if (results != null)                
                        return results;
                    return new PurchaseModel();
                }
            }
            catch (Exception)
            {

                return new PurchaseModel();
            }


        }
        public string Update(PurchaseModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {                                        
                   var mySql = $"UPDATE Purchases SET PurchaseDate={model?.PurchaseDate}," +
                        $"IdSupplier={model?.Supplier?.Id}, Total = {model?.Total}, " +
                        $"FROM Purchase WHERE (Id = {model?.Id})";
                    var resultCommodity = db.Query<PurchaseModel>(mySql);
                    return "Se actulizo correctamente";
                }
            }
            catch (Exception)
            {
                return "No se pudo Actualizar";
            }
        }
    }
}
