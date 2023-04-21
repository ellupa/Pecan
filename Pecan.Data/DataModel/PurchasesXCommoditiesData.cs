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
    public class PurchasesXCommoditiesData : ICrd<PurchasesXCommoditiesModel>
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

        /* public IEnumerable<PurchasesXCommoditiesModel> GetAll()
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT PurchasesXCommodities.*, Purchases.*, Suppliers.*, Commodities.*, Stock.* FROM PurchasesXCommodities" +                        
                        "INNER JOIN Purchases ON Purchases.Id = PurchasesXCommodities.IdPurchase" +
                        "INNER JOIN Suppliers ON Suppliers.Id = Purchases.IdSupplier" +
                        "INNER JOIN Commodities ON Commodities.Id = PurchasesXCommodities.IdCommodities" +
                        "INNER JOIN Stock ON Stock.Id = Commodities.IdStock";
                    var results = db.Query<PurchasesXCommoditiesModel, PurchaseModel, CommodityModel, StockModel ,SupplierModel, PurchasesXCommoditiesModel>(mySql,
                        (purchaseXCommodities,purchase, commodity, stock, supplier) =>
                        {
                            PurchasesXCommoditiesModel purchaseDetails = new PurchasesXCommoditiesModel
                            {
                                Id = purchaseXCommodities.Id,

                               Purchase = new PurchaseModel()
                               {
                                    Id = purchase.Id,
                                    PurchaseDate = purchase.PurchaseDate,
                                    Total = purchase.Total,
                                    Supplier = supplier
                               },                               
                               Commodity = new CommodityModel()
                               {
                                    CommodityName = commodity.CommodityName,
                                    Id= commodity.Id,
                                    CodBar = commodity.CodBar,
                                    CostPrice = commodity.CostPrice,
                                    PricePublic = commodity.PricePublic,
                                    Stock = stock                                    
                               },
                               Purchase = purchaseXCommodities.Purchase,
                               Commodity = purchaseXCommodities.Commodity,
                               if (commodity != null)
                                Commodity.Stock = stock,
                               QuantityOfProducts = purchaseXCommodities.QuantityOfProducts,
                            };
                            return purchaseDetails;
                        },
                        splitOn: "Id, Id"
                    ) ;
                    return results.ToList();

                }
            }
            catch (Exception)
            {
                List<PurchasesXCommoditiesModel> _lstPurchase = new List<PurchasesXCommoditiesModel>();
                return _lstPurchase;
            }         
        } */

        public IEnumerable<PurchasesXCommoditiesModel> GetAll()
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT px.*, p.Id, p.PurchaseDate, p.Total, s.*, c.Id, c.CommodityName, c.CodBar, c.CostPrice, c.PricePublic, st.* " +
                                "FROM PurchasesXCommodities px " +
                                "LEFT JOIN Purchases p ON p.Id = px.IdPurchase " +
                                "LEFT JOIN Suppliers s ON s.Id = p.IdSupplier " +
                                "LEFT JOIN Commodities c ON c.Id = px.IdCommodities " +
                                "LEFT JOIN Stock st ON st.Id = c.IdStock";
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

        public PurchasesXCommoditiesModel GetById(int id)
        {

            using (var db = new MySqlConnection(PecanContext.ConnectionString()))
            {
                var mySql = "SELECT px.*, p.Id, p.PurchaseDate, p.Total, s.*, c.Id, c.CommodityName, c.CodBar, c.CostPrice, c.PricePublic, st.* " +
                            "FROM PurchasesXCommodities px " +
                            "LEFT JOIN Purchases p ON p.Id = px.IdPurchase " +
                            "LEFT JOIN Suppliers s ON s.Id = p.IdSupplier " +
                            "LEFT JOIN Commodities c ON c.Id = px.IdCommodities " +
                            "LEFT JOIN Stock st ON st.Id = c.IdStock" +
                            $"WHERE PurchasesXCommodities.Id = {id}";
                var results = (PurchasesXCommoditiesModel)db.Query<PurchasesXCommoditiesModel, PurchaseModel, SupplierModel, CommodityModel, StockModel, PurchasesXCommoditiesModel>(
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
                return results;
            }            
        }
        
        public IEnumerable<PurchasesXCommoditiesModel> GetByPurchaseId(int id)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT px.*, p.Id, p.PurchaseDate, p.Total, s.*, c.Id, c.CommodityName, c.CodBar, c.CostPrice, c.PricePublic, st.* " +
                                "FROM PurchasesXCommodities px " +
                                "LEFT JOIN Purchases p ON p.Id = px.IdPurchase " +
                                "LEFT JOIN Suppliers s ON s.Id = p.IdSupplier " +
                                "LEFT JOIN Commodities c ON c.Id = px.IdCommodities " +
                                "LEFT JOIN Stock st ON st.Id = c.IdStock" +
                                $"WHERE PurchasesXCommodities.IdPurchase = {id}";
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
    }
}
