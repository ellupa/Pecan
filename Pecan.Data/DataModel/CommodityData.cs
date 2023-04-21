using Dapper;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Pecan.Entities;
using Pecan.Interface;

namespace Pecan.Data.DataModel
{
    public class CommodityData : ICrd<CommodityModel>, IModify<CommodityModel>
    {
        public string Add(CommodityModel model)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "INSERT INTO Stock(QuantityOfProducts) Values (@QuantityOfProducts)";
                    var result = db.Execute(mySql,
                    new
                    {
                        QuantityOfProducts = model?.Stock?.QuantityOfProducts
                    }
                    );
                    mySql = "SELECT TOP 1 Id From Stock ORDER BY Id DESC";
                    var op = (StockModel)db.Query<StockModel>(mySql);

                    if (model?.Stock != null)
                        model.Stock.Id= op.Id;
                    
                    mySql = "INSERT INTO Commodities(CommodityName, CodBar, CostPrice, PricePublic, IdStock)" +
                        " Values(@Name, @CodBar, @CostPrice, @PricePublic, @IdStock)";
                    result = db.Execute(mySql,
                    new
                    {
                        Name = model?.CommodityName,
                        CodBar = model?.CodBar,
                        PricePublic = model?.PricePublic,
                        CostPrice = model?.CostPrice,
                        IdStock = model?.Stock?.Id,
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
                    var mySql = $"DELETE FROM Stock Where({model?.Stock?.Id} = Id)";
                    var result = db.Execute(mySql);
                    mySql = $"DELETE FROM Comodities Where({model?.Id} = Id)";
                    result = db.Execute(mySql);
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
                    var mySql = "SELECT Commodities.* Stock.* FROM Commodities" +
                        "INNER JOIN Stock ON Stock.Id = CommodityName.IdStock";
                    var results = db.Query<CommodityModel, StockModel, CommodityModel>(mySql,
                        (commodities,stock) =>
                        {
                            CommodityModel commodityDetails = new CommodityModel
                            {
                                Id = commodities.Id,
                                CodBar = commodities.CodBar,
                                CommodityName = commodities.CommodityName,
                                CostPrice = commodities.CostPrice,
                                PricePublic = commodities.PricePublic,
                                Stock = stock,

                            };
                            return commodityDetails;
                        },
                        splitOn: "Id, Id"
                    );
                    return results.ToList();
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
                    var mySql = "SELECT Commodities.* Stock.* FROM Commodities" +
                        "INNER JOIN Stock ON Stock.Id = CommodityName.IdStock" +
                        $"WHERE {id} = id";
                    CommodityModel results = (CommodityModel)db.Query<CommodityModel, StockModel, CommodityModel>(mySql,
                        (commodities, stock) =>
                        {
                            CommodityModel commodityDetails = new CommodityModel
                            {
                                Id = commodities.Id,
                                CodBar = commodities.CodBar,
                                CommodityName = commodities.CommodityName,
                                CostPrice = commodities.CostPrice,
                                PricePublic = commodities.PricePublic,
                                Stock = stock,

                            };
                            return commodityDetails;
                        },
                        splitOn: "Id, Id"
                    );
                    return results;
                };                
            }
            catch (Exception)
            {
                commodityModel = null;
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
                    var mySql = "SELECT Commodities.* Stock.* FROM Commodities" +
                        "INNER JOIN Stock ON Stock.Id = CommodityName.IdStock" +
                        $"WHERE {code} = CodeBar";
                    CommodityModel results = (CommodityModel)db.Query<CommodityModel, StockModel, CommodityModel>(mySql,
                        (commodities, stock) =>
                        {
                            CommodityModel commodityDetails = new CommodityModel
                            {
                                Id = commodities.Id,
                                CodBar = commodities.CodBar,
                                CommodityName = commodities.CommodityName,
                                CostPrice = commodities.CostPrice,
                                PricePublic = commodities.PricePublic,
                                Stock = stock,

                            };
                            return commodityDetails;
                        },
                        splitOn: "Id, Id"
                    );
                    return results;
                }
            }
            catch (Exception)
            {
                model = null;
                return model;
            }
        }

        public IEnumerable<CommodityModel> GetCommodityByName(string name)
        {
            try
            {
                using (var db = new MySqlConnection(PecanContext.ConnectionString()))
                {
                    var mySql = "SELECT Commodities.* Stock.* FROM Commodities" +
                        "INNER JOIN Stock ON Stock.Id = Commodities.IdStock" +
                        $"WHERE CommodityName LIKE {name}%";
                    var results = db.Query<CommodityModel, StockModel, CommodityModel>(mySql,
                        (commodities, stock) =>
                        {
                            CommodityModel commodityDetails = new CommodityModel
                            {
                                Id = commodities.Id,
                                CodBar = commodities.CodBar,
                                CommodityName = commodities.CommodityName,
                                CostPrice = commodities.CostPrice,
                                PricePublic = commodities.PricePublic,
                                Stock = stock,

                            };
                            return commodityDetails;
                        },
                        splitOn: "Id, Id"
                    );                    
                    return results.ToList();
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
                    var mySql = $"UPDATE Stock SET QuantityOfProducts = {model?.Stock?.QuantityOfProducts}";
                    var resultStock = db.Query<StockModel>(mySql);
                    mySql = $"UPDATE Commodities SET CommodityName={model?.CommodityName}," +
                        $" CodBar={model?.CodBar}, CostPrice = {model?.CostPrice}, " +
                        $"PricePublic={model?.PricePublic} " +
                        $"FROM Commodities WHERE (Id = {model?.Id})";
                   var resultCommodity = db.Query<CommodityModel>(mySql);
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
