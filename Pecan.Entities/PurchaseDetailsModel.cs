using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Entities
{
    public class PurchaseDetailsModel
    {
        public PurchaseModel? Purchase { get; set; }
        public CommodityModel? Commodity { get; set; }
        public PurchasesXCommoditiesModel? PurchaseXCommodity { get; set; }
    }
}
