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
    public class Department_Service
    {
        private Data_Context dataContext;
        public Department_Service()
        {
            this.dataContext = new Data_Context();
        }


        public async Task<List<DepartmentModel>> GetAllDepartments()
        {            
            HttpClient client = new HttpClient();
           List<DepartmentModel> Departmentobj = new List<DepartmentModel>();
            const string url = "http://localhost:53854/api/Department";
            HttpResponseMessage response = await client.GetAsync($"{url}");
            // response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var DepartmenyResponse = response.Content.ReadAsStringAsync().Result;

                Departmentobj =  JsonConvert.DeserializeObject<List<DepartmentModel>>(DepartmenyResponse);
                return Departmentobj;
            }
            //  return posts;
            return Departmentobj;
        }

        public List<DepartmentModel> GetDepartments()
        {
            return Task.Run(() => GetAllDepartments()).Result;
        }

        public async Task<Department> GetDepartmentById(int? departmentid)
        {
            HttpClient client = new HttpClient();
            Department Departmentobj = new Department();
            const string url = "http://localhost:53854/api/Department";
            HttpResponseMessage response = await client.GetAsync($"{url}/{departmentid}");
            // response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var DepartmenyResponse = response.Content.ReadAsStringAsync().Result;

                Departmentobj = JsonConvert.DeserializeObject<Department>(DepartmenyResponse);
                return Departmentobj;
            }
            //  return posts;
            return Departmentobj;
        }

        public async Task<bool> AddDepartment(DepartmentModel model)
        {
           
                HttpClient client = new HttpClient();

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage respose = await client.PostAsync("http://localhost:53854/api/Department", content);

                if (respose.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
                
           
        }
        //public bool UpdateDepartment(Department department)
        //{
        //    try
        //    {
        //        dataContext.Entry(department).State = EntityState.Modified;
        //        dataContext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    { return false; }
        //}
        //public bool RemoveDepartment(Department department)
        //{
        //    try
        //    {
        //        dataContext.Departments.Remove(department);
        //        dataContext.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    { return false; }
        //}
        //public Department GetDepartment(int? department_id)
        //{


        //    return dataContext.Departments.Find(department_id);
        //}
        //public List<Item> Department_items(int? id)
        //{
        //    return find_by_id(id)Items.ToList();
        //}
    }
}
