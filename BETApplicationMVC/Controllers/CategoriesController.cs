using BETApplicationMVC.Shopify.Data;
using BETApplicationMVC.Shopify.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BETApplicationMVC.Shopify.Controllers
{
    public class CategoriesController : Controller
    {
        Category_Service Category_Service = new Category_Service();
        Department_Service department_Service = new Department_Service();

        public CategoriesController()
        {

        }
        public async Task<ActionResult> Index()
        {
            return View(await Category_Service.GetAllCategories());
        }      
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (await Category_Service.GetCategory(id) != null)
                return View(await Category_Service.GetCategory(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public async Task<ActionResult> Create()
        {
            ViewBag.Department_ID = new SelectList(await department_Service.GetAllDepartments(), "Department_ID", "Department_Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category model)
        {
            ViewBag.Department_ID = new SelectList(await department_Service.GetAllDepartments(), "Department_ID", "Department_Name");
            if (ModelState.IsValid)
            {
              if( await Category_Service.AddCategory(model))
                return RedirectToAction("Index");
                else
                    return RedirectToAction("Bad_Request", "Error");
            }

            return View(model);
        }
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    ViewBag.Department_ID = new SelectList(await department_Service.GetAllDepartments(), "Department_ID", "Department_Name");
        //    if (id == null)
        //        return RedirectToAction("Bad_Request", "Error");
        //    if (Category_Service.GetCategory(id) != null)
        //        return View(Category_Service.GetCategory(id));
        //    else
        //        return RedirectToAction("Not_Found", "Error");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(Category model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Category_Service.UpdateCategory(model);
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Department_ID = new SelectList(await department_Service.GetAllDepartments(), "Department_ID", "Department_Name");
        //    return View(model);
        //}

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //        return RedirectToAction("Bad_Request", "Error");
        //    if (Category_Service.GetCategory(id) != null)
        //        return View(Category_Service.GetCategory(id));
        //    else
        //        return RedirectToAction("Not_Found", "Error");
        //}
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Category_Service.RemoveCategory(await Category_Service.GetCategory(id));

        //    return RedirectToAction("Index");
        //}
    }
}