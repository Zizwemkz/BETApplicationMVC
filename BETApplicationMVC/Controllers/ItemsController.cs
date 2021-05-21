using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BETApplicationMVC.Shopify.Data;
using BETApplicationMVC.Shopify.Models;
using BETApplicationMVC.Shopify.Logic;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace BETApplicationMVC.Shopify.Controllers
{
    public class ItemsController : Controller
    {
        private Item_Service item;
        Category_Service category_Service;
        public ItemsController()
        {
            this.item = new Item_Service();
            this.category_Service = new Category_Service();
        }


        public async Task<ActionResult> Index()
        {
            return View(await item.GetAllItems());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (item.GetItemById(id) != null)
                return View(item.GetItemById(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(category_Service.GetCategories(), "Category_ID", "Name");
            return View();
        }
      

        [HttpPost]
        public async Task<ActionResult> Create(Item model, HttpPostedFileBase img_upload)
        {
            ViewBag.Category_ID = new SelectList(category_Service.GetCategories(), "Category_ID", "Name");

            byte[] data = null;
            if (img_upload == null)
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Images/Icons/BETICONJPG.PNG"));

                foreach (string filePath in filePaths)
                {
                    string fileName = Path.GetFileName(filePath);
                    // Convert a C# string to a byte array    
                    data = Encoding.ASCII.GetBytes(fileName);
                }
            }

            else
            {
                data = new byte[img_upload.ContentLength];
                img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            }
         
          
            var itemobj = new Item
            {
                QuantityInStock = model.QuantityInStock,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Category_ID = model.Category_ID,
                Picture = data
            };
            if (itemobj != null)
            {
           var Result = await item.CreateItem(itemobj);
            return RedirectToAction("Index");
            }


           return RedirectToAction("Bad_Request", "Error");
        }
    

        public ActionResult Edit(int? id)
        {
            ViewBag.Category_ID = new SelectList(category_Service.GetCategories(), "Category_ID", "Name");
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (item.GetItem(id) != null)
                return View(item.GetItem(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
     

    }
}
