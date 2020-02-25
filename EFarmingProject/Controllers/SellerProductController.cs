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
    public class SellerProductController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        //GET: SellerProduct
        [Authorize]
        public ActionResult Index()
        {
            var t = (int)(Session["BId"]);
            var user = db.Tbl_BuyerProfile.Single(x => x.BId == t);
            var temp = user.IsSeller;
            if ((bool)temp && temp != null)
            {
                List<Tbl_Category> list = db.Tbl_Category.ToList();
                ViewBag.Catlist = new SelectList(list, "CategoryId", "CategoryName");
                return View(db.Tbl_SellerProduct.Where(x=>x.BId==t).ToList());
            }
            else
            {
                ViewBag.Message = "You don't have enough permission. Please contact admin panel";
                return View();
            }

        }

        //GET: SellerProduct/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_SellerProduct cat = db.Tbl_SellerProduct.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // GET: SellerProduct/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var IsSeller = Session["RoleAsSeller"];
            if ((bool)IsSeller && IsSeller != null)
            {
                List<Tbl_Category> list = db.Tbl_Category.ToList();
                ViewBag.Catelist = new SelectList(list, "CategoryId", "CategoryName");
                return View();
            }
            else
            {
                ViewBag.Message = "You don't have enough permission. Please contact admin panel";
                return View();
            }

        }

        // POST: SellerProduct/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(Tbl_SellerProduct category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(category.ImgFile.FileName);
                    string extension = Path.GetExtension(category.ImgFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    category.SProductImage = "~/SProductImg/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/SProductImg/"), fileName);
                    category.ImgFile.SaveAs(fileName);

                    category.SCreatedDate = DateTime.Now;
                    int temp = (int)(Session["BId"]);
                    category.BId = (int)(Session["BId"]);

                    db.Tbl_SellerProduct.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                // TODO: Add insert logic here
                return View(category);
            }
            catch
            {
                return View();
            }
        }

        // GET: SellerProduct/Edit/5
        public ActionResult Edit(int? id)
        {
            List<Tbl_Category> list = db.Tbl_Category.ToList();
            ViewBag.Catlist = new SelectList(list, "CategoryId", "CategoryName");
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_SellerProduct cat = db.Tbl_SellerProduct.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // POST: SellerProduct/Edit/5
        [HttpPost]
        public ActionResult Edit(Tbl_SellerProduct category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(category.ImgFile.FileName);
                    string extension = Path.GetExtension(category.ImgFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    category.SProductImage = "~/ProductImg/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/ProductImg/"), fileName);
                    category.ImgFile.SaveAs(fileName);

                    category.SModifiedDate = DateTime.Now;
                    //category.BId = (int)(Session["BId"]);
                    category.IsAdminApproved = false;
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

        //// GET: SellerProduct/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: SellerProduct/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
