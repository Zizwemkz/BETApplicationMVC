using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETApplicationMVC.Shopify.Data
{
    public partial class StockCart
    {
        [Key]
        public string cart_id { get; set; }
        public DateTime date_created { get; set; }
        public virtual ICollection<StockCart_Item> StockCart_Items { get; set; }
    }
}
