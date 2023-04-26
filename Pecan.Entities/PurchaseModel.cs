using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Entities
{
    public class PurchaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }        
        public DateTime PurchaseDate { get; set; }
        public SupplierModel? Supplier { get; set; }
        public float Total { get; set; }

        public List<PurchasesXCommoditiesModel>? PurchasesXCommodities { get; set; }

    }
}
