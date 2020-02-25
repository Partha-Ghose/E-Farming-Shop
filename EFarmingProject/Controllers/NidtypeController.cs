using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class NidtypeController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: District
        public ActionResult Index()
        {
            return View(db.Tbl_NidType.ToList());
        }

        // GET: District/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_NidType cat = db.Tbl_NidType.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // GET: District/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: District/Create
        [HttpPost]
        public ActionResult Create(Tbl_NidType category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Tbl_NidType.Add(category);
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

        // GET: District/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_NidType cat = db.Tbl_NidType.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // POST: District/Edit/5
        [HttpPost]
        public ActionResult Edit(Tbl_NidType category)
        {
            try
            {
                if (ModelState.IsValid)
                {
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

        // GET: District/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_NidType cat = db.Tbl_NidType.Find(id);
            db.Tbl_NidType.Remove(cat);
            db.SaveChanges();
            if (cat == null)
                return HttpNotFound();
            return RedirectToAction("Index");
        }

        // POST: District/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
