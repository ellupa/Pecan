using Pecan.Data.DataModel;
using Pecan.Entities;
using Pecan.Interface;
using static System.Net.WebRequestMethods;
using System.Text.RegularExpressions;

namespace Pecan.Managers
{
    public class CommodityManager : ICrd<CommodityModel>, IModify<CommodityModel>
    {
        CommodityData data = new CommodityData();
        public string Add(CommodityModel model)
        {
            return data.Add(model);
        }

        public string Delete(CommodityModel model)
        {
            return data.Delete(model);
        }

        public IEnumerable<CommodityModel> GetAll()
        {
            var result = data.GetAll();
            if (result.Any())
                return result;
            else
                throw new Exception("No hay Productos Cargados");
        }

        public CommodityModel GetById(int id)
        {
            var result = data.GetById(id);
            if (result != null)
                return result;
            else
                throw new Exception("No se encontro el producto");
        }

        public CommodityModel GetCommodityByCodeBar(string code)
        {
            
            if (Regex.IsMatch(code, @"^[0-9]+$"))
            {
                var result = data.GetCommodityByCodeBar(code);
                if (result != null)
                    return result;
                else
                    throw new Exception("No se encontro el producto por el codigo de barras");
            }
            else
                throw new Exception("Solo se permiten numeros para buscar por codigo");
        }

        public IEnumerable<CommodityModel> GetCommodityByName(string name)
        {
            if (Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))            
                return data.GetCommodityByName(name);            
            else
                throw new Exception("El nombre de producto ingresado tiene numeros o caractares especiales");                                    
        }

        public string Update(CommodityModel model)
        {
            return data.Update(model);
        }
    }
}