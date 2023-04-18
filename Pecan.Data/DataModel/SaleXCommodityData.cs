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
    public class SaleXCommodityData : ICrd<SaleXCommodityModel>
    {
        public string Add(SaleXCommodityModel saleXCommodity)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"INSERT INTO SalesXCommodities(QuantityOfproducts, Sales, Commodities)Values('{saleXCommodity.QuantityOfProduct}','{saleXCommodity.Sales}','{saleXCommodity.Commodities}')";
                    db.Execute(mySql);
                }
                return "Se guardo correctamente la venta";
            }
            catch (Exception)
            {
                return "No se pudo guardar la venta, Fijese que esten bien ingresado";
            }
        }

        public string Delete(SaleXCommodityModel saleXCommodity)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"DELETE FROM SalesXCommodities WHERE Id = {saleXCommodity.Id}";
                    var rowsAffected = db.Execute(mySql);
                    if (rowsAffected > 0)
                    {
                        return "Venta eliminada correctamente";
                    }
                    else
                    {
                        return "No se encontró ninguna venta para eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrió un error al eliminar la venta.";
            }
        }

        public IEnumerable<SaleXCommodityModel> GetAll()
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = @"SELECT Stock.*, Sales.*, Commodities.*, SalesXCommodities.*
                        FROM SalesXCommodities
                        INNER JOIN Sales ON Sales.Id = SalesXCommodities.IdSales
                        INNER JOIN Commodities ON SalesXCommodities.IdCommodities = Commodities.Id
                        INNER JOIN Stock ON Commodities.IdStock = Stock.Id";

                    var results = db.Query< SaleXCommodityModel, SaleModel, CommodityModel, StockModel, SaleXCommodityModel>(mySql,
                        (saleXCommodity, sale, commodity, stock ) =>
                        {
                            saleXCommodity.Sales = sale;
                            if ( commodity != null )
                            {
                                saleXCommodity.Commodities = commodity;
                            }

                            return saleXCommodity;
                        },
                        splitOn: "Id, Id, Id"
                    ).ToList();

                    return results;
                }
            }
            catch (Exception ex)
            {
                List<SaleXCommodityModel> result = new List<SaleXCommodityModel>();
                return result;
            }
        }


        public SaleXCommodityModel GetById(int id)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $@"SELECT Stock.*, Sales.*, Commodities.*, SalesXCommodities.*
                        FROM SalesXCommodities
                        INNER JOIN Sales ON Sales.Id = SalesXCommodities.IdSales
                        INNER JOIN Commodities ON SalesXCommodities.IdCommodities = Commodities.Id
                        INNER JOIN Stock ON Commodities.IdStock = Stock.Id
                        WHERE SalesXCommodities.Id = {id}";

                    var results = db.Query<SaleXCommodityModel, SaleModel, CommodityModel, StockModel, SaleXCommodityModel>(mySql,
                        (saleXCommodity, sale, commodity, stock) =>
                        {
                            saleXCommodity.Sales = sale;
                            if (commodity != null)
                            {
                                saleXCommodity.Commodities = commodity;
                            }
                            return saleXCommodity;
                        },
                        splitOn: "Id, Id, Id"
                    ).FirstOrDefault();
                    if (results != null)
                        return results;
                    return results = new SaleXCommodityModel();
                }
            }
            catch (Exception ex)
            {
                SaleXCommodityModel result = new SaleXCommodityModel();
                return result;
            }
        }
    }
}

