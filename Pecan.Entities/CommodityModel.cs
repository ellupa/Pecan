﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Entities
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
        public StockModel? Stock { get; set; }
                
    }
}
