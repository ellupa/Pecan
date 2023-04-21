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
            if (data.GetAll().Any())
                return data.GetAll();
            else
                throw new Exception("No hay Productos Cargados");
        }

        public CommodityModel GetById(int id)
        {
            if (data.GetById(id) != null)
                return data.GetById(id);
            else
                throw new Exception("No se encontro el producto");
        }

        public CommodityModel GetCommodityByCodeBar(string code)
        {
            if (Regex.IsMatch(code, @"^[0-9]+$"))
                if (data.GetCommodityByCodeBar(code) != null)
                    return data.GetCommodityByCodeBar(code);
                else
                    throw new Exception("No se encontro el producto por el codigo de barras");
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