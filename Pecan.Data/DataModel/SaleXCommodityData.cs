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
    public class SaleXCommodityData : IAddAList<SaleXCommodityModel>, IModify<SaleXCommodityModel>
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
                    var mySql = @"SELECT Sales.*, Commodities.*, SalesXCommodities.*
                        FROM SalesXCommodities
                        INNER JOIN Sales ON Sales.Id = SalesXCommodities.IdSales
                        INNER JOIN Commodities ON SalesXCommodities.IdCommodities = Commodities.Id";

                    List<SaleXCommodityModel> results = db.Query<SaleModel, CommodityModel, SaleXCommodityModel>(mySql,
                        (sale, commodity) =>
                        {
                            SaleXCommodityModel saleDetails = new SaleXCommodityModel
                            {
                                Sales = sale,
                                Commodities = commodity                           
                            };

                            return saleDetails;
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
                    var mySql = $@"SELECT Sales.*, Commodities.*, SalesXCommodities.*
                        FROM SalesXCommodities
                        INNER JOIN Sales ON Sales.Id = SalesXCommodities.IdSales
                        INNER JOIN Commodities ON SalesXCommodities.IdCommodities = Commodities.Id
                        WHERE SalesXCommodities.Id = {id}";


                    var results = db.Query<SaleModel, CommodityModel, SaleXCommodityModel>(mySql,
                        (sale, commodity) =>
                        {
                            SaleXCommodityModel saleDetails = new SaleXCommodityModel
                            {
                                Sales = sale,
                                Commodities = commodity
                            };

                            return saleDetails;
                        },
                        splitOn: "Id, Id, Id"
                    ).FirstOrDefault();

                    return results;
                }
            }
            catch (Exception ex)
            {
                SaleXCommodityModel result = new SaleXCommodityModel();
                return result;
            }
        }

        public string Update(SaleXCommodityModel saleXCommodity)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"UPDATE Stock SET SupplierName = {saleXCommodity.Sales}, Tel = {saleXCommodity.Commodities} WHERE Id = {saleXCommodity.Id}";
                    var rowsAffected = db.Execute(mySql);
                    if (rowsAffected > 0)
                    {
                        return "Venta actualizada correctamente";
                    }
                    else
                    {
                        return "No se encontró ninguna venta para actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrió un error al actualizar la venta.";

            }
        }
    }
}
