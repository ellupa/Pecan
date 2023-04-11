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
        public int? IdPurchase { get; set; }
        public int IdCommodity { get; set; }
        public float QuantityOfProducts { get; set; }
    }
}
