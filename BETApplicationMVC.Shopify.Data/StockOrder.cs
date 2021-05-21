using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anele.Shopify.Data
{
   public class StockOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Order_ID { get; set; }
        public DateTime date_created { get; set; }
        public bool shipped { get; set; }
        public string status { get; set; }
        public bool packed { get; set; }

        public virtual ICollection<StockOrder_Item> StockOrder_Items { get; set; }
    }
}
