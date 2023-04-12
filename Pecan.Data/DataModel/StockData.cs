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
    public class StockData :IAddAList<StockModel>, IModify<StockModel>
    {
        public string Add(StockModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"INSERT INTO Stock(QuantityOfProducts)Values('{model.QuantityOfProducts}')";
                    db.Execute(mySql);
                }
                return "Se guardo correctamente el stock";
            }
            catch (Exception)
            {
                return "No se pudo guardar el stock, Fijese que esten bien ingresado";
            }
        }

        public string Delete(StockModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"DELETE FROM Stock WHERE Id = {model.Id}";
                    var rowsAffected = db.Execute(mySql);
                    if (rowsAffected > 0)
                    {
                        return "Stock eliminado correctamente";
                    }
                    else
                    {
                        return "No se encontró ningún stock para eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrió un error al eliminar el Stock.";
            }
        }

        public IEnumerable<StockModel> GetAll()
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT Id,QuantityOfProducts FROM Stock";
                    var result = db.Query<StockModel>(mySql);
                    return result.ToList();
                }
            }
            catch (Exception)
            {
                List<StockModel> _lstStock = new List<StockModel>();
                return _lstStock;
            }
        }

        public StockModel GetById(int id)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"SELECT Id, QuantityOfProducts FROM Stock WHERE Id = {id}";
                    var result = db.QueryFirstOrDefault<StockModel>(mySql);
                    if (result != null)
                        return result;
                    return result = new StockModel();
                }
            }
            catch (Exception ex)
            {
                StockModel stock= new StockModel();
                return stock;
            }
        }

        public string Update(StockModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"UPDATE Stock SET QuantityOfProducts = {model.QuantityOfProducts} WHERE Id = {model.Id}";
                    var rowsAffected = db.Execute(mySql);
                    if (rowsAffected > 0)
                    {
                        return "Stock actualizado correctamente";
                    }
                    else
                    {
                        return "No se encontró ningún stock para actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ocurrió un error al actualizar el Stock.";
            }
        }
    }
}
