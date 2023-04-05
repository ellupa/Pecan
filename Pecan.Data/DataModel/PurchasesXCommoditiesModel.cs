using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Data.DataModel
{
    public class PurchasesXCommoditiesModel
    {
        public int Id { get; set; }
        public PurchaseModel? Purchase { get; set; }
        public int IdCommodity { get; set; }
        public float QuantityOfProducts { get; set; }
    }
}
