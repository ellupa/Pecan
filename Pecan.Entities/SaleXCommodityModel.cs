using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Data.DataModel
{
    public class SaleXCommodityModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public float QuantityOfProduct { get; set; }

        public int IdSales { get; set; }

        public int IdCommodities { get; set; }
    }
}
