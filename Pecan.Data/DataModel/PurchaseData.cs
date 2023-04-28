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
    public class PurchaseData : IModify<PurchaseModel>
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

        public IEnumerable<PurchasesXCommoditiesModel> GetAll()
        {

            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT px.*, p.Id, p.PurchaseDate, p.Total, s.*, c.Id, c.CommodityName, c.CodBar, c.CostPrice, c.PricePublic, st.* " +
                                "FROM PurchasesXCommodities px " +
                                "INNER JOIN Purchases p ON p.Id = px.IdPurchase " +
                                "INNER JOIN Suppliers s ON s.Id = p.IdSupplier " +
                                "INNER JOIN Commodities c ON c.Id = px.IdCommodities " +
                                "INNER JOIN Stock st ON st.Id = c.IdStock";
                    var results = db.Query<PurchasesXCommoditiesModel, PurchaseModel, SupplierModel, CommodityModel, StockModel, PurchasesXCommoditiesModel>(
                        mySql,
                        (px, p, s, c, st) =>
                        {
                            px.Purchase = p;
                            px.Commodity = c;
                            if (p != null)
                            {
                                p.Supplier = s;
                            }
                            if (c != null)
                            {
                                c.Stock = st;
                            }
                            return px;
                        },
                        splitOn: "Id, Id, Id, Id"
                    );
                    return results.ToList();
                }
            }
            catch (Exception)
            {
                return new List<PurchasesXCommoditiesModel>();
            }
        }

        //public List<PurchaseModel> GetAll()
        //{
        //    using (var connection = new MySqlConnection(PecanContext.ConnectionString()))
        //    {
        //        string sql = @"
        //            SELECT p.*, c.*
        //            FROM Purchases p
        //            INNER JOIN PurchasesXCommodities cxp ON cxp.IdPurchase = Purchases.Id
        //            INNER JOIN Productos p ON p.Id = cxp.IdCommodities";

        //        var comprasDict = new Dictionary<int, PurchaseModel>();

        //        var compras = connection.Query<PurchaseModel, CommodityModel, PurchaseModel>(
        //            sql,
        //            (compra, commodity) =>
        //            {
        //                PurchaseModel compraEntry;

        //                if (!comprasDict.TryGetValue(compra.Id, out compraEntry))
        //                {
        //                    compraEntry = compra;
        //                    compraEntry.PurchasesXCommodities = new List<PurchasesXCommoditiesModel>();
        //                    comprasDict.Add(compraEntry.Id, compraEntry);
        //                }

        //                compraEntry.PurchasesXCommodities.Add(commodity);
        //                return compraEntry;
        //            },
        //            splitOn: "ProductoId"
        //        ).Distinct().ToList();

        //        return compras;
        //    }
        //}


        public IEnumerable<PurchasesXCommoditiesModel> GetById(int id)
        {
            try
            {

                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT px.*, p.Id, p.PurchaseDate, p.Total, s.*, c.Id, c.CommodityName, c.CodBar, c.CostPrice, c.PricePublic, st.* " +
                                "FROM PurchasesXCommodities px " +
                                "INNER JOIN Purchases p ON p.Id = px.IdPurchase " +
                                "INNER JOIN Suppliers s ON s.Id = p.IdSupplier " +
                                "INNER JOIN Commodities c ON c.Id = px.IdCommodities " +
                                "INNER JOIN Stock st ON st.Id = c.IdStock" +
                                $"WHERE PurchasesXCommodities.Id = {id}";
                    var results = db.Query<PurchasesXCommoditiesModel, PurchaseModel, SupplierModel, CommodityModel, StockModel, PurchasesXCommoditiesModel>(
                        mySql,
                        (px, p, s, c, st) =>
                        {
                            px.Purchase = p;
                            px.Commodity = c;
                            if (p != null)
                            {
                                p.Supplier = s;
                            }
                            if (c != null)
                            {
                                c.Stock = st;
                            }
                            return px;
                        },
                        splitOn: "Id, Id, Id, Id"
                    );
                    return results.ToList();
                }
            }catch (Exception)
            {
                return new List<PurchasesXCommoditiesModel>();
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
