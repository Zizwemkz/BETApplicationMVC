using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BETApplicationMVC.Shopify.Data;
using BETApplicationMVC.Shopify.Logic;
using BETApplicationMVC.Shopify.Models;

namespace BETApplicationMVC.Shopify.Controllers
{
    //[RoutePrefix("api/apiCintroller")]
    public class apiCategoriesController : ApiController
    {
        private Data_Context db;
        Category_Service category_Service;

        public apiCategoriesController()
        {
            this.db = new Data_Context();
            this.category_Service = new Category_Service();
        }
        // GET: api/apiCategories
        public IEnumerable<CategoryModel> GetCategories(int id)
        {
            return category_Service.GetCategories().Where(p => p.Department_ID == id).Select(x => new CategoryModel()
            {
                Category_ID = x.Category_ID,
                Name = x.Name,
                Department_ID = x.Department_ID
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Category_ID == id) > 0;
        }
    }
}