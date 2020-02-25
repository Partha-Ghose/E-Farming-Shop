using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class CategoryController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View(db.Tbl_Category.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_Category cat = db.Tbl_Category.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // GET: Category/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Tbl_Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.CreatedDate = DateTime.Now;
                    db.Tbl_Category.Add(category);
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

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_Category cat = db.Tbl_Category.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Tbl_Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.ModifiedDate = DateTime.Now;
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

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_Category cat = db.Tbl_Category.Find(id);
            db.Tbl_Category.Remove(cat);
            db.SaveChanges();
            if (cat == null)
                return HttpNotFound();
            return RedirectToAction("Index");
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Tbl_Category category)
        {
            Tbl_Category cate = new Tbl_Category();
            try
            {
                if(ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    cate = db.Tbl_Category.Find(id);
                    if (cate == null)
                        return HttpNotFound();
                    db.Tbl_Category.Remove(cate);
                    db.SaveChanges();
                }
                //return View(cate);
                return RedirectToAction("Index");
                // TODO: Add delete logic here
            }
            catch
            {
                return View();
            }
        }
    }
}
