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
    public class SaleData : IAddAList<SaleModel>, IModify<SaleModel>
    {
        public string Add(SaleModel sale)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"INSERT INTO Sales(DateOfSale,Total)Values('{sale.DateOfSale}','{sale.Total}')";
                    db.Execute(mySql);
                }
                return "Se guardo correctamente la venta";
            }
            catch (Exception)
            {
                return "No se pudo guardar la venta, Fijese que esten bien ingresado";
            }
        }

        public string Delete(SaleModel sale)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"DELETE FROM Sales WHERE Id = {sale.Id}";
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

        public IEnumerable<SaleModel> GetAll()
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT Id,DateOfSale,Total FROM Sales";
                    var result = db.Query<SaleModel>(mySql);
                    return result.ToList();
                }
            }
            catch (Exception)
            {
                List<SaleModel> _lstStock = new List<SaleModel>();
                return _lstStock;
            }
        }

        public SaleModel GetById(int id)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"SELECT Id,DateOfSale,Total FROM Sales WHERE Id = {id}";
                    var result = db.QueryFirstOrDefault<SaleModel>(mySql);
                    if (result != null)
                        return result;
                    return result = new SaleModel();
                }
            }
            catch (Exception ex)
            {
                SaleModel sale = new SaleModel();
                return sale;
            }
        }

        public string Update(SaleModel sale)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"UPDATE Sales SET DateOfSale = {sale.DateOfSale} WHERE Id = {sale.Total}";
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
