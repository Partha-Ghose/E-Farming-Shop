using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class AdminSellerController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: SellerProduct
        public ActionResult Index()
        {
            List<Tbl_Category> list = db.Tbl_Category.ToList();
            ViewBag.Catelist = new SelectList(list, "CategoryId", "CategoryName");
            return View(db.Tbl_SellerProduct.Where(x => x.IsAdminApproved == false || x.IsAdminApproved == null).ToList());
        }

        //GET: SellerProduct/Details

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
        //[HttpGet]
        //public ActionResult Create()
        //{
        //    List<Tbl_Category> list = db.Tbl_Category.ToList();
        //    ViewBag.Catelist = new SelectList(list, "CategoryId", "CategoryName");
        //    return View();
        //}

        //// POST: SellerProduct/Create
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
        //            category.SProductImage = "~/SProductImg/" + fileName;
        //            fileName = Path.Combine(Server.MapPath("~/SProductImg/"), fileName);
        //            category.ImgFile.SaveAs(fileName);

        //            category.SCreatedDate = DateTime.Now;

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

        // GET: SellerProduct/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: SellerProduct/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: SellerProduct/Delete/5
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

        // POST: SellerProduct/Delete/5
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

        public ActionResult Acitivate(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = db.Tbl_SellerProduct.Find(id);
            product.IsAdminApproved = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}