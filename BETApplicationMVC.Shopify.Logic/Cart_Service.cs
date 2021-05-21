using BETApplicationMVC.Shopify.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BETApplicationMVC.Shopify.Logic
{
    public  class Cart_Service
    {
        private Data_Context dataContext;
        public static string shoppingCartID { get; set; }
        public const string CartSessionKey = "CartId";
        public Cart_Service()
        {
            this.dataContext = new Data_Context();
        }

        public async Task<bool> AddItemToCart(int id)
        {
            shoppingCartID = GetCartID();


            Item Itemobj = new Item();
            Cart_Item CartItemobj = new Cart_Item();
            HttpClient client = new HttpClient();
            int itemcode = 0;
            if (id != 0)
            {

                const string url2 = "http://localhost:53854/api/Item";
                HttpResponseMessage response = await client.GetAsync($"{url2}/{id}");
                // response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var ItemResponse = response.Content.ReadAsStringAsync().Result;
                    Itemobj = JsonConvert.DeserializeObject<Item>(ItemResponse);
                }


                if (Itemobj != null)
                {
                    itemcode = Itemobj.ItemCode;
                    const string url1 = "http://localhost:53854/api/CartItem";
                    HttpResponseMessage responsecart = await client.GetAsync($"{url1}/{shoppingCartID}/{itemcode}");
                    // response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        var CartItemResponse = responsecart.Content.ReadAsStringAsync().Result;

                        CartItemobj = JsonConvert.DeserializeObject<Cart_Item>(CartItemResponse);
                        return true;
                    }
                }
            }
            return false;
        }

        
        public void RemoveItemFromCart(string id)
        {
            shoppingCartID = GetCartID();

            var item = dataContext.Cart_Items.Find(id);
            if (item != null)
            {
                var cartItem =
                    dataContext.Cart_Items.FirstOrDefault(predicate: x => x.cart_id == shoppingCartID && x.item_id == item.item_id);
                if (cartItem != null)
                {
                    dataContext.Cart_Items.Remove(entity: cartItem);
                }
                dataContext.SaveChanges();
            }
        }
        public List<Cart_Item> GetCartItems()
        {
            shoppingCartID = GetCartID();
            return dataContext.Cart_Items.ToList().FindAll(match: x => x.cart_id == shoppingCartID);
        }
        public void UpdateCart(string id, int qty)
        {
            var item = dataContext.Cart_Items.Find(id);
            if (qty < 0)
                item.quantity = qty / -1;
            else if (qty == 0)
                RemoveItemFromCart(item.cart_item_id);
            else if (item.Item.QuantityInStock < qty)
                item.quantity = item.Item.QuantityInStock;
            else
                item.quantity = qty;
            dataContext.SaveChanges();
        }
        public double GetCartTotal(string id)
        {
            double amount = 0;
            foreach (var item in dataContext.Cart_Items.ToList().FindAll(match: x => x.cart_id == id))
            {
                amount += (item.price * item.quantity);
            }
            return amount;
        }
        public void EmptyCart()
        {
            shoppingCartID = GetCartID();
            foreach (var item in dataContext.Cart_Items.ToList().FindAll(match: x => x.cart_id == shoppingCartID))
            {
                dataContext.Cart_Items.Remove(item);
            }
            try
            {
                dataContext.Carts.Remove(dataContext.Carts.Find(shoppingCartID));
                dataContext.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public string GetCartID()
        {
            if (System.Web.HttpContext.Current.Session[name: CartSessionKey] == null)
            {
                if (!String.IsNullOrWhiteSpace(value: System.Web.HttpContext.Current.User.Identity.Name))
                {
                    System.Web.HttpContext.Current.Session[name: CartSessionKey] = System.Web.HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid temp = Guid.NewGuid();
                    System.Web.HttpContext.Current.Session[name: CartSessionKey] = temp.ToString();
                }
            }
            return System.Web.HttpContext.Current.Session[name: CartSessionKey].ToString();
        }
    }
}
