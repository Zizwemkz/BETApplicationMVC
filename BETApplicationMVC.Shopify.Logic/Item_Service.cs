using BETApplicationMVC.Shopify.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BETApplicationMVC.Shopify.Logic
{
    public class Item_Service
    {
        private Data_Context dataContext;

        public Item_Service()
        {
           this.dataContext = new Data_Context();
        }

        public List<Item> GetItems()
        {
            //return Task.Run(() => GetAllItems()).Result;
            return dataContext.Items.Include(i => i.Category).ToList();
        }

      
        public async Task<List<Item>> GetAllItems()
        {
            HttpClient client = new HttpClient();
            List<Item> Departmentobj = new List<Item>();
            const string url = "http://localhost:53854/api/Item";
            HttpResponseMessage response = await client.GetAsync($"{url}");
            // response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var DepartmenyResponse = response.Content.ReadAsStringAsync().Result;

                Departmentobj = JsonConvert.DeserializeObject<List<Item>>(DepartmenyResponse);
                return Departmentobj;
            }
            //  return posts;
            return Departmentobj;
            //return dataContext.Items.Include(i => i.Category).ToList();
        }

        public async Task<Item> GetItemById(int? ItemId)
        {
            HttpClient client = new HttpClient();
            Item Departmentobj = new Item();
            const string url = "http://localhost:53854/api/Item";
            HttpResponseMessage response = await client.GetAsync($"{url}/{ItemId}");
            // response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var ItemResponse = response.Content.ReadAsStringAsync().Result;

                Departmentobj = JsonConvert.DeserializeObject<Item>(ItemResponse);
                return Departmentobj;
            }
            //  return posts;
            return Departmentobj;
            //return dataContext.Items.Include(i => i.Category).ToList();
        }


        //public bool AddItem(Item item)
        //{
        //    try
        //    {
        //        dataContext.Items.Add(item);
        //        dataContext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    { return false; }
        //}


        public async Task<bool> CreateItem(Item itemmodel)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(itemmodel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage respose = await client.PostAsync("http://localhost:53854/api/Item", content);

            if (respose.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

       
        public bool UpdateItem(Item item)
        {
            try
            {
                dataContext.Entry(item).State = EntityState.Modified;
                dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public bool RemoveItem(Item item)
        {
            try
            {
                dataContext.Items.Remove(item);
                dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        public Item GetItem(int? item_id)
        {
            return dataContext.Items.Find(item_id);
        }
    }
}
