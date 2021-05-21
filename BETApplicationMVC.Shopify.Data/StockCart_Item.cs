﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BETApplicationMVC.Shopify.Data
{
    public partial class StockCart_Item
    {
        [Key]
        public string cart_item_id { get; set; }
        [ForeignKey("StockCart")]
        public string cart_id { get; set; }
        [ForeignKey("Item")]
        public int item_id { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }

        public virtual StockCart StockCart { get; set; }
        public virtual Item Item { get; set; }
    }
}