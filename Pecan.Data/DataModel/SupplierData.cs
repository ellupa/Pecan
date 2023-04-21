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
    public class Supplier : ICrd<SupplierModel>, IModify<SupplierModel>
    {
        public string Add(SupplierModel supplier)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"INSERT INTO Suppliers(SupplierName, Tel)Values('{supplier.SupplierName}','{supplier.Tel}')";
                    db.Execute(mySql);
                }
                return "Se guardo correctamente el proveedor";
            }
            catch (Exception)
            {
                return "No se pudo guardar el proveedor, Fijese que esten bien ingresado";
            }
        }

        public string Delete(SupplierModel supplier)
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

        public IEnumerable<SupplierModel> GetAll()
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT Id,SupplierName,Tel FROM Suppliers";
                    var result = db.Query<SupplierModel>(mySql);
                    return result.ToList();
                }
            }
            catch (Exception)
            {
                List<SupplierModel> _lstSupplier = new List<SupplierModel>();
                return _lstSupplier;
            }
        }

        public SupplierModel GetById(int id)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"SELECT Id,SupplierName,Tel FROM Suppliers WHERE Id = {id}";
                    var result = db.QueryFirstOrDefault<SupplierModel>(mySql);
                    if (result != null)
                        return result;
                    return result = new SupplierModel();
                }
            }
            catch (Exception ex)
            {
                SupplierModel supplier = new SupplierModel();
                return supplier;
            }
        }

        public string Update(SupplierModel supplier)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"UPDATE Stock SET SupplierName = {supplier.SupplierName}, Tel = {supplier.Tel} WHERE Id = {supplier.Id}";
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