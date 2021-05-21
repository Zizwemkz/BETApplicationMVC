using BETApplicationMVC.Shopify.Data;
using BETApplicationMVC.Shopify.Logic;
using BETApplicationMVC.Shopify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BETApplicationMVC.Shopify.Controllers
{
    public class apiDepartmentsController : ApiController
    {
        Department_Service department_Service;
        public apiDepartmentsController()
        {
            this.department_Service = new Department_Service();
        }
        // GET: api/apiDepartments
        public List<Department> GetDepartments()
        {
            return department_Service.GetDepartments().Select(x => new Department()
            {
                Department_Name = x.Department_Name,
                Description = x.Description,
                Department_ID = x.Department_ID
            }).ToList();
        }

    }
}
