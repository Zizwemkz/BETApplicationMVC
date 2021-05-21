using BETApplicationMVC.Shopify.Data;
using BETApplicationMVC.Shopify.Models;
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
    public class Customer_Service
    {

        private Data_Context dataContext;

        public Customer_Service()
        {
            this.dataContext = new Data_Context();
        }


        public List<Customer> GetCustomers()
        {
            return dataContext.Customers.ToList();
        }


        public async Task<bool> AddCustomer(RegisterViewModel model)
        {
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8,"application/json");
            
            HttpResponseMessage respose = await client.PostAsync("http://localhost:53854/api/Register", content);

            if (respose.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> LogUser(LoginViewModel model)
        {
            var logindetails = new
            {
                UserName = model.Email,
                Password = model.Password
            };

           HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(logindetails);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage respose = await client.PostAsync("http://localhost:53854/api/LogIn", content);

            if (respose.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }



        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                dataContext.Entry(customer).State = EntityState.Modified;
                dataContext.SaveChanges();
                return true;


            }
            catch (Exception ex)
            { return false; }
        }
        public Customer GetCustomer(string email)
        {

          Customer Customerobj =  dataContext.Customers.Where(x => x.Email.Equals(email)).FirstOrDefault();

            return Customerobj;
        }

        public string GetGender(string id_num)
        {
            if (Convert.ToInt16(id_num.Substring(7, 1)) >= 5)
                return "Male";
            else
                return "Female";
        }
    }
}
