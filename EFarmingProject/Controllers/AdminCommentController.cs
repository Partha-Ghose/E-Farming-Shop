using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class AdminCommentController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: AdminComment
        public ActionResult Index()
        {
            return View(db.Tbl_Comment.ToList());
        }

        // GET: AdminComment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminComment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminComment/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminComment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminComment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminComment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_Comment cat = db.Tbl_Comment.Find(id);
            db.Tbl_Comment.Remove(cat);
            db.SaveChanges();
            if (cat == null)
                return HttpNotFound();
            return RedirectToAction("Index");
        }

        // POST: AdminComment/Delete/5
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
