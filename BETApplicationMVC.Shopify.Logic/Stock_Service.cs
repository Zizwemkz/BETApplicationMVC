using BETApplicationMVC.Shopify.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BETApplicationMVC.Shopify.Logic
{
    public class Stock_Service
    {
        private Data_Context dataContext;
        private Item_Service item_Service;
        public Stock_Service()
        {
            this.dataContext = new Data_Context();
            this.item_Service = new Item_Service();
        }
        public void UpdateStockReceived(int item_id, int quantity)
        {
            var item = dataContext.Items.Find(item_id);
            item.QuantityInStock += quantity;
            dataContext.SaveChanges();
        }
       

     
        public string GenerateOrderNumber(int length)
        {
            var random = new Random();
            string number = string.Empty;
            for (int i = 0; i < length; i++)
                number = String.Concat(number, random.Next(10).ToString());
           
            return number;
        }
      
 
      
        
    }
}
