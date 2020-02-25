using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class DistrictController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: District
        public ActionResult Index()
        {
            return View(db.Tbl_District.ToList());
        }

        // GET: District/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_District cat = db.Tbl_District.Find(id);
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
        public ActionResult Create(Tbl_District category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Tbl_District.Add(category);
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
            Tbl_District cat = db.Tbl_District.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // POST: District/Edit/5
        [HttpPost]
        public ActionResult Edit(Tbl_District category)
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
            Tbl_District cat = db.Tbl_District.Find(id);
            db.Tbl_District.Remove(cat);
            db.SaveChanges();
            if (cat == null)
                return HttpNotFound();
            return RedirectToAction("Index");
        }

    }
}
