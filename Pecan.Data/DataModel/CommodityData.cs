using Dapper;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using Pecan.Entities;
using Pecan.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pecan.Data.DataModel
{
    public class CommodityData : IAddAList<CommodityModel>, IModify<CommodityModel>
    {
        public string Add(CommodityModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "INSERT INTO Commodities(CommodityName, CodBar, CostPrice, PricePublic, IdStock)" +
                        " Values(@Name, @CodBar, @CostPrice, @PricePublic, @IdStock)";
                    var result = db.Execute(mySql,
                    new
                    {
                        Name = model.CommodityName,
                        CodBar = model?.CodBar,
                        PricePublic = model?.PricePublic,
                        CostPrice = model?.CostPrice,
                        IdStock = model?.IdStock
                    });
                }
                return "Se guardo correctamente el producto nuevo";
            }
            catch (Exception)
            {
                return "No se pudo guardar el producto, Fijese que esten bien los Formatos de textos y numeros";
            }
        }

        public string Delete(CommodityModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"DELETE FROM Comodities Where({model.Id} = Id)";
                    return "Se elimino correctamente";
                };
            }
            catch (Exception)
            {

                return "No se pudo eliminar";
            }
            
        }

        public IEnumerable<CommodityModel> GetAll()
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT CommodityName, CodBar, CostPrice, PricePublic, IdStock FROM Commodities";
                    var result = db.Query<CommodityModel>(mySql);
                    return result.ToList();
                }
            }
            catch (Exception)
            {
                List<CommodityModel> _lstCommodity = new List<CommodityModel>();
                return _lstCommodity;
            }
        }

        public CommodityModel GetById(int id)
        {
            CommodityModel commodityModel;
            try
            {                
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"Select * From Commodities WHERE({id} = Id)";
                    commodityModel = (CommodityModel)db.Query<CommodityModel>(mySql);
                };

                return commodityModel;
            }
            catch (Exception)
            {
                commodityModel = new CommodityModel();
                return commodityModel;
            }
            
        }

        public CommodityModel GetCommodityByCodeBar(string code)
        {
            CommodityModel model;
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT CommodityName, CodBar, CostPrice, PricePublic, IdStock FROM Commodities" +
                        $" WHERE (CodBar = {code})";
                    model = (CommodityModel)db.Query<CommodityModel>(mySql);
                    return model;
                }
            }
            catch (Exception)
            {
                model = new CommodityModel();
                return model;
            }
        }

        public IEnumerable<CommodityModel> GetCommodityByName(string name)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT CommodityName, CodBar, CostPrice, PricePublic, IdStock FROM Commodities" +
                        $" WHERE CommodityName LIKE {name}%";
                    var result = db.Query<CommodityModel>(mySql);
                    return result.ToList();
                }
            }
            catch (Exception)
            {
                List<CommodityModel> _lstCommodity = new List<CommodityModel>();
                return _lstCommodity;
            }
        }

        public string Update(CommodityModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = $"UPDATE Commodities SET CommodityName={model.CommodityName}, CodBar={model.CodBar}, CostPrice = {model.CostPrice}, PricePublic={model.PricePublic}, IdStock={model.IdStock} FROM Commodities WHERE (Id = {model.Id})";
                    var result = db.Query<CommodityModel>(mySql);
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
