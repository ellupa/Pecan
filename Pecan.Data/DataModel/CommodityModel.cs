﻿
using Dapper;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Data.DataModel
{
    public class CommodityModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string? CommodityName { get; set; }
        public string CodBar { get; set; } = string.Empty;
        public float CostPrice { get; set; }
        public float PricePublic { get; set; }
        public int IdStock { get; set; }
        
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

        public IEnumerable<CommodityModel> GetProduct(string name)
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

       /* public IEnumerable<CommodityModel> GetProduct(int codBar)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
       */
    }
}
