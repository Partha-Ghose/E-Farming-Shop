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
    public class ProductController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: Product
        public ActionResult Index()
        {
            List<Tbl_Category> list = db.Tbl_Category.ToList();
            ViewBag.Catlist = new SelectList(list, "CategoryId", "CategoryName");
            return View(db.Tbl_SellerProduct.Where(x=>x.IsAdminApproved==true).ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_SellerProduct cat = db.Tbl_SellerProduct.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // GET: Product/Create
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    List<Tbl_Category> list = db.Tbl_Category.ToList();
        //    ViewBag.Catlist = new SelectList(list, "CategoryId", "CategoryName");
        //    return View();
        //}

        //// POST: Product/Create
        //[HttpPost]
        //public ActionResult Create(Tbl_SellerProduct category)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            string fileName = Path.GetFileNameWithoutExtension(category.ImgFile.FileName);
        //            string extension = Path.GetExtension(category.ImgFile.FileName);
        //            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
        //            category.SProductImage = "~/ProductImg/" + fileName;
        //            fileName = Path.Combine(Server.MapPath("~/ProductImg/"), fileName);
        //            category.ImgFile.SaveAs(fileName);

        //            db.Tbl_SellerProduct.Add(category);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        // TODO: Add insert logic here
        //        return View(category);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Product/Edit/5
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

        // POST: Product/Edit/5
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

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_SellerProduct cat = db.Tbl_SellerProduct.Find(id);
            db.Tbl_SellerProduct.Remove(cat);
            db.SaveChanges();
            if (cat == null)
                return HttpNotFound();
            return RedirectToAction("Index");
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Tbl_SellerProduct category)
        {
            Tbl_SellerProduct cate = new Tbl_SellerProduct();
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    cate = db.Tbl_SellerProduct.Find(id);
                    if (cate == null)
                        return HttpNotFound();
                    db.Tbl_SellerProduct.Remove(cate);
                    db.SaveChanges();
                }
                return View(cate);
                // TODO: Add delete logic here
            }
            catch
            {
                return View();
            }
        }
    }
}
