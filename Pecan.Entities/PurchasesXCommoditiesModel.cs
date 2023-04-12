using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Entities
{
    public class PurchasesXCommoditiesModel
    {
        public int Id { get; set; }
        public PurchaseModel? Purchase { get; set; }
        public CommodityModel? Commodity { get; set; }
        public float QuantityOfProducts { get; set; }
    }
}
