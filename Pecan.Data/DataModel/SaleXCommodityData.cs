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
    public class SaleXCommodityData : IAddAList<SaleDetailModel>, IModify<SaleXCommodityModel>
    {
        public string Add(SaleDetailModel supplier)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"INSERT INTO Stock(SupplierName, Tel)Values('{supplier.IdSales}','{supplier.IdCommodities}')";
                    db.Execute(mySql);
                }
                return "Se guardo correctamente el proveedor";
            }
            catch (Exception)
            {
                return "No se pudo guardar el proveedor, Fijese que esten bien ingresado";
            }
        }

        public string Delete(SaleDetailModel supplier)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"DELETE FROM Suppliers WHERE Id = {supplier.Id}";
                    var rowsAffected = db.Execute(mySql);
                    if (rowsAffected > 0)
                    {
                        return "Proveedor eliminado correctamente";
                    }
                    else
                    {
                        return "No se encontró ningún proveedor para eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrió un error al eliminar el proveedor.";
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
                    var mySql = $"SELECT Id,SupplierName,Tel FROM Stock WHERE Id = {id}";
                    var result = db.QueryFirstOrDefault<SaleXCommodityModel>(mySql);
                    return result;
                }
            }
            catch (Exception ex)
            {
                SaleXCommodityModel supplier = new SaleXCommodityModel();
                return supplier;
            }
        }

        public string Update(SaleXCommodityModel supplier)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"UPDATE Stock SET SupplierName = {supplier.IdSales}, Tel = {supplier.IdCommodities} WHERE Id = {supplier.Id}";
                    var rowsAffected = db.Execute(mySql);
                    if (rowsAffected > 0)
                    {
                        return "Proveedor actualizado correctamente";
                    }
                    else
                    {
                        return "No se encontró ningún proveedor para actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrió un error al actualizar el proveedor.";

            }
        }

        IEnumerable<SaleXCommodityModel> IAddAList<SaleXCommodityModel>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
