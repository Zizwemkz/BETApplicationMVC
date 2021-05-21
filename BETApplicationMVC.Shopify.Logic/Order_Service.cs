using BETApplicationMVC.Shopify.Data;
using BETApplicationMVC.Shopify.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BETApplicationMVC.Shopify.Logic
{
    public class Order_Service
    {
        private Data_Context dataContext;
        private Payment_Service payment_Service;

        public Order_Service()
        {
            this.dataContext = new Data_Context();
            this.payment_Service = new Payment_Service();
        }
        public List<Order> GetOrders()
        {
            return dataContext.Orders.ToList();
        }
        public List<Order> GetOrders(string status)
        {
            return dataContext.Orders.Where(p => p.status.ToLower() == status.ToLower()).ToList();
        }
        public Order GetOrder(string order_id)
        {
            return dataContext.Orders.Find(order_id);
        }
        public List<Order_Item> GetOrderItems(string order_id)
        {
            return GetOrder(order_id).Order_Items.ToList();
        }
        public OrderDetailModel GetOrderDetail(string order_id)
        {
            try
            {
                string shipping_method = "Collect at warehouse", payment_method = "Awaiting Payment";
                if (payment_Service.GetOrderPayment(order_id) != null)
                    payment_method = payment_Service.GetOrderPayment(order_id).PaymentMethod;

                return new OrderDetailModel()
                {
                    customer = GetOrder(order_id).Customer,
                    order = GetOrder(order_id),
                    shipping_method = shipping_method,
                    delivery = null,
                    payment_Method = payment_method,
                    payment = payment_Service.GetOrderPayment(order_id),
                    order_items = GetOrderItems(order_id),
                    order_total = (decimal)GetOrderTotal(order_id)
                };                
            }
            catch(Exception ex) {
                return new OrderDetailModel();
            }
        }
      
        public void AddOrder(Customer customer)
        {
            try
            {                                
                dataContext.Orders.Add(new Order()
                {
                    Order_ID = GenerateOrderNumber(10),
                    Email = customer.Email,
                    date_created = DateTime.Now,
                    shipped = false,
                    status = "Awaiting Payment"
                });
                dataContext.SaveChanges();
            }
            catch(Exception ex) { }           
        }
        public void AddOrderItems(Order order, List<Cart_Item> items)
        {
            foreach (var item in items)
            {
                var x = new Order_Item()
                {
                    Order_id = order.Order_ID,
                    item_id = item.item_id,
                    quantity = item.quantity,
                    price = item.price
                };
                dataContext.Order_Items.Add(x);
                dataContext.SaveChanges();
            }
        }       
     
        public double GetOrderTotal(string order_id)
        {
            double amount = 0;
            foreach (var item in dataContext.Order_Items.ToList().FindAll(match: x => x.Order_id == order_id))
            {
                amount += (item.price * item.quantity);
            }
            return amount;
        }
    
        public string GenerateOrderNumber(int length)
        {
            var random = new Random();
            string number = string.Empty;
            for (int i = 0; i < length; i++)
                number = String.Concat(number, random.Next(10).ToString());
            while (GetOrder(number) != null)
               number = GenerateOrderNumber(length);
            return number;
        }


    }
}
