using BETApplicationMVC.Shopify.Data;
using BETApplicationMVC.Shopify.Logic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BETApplicationMVC.Shopify.Controllers
{
    public class StockController : Controller
    {
        private Data_Context db;
        private Item_Service item_Service;
        private Stock_Service stock_Service;
        public string shoppingCartID { get; set; }
        public const string CartSessionKey = "CartId";
        // GET: Stock
        public StockController()
        {
            this.db = new Data_Context();
            this.item_Service = new Item_Service();
            this.stock_Service = new Stock_Service();
        }

        public ActionResult Fall_catalog()
        {
            return View(item_Service.GetItems());
        }

        public string GetCartID()
        {
            if (System.Web.HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!String.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    System.Web.HttpContext.Current.Session[CartSessionKey] = System.Web.HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid temp_cart_ID = Guid.NewGuid();
                    System.Web.HttpContext.Current.Session[CartSessionKey] = temp_cart_ID.ToString();
                }
            }
            return System.Web.HttpContext.Current.Session[CartSessionKey].ToString();
        }
        
    }
}