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

namespace Pecan.Data.DataModel
{
    public class CommodityData : IAddAList<CommodityModel>
    {
        public string Add(CommodityModel model)
        {
            throw new NotImplementedException();
        }

        public string AddCommodity(CommodityModel commodityModel)
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
                        Name = commodityModel.CommodityName,
                        CodBar = commodityModel?.CodBar,
                        PricePublic = commodityModel?.PricePublic,
                        CostPrice = commodityModel?.CostPrice,
                        IdStock = commodityModel?.IdStock
                    }) ;
                }
                    return "Se guardo correctamente el producto nuevo";
            }
            catch (Exception)
            {
                return "No se pudo guardar el producto, Fijese que esten bien los Formatos de textos y numeros";
            }
            
        }

        public IEnumerable<CommodityModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public CommodityModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public CommodityModel GetProduct(string name)
        {
            CommodityModel? model = null;
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    //var mySql = "";
                }
                return model;
            }
            catch (Exception)
            {

                return model;
            }
        }

        public IEnumerable<CommodityModel> GetProducts(string name)
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
    }
}
