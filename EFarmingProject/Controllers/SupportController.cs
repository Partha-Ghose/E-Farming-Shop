using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class SupportController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
       

        // GET: Support/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Support/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Tbl_Support category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(category.ImageFils.FileName);
                    string extension = Path.GetExtension(category.ImageFils.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    category.ProductImage = "~/SupportImg/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/SupportImg/"), fileName);
                    category.ImageFils.SaveAs(fileName);

                    category.CreatedDate = DateTime.Now;
                    int temp = (int)(Session["BId"]);
                    category.BId = (int)(Session["BId"]);

                    db.Tbl_Support.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
                // TODO: Add insert logic here

                return View(category);
            }
            catch
            {
                return View();
            }
        }

       
    }
}
