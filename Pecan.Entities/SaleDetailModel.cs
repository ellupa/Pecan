using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Entities
{
    public class SaleDetailModel
    {
        public SaleModel? Sale { get; set; }
        public CommodityModel? Commodity { get; set; }
        public SaleXCommodityModel? SaleXCommodity { get; set; }
    }
}
