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
    public class Category_Service
    {
        private Data_Context dataContext;

        public Category_Service()
        {
            this.dataContext = new Data_Context();
        }

        public List<Category> GetCategories()
        {
            return dataContext.Categories.ToList();
        }
   
        public async Task<List<Category>> GetAllCategories()
        {
            HttpClient client = new HttpClient();
            List<Category> Categoryobj = new List<Category>();
            const string url = "http://localhost:53854/api/Category";
            HttpResponseMessage response = await client.GetAsync($"{url}");
            // response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var CategoryResponse = response.Content.ReadAsStringAsync().Result;

                Categoryobj = JsonConvert.DeserializeObject<List<Category>>(CategoryResponse);
                return Categoryobj;
            }
            //  return posts;
            return Categoryobj;
            //return dataContext.Items.Include(i => i.Category).ToList();
        }


        public async Task<bool> AddCategory(Category model)
        {

            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage respose = await client.PostAsync("http://localhost:53854/api/Category", content);

            if (respose.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<Category> GetCategory(int? categoryid)
        {
            HttpClient client = new HttpClient();
            Category Categoryobj = new Category();
            const string url = "http://localhost:53854/api/Category";
            HttpResponseMessage response = await client.GetAsync($"{url}/{categoryid}");
            // response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var EmpResponse = response.Content.ReadAsStringAsync().Result;

                Categoryobj = JsonConvert.DeserializeObject<Category>(EmpResponse);
                //JsonSerializer.Deserialize<Category>(response.Content.ReadAsStreamAsync());
                return Categoryobj;
            }
            //  return posts;
            return Categoryobj;
            // return dataContext.Categories.Find(category_id);
        }

        //public bool UpdateCategory(Category category)
        //{
        //    try
        //    {
        //        dataContext.Entry(category).State = EntityState.Modified;
        //        dataContext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    { return false; }
        //}
        //public bool RemoveCategory(Category category)
        //{
        //    try
        //    {
        //        dataContext.Categories.Remove(category);
        //        dataContext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    { return false; }
        //}

    }
}
