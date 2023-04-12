using Dapper;
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
    public class SuppliersXCommodityData : IAddAList<SuppliersXCommoditiesModel>, IModify<SuppliersXCommoditiesModel>
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
                    var mySql = @"SELECT sc.Id, Commodities.*, Suppliers.*
                        FROM SuppliersXCommodities sc
                        INNER JOIN Suppliers ON sc.IdSupplier = Suppliers.Id
                        INNER JOIN Commodities ON sc.IdCommodities = Commodities.Id;";

                    List<SuppliersXCommoditiesModel> results = db.Query<SupplierModel, CommodityModel, SuppliersXCommoditiesModel, SuppliersXCommoditiesModel>(mySql,
                        (supplier, commodity, suppliersXCommoditiesModel) =>
                        {
                            SuppliersXCommoditiesModel supplierDetails = new SuppliersXCommoditiesModel
                            {
                                Id= suppliersXCommoditiesModel.Id,
                                Suppliers = supplier,
                                Commodities = commodity
                            };

                            return supplierDetails;
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
                   var mySql = $@"SELECT sc.Id, Commodities., Suppliers.
                        FROM SuppliersXCommodities sc
                        INNER JOIN Suppliers ON sc.IdSupplier = Suppliers.Id
                        INNER JOIN Commodities ON sc.IdCommodities = Commodities.Id
                        WHERE sc.Id = {id}";


                    SuppliersXCommoditiesModel? results = db.Query<SupplierModel, CommodityModel, SuppliersXCommoditiesModel, SuppliersXCommoditiesModel>(mySql,
                        (supplier, commodity, suppliersXCommoditiesModel) =>
                        {
                            SuppliersXCommoditiesModel supplierDetails = new SuppliersXCommoditiesModel
                            {
                                Id = suppliersXCommoditiesModel.Id,
                                Suppliers = supplier,
                                Commodities = commodity
                            };

                            return supplierDetails;
                        },
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

        public string Update(SuppliersXCommoditiesModel supplierXCommodity)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"UPDATE SupplierXCommodity SET IdCommodities = {supplierXCommodity.Commodities.Id}, IdSupplier = {supplierXCommodity.Suppliers.Id} WHERE Id = {supplierXCommodity.Id}";
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
    }
}
