using BETApplicationMVC.Shopify.Data;
using BETApplicationMVC.Shopify.Logic;
using BETApplicationMVC.Shopify.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BETApplicationMVC.Shopify.Controllers
{
    public class ShoppingController : Controller
    {
        private Cart_Service cart_Service;
        private Item_Service item_Service;
        private Customer_Service customer_Service;
        private Order_Service order_Service;
        private Department_Service department_Service;

        public ShoppingController()
        {
            this.cart_Service = new Cart_Service();
            this.item_Service = new Item_Service();
            this.order_Service = new Order_Service();
            this.department_Service = new Department_Service();
        }
        public async Task<ActionResult> Index(int? id)
        {
            List<Item> items_results = new List<Item>();
            try
            {
                if(id !=null && id != 0)
                {
                        items_results = item_Service.GetItems().Where(x => x.Category.Department_ID == (int)id).ToList();
                        ViewBag.Department = department_Service.GetDepartmentById(id);
                }                    
                
                else
                {
                    items_results = await item_Service.GetAllItems();
                    ViewBag.Department = "All Departments";
                }
            }
            catch(Exception ex) { }
            return View(items_results);
        }
        public ActionResult add_to_cart(int id)
        {
            var item = item_Service.GetItemById(id);
            if (item != null)
            {
                cart_Service.AddItemToCart(id);
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult remove_from_cart(string id)
        {
            var item = cart_Service.GetCartItems().FirstOrDefault(x => x.cart_item_id == id);
            if (item != null)
            {
                cart_Service.RemoveItemFromCart(id: id);
                return RedirectToAction("ShoppingCart");
            }
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult ShoppingCart()
        {
            ViewBag.Total = cart_Service.GetCartTotal(cart_Service.GetCartID());
            ViewBag.TotalQTY = cart_Service.GetCartItems().FindAll(x => x.cart_id == cart_Service.GetCartID()).Sum(q => q.quantity);
            return View(cart_Service.GetCartItems().FindAll(x => x.cart_id == cart_Service.GetCartID()));
        }
        [HttpPost]
        public ActionResult ShoppingCart(List<Cart_Item> items)
        {
            foreach (var i in items)
            {
                cart_Service.UpdateCart(i.cart_item_id, i.quantity);
            }          
            return RedirectToAction("ShoppingCart");
        }
        public ActionResult countCartItems()
        {
            int qty = cart_Service.GetCartItems().Sum(x => x.quantity);
            return Content(qty.ToString());
        }
        public ActionResult Checkout()
        {
            if (cart_Service.GetCartItems().Count == 0)
            {
                ViewBag.Err = "Opps... you should have atleat one cart item, please shop a few items";
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("HowToGetMyOrder");
        }
      
        public ActionResult HowToGetMyOrder()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HowToGetMyOrder(string street_number, string street_name, string City, string State, string ZipCode, string Country)
        {
            Session["street_number"] = street_number;
            Session["street_name"] = street_name;
            Session["City"] = City;
            Session["State"] = State;
            Session["ZipCode"] = ZipCode;
            Session["Country"] = Country;
            return RedirectToAction("PlaceOrder", new { id = "deliver" });
        }
     
        public ActionResult Return_Url(string id)
        {
            var order = order_Service.GetOrder(id);

            ViewBag.Order = order;
            ViewBag.Account = customer_Service.GetCustomer(order.Email);

            ViewBag.Items = order_Service.GetOrderItems(order.Order_ID);
            ViewBag.Total = order_Service.GetOrderTotal(order.Order_ID);           
            return View();
        }
      
    }
}