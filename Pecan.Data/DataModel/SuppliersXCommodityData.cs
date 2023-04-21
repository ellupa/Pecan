﻿using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Pecan.Entities;
using Pecan.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Data.DataModel
{
    public class SuppliersXCommodityData : ICrd<SuppliersXCommoditiesModel>
    {
        public string Add(SuppliersXCommoditiesModel supplierXCommodity)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"INSERT INTO SuppliersXCommodities(IdCommodities, Supplier)Values('{supplierXCommodity.Commodities.Id}','{supplierXCommodity.Suppliers.Id}')";
                    db.Execute(mySql);
                }
                return "Se guardo correctamente el vendedor";
            }
            catch (Exception)
            {
                return "No se pudo guardar el vendedor, Fijese que esten bien ingresado";
            }
        }

        public string Delete(SuppliersXCommoditiesModel supplierXCommodity)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"DELETE FROM SuppliersXCommodities WHERE Id = {supplierXCommodity.Id}";
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

        public IEnumerable<SuppliersXCommoditiesModel> GetAll()
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = @"SELECT sc.Id, Commodities.*, Suppliers.*, Stock.*
                        FROM SuppliersXCommodities sc
                        INNER JOIN Suppliers ON sc.IdSupplier = Suppliers.Id
                        INNER JOIN Commodities ON sc.IdCommodities = Commodities.Id
                        INNER JOIN Stock ON Commodities.IdStock = Stock.Id;";

                    var results = db.Query<SuppliersXCommoditiesModel, SupplierModel, CommodityModel, StockModel, SuppliersXCommoditiesModel>(mySql,
                        (suppliersXCommodities, supplier, commodity, stock) =>
                        {
                            suppliersXCommodities.Suppliers = supplier;
                            if (commodity!=null)
                            {
                                commodity.Stock= stock;
                            }
                            suppliersXCommodities.Commodities = commodity;
                            return suppliersXCommodities;
                        },
                        splitOn: "Id, Id, Id"
                    ).ToList();

                    return results;
                }
            }
            catch (Exception ex)
            {
                List<SuppliersXCommoditiesModel> result = new List<SuppliersXCommoditiesModel>();
                return result;
            }
        }


        public SuppliersXCommoditiesModel GetById(int id)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = @"SELECT sc.Id, Commodities.*, Suppliers.*, Stock.*
                        FROM SuppliersXCommodities sc
                        INNER JOIN Suppliers ON sc.IdSupplier = Suppliers.Id
                        INNER JOIN Commodities ON sc.IdCommodities = Commodities.Id
                        INNER JOIN Stock ON Commodities.IdStock = Stock.Id
                        WHERE sc.Id = @id";

                    var parameters = new { id };
                    var results = db.Query<SuppliersXCommoditiesModel, SupplierModel, CommodityModel, StockModel, SuppliersXCommoditiesModel>(mySql,
                        (suppliersXCommodities, supplier, commodity, stock) =>
                        {
                            suppliersXCommodities.Suppliers = supplier;
                            if (commodity != null)
                            {
                                commodity.Stock = stock;
                            }
                            suppliersXCommodities.Commodities = commodity;
                            return suppliersXCommodities;
                        },
                        parameters,
                        splitOn: "Id, Id, Id"
                    ).FirstOrDefault();

                    if (results != null)
                        return results;

                    return results = new SuppliersXCommoditiesModel();
                }
            }
            catch (Exception ex)
            {
                SuppliersXCommoditiesModel result = new SuppliersXCommoditiesModel();
                return result;
            }
        }
    }
}
