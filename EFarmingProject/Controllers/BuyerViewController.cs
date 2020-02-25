using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class BuyerViewController : Controller
    {
        // GET: BuyerView
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult BuyerProfile()
        {
            Tbl_BuyerProfile imagemodel = new Tbl_BuyerProfile();
            int id = (int)(Session["BId"]);
            using (dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities())
            {
                imagemodel = db.Tbl_BuyerProfile.Where(x => x.BId == id).FirstOrDefault();
                Session["BId"] = imagemodel.BId;
            }
            return View(imagemodel);
        }

        [Authorize]
        public ActionResult ProductOwnerProfile(int id)
        {
            using (dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities())
            {
                var profile = db.Tbl_BuyerProfile.Single(x=>x.BId == id);
                return View(profile);
            }
        }

        public ActionResult Edit(int? id)
        {
            using (dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities())
            {
                List<Tbl_District> list = db.Tbl_District.ToList();
                ViewBag.Dislist = new SelectList(list, "DistrictId", "DistrictType");

                List<Tbl_NidType> listt = db.Tbl_NidType.ToList();
                ViewBag.Nidlist = new SelectList(listt, "NidTypeId", "NidType");

                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Tbl_BuyerProfile cat = db.Tbl_BuyerProfile.Find(id);
                if (cat == null)
                    return HttpNotFound();
                return View(cat);
            }
                
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Tbl_BuyerProfile category)
        {
            dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(category.ImageFile.FileName);
                    string extension = Path.GetExtension(category.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    category.BImage = "~/BImage/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/BImage/"), fileName);
                    category.ImageFile.SaveAs(fileName);

                    category.BModifiedDate = DateTime.Now;
                    category.NidTypeId = 1;
                    category.DistrictId = 1;
                    db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                // TODO: Add update logic here

                return View(category);
            }
            catch
            {
                return View();
            }
        }

    }
}