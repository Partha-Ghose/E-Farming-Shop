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
    public class AdminBlogController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: AdminBlog
        public ActionResult Index()
        {
            return View(db.Tbl_Blog.ToList());
        }

        // GET: AdminBlog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_Blog cat = db.Tbl_Blog.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // GET: AdminBlog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminBlog/Create
        [HttpPost]
        public ActionResult Create(Tbl_Blog category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(category.BImageFill.FileName);
                    string extension = Path.GetExtension(category.BImageFill.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    category.BlogImage = "~/BlogImg/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/BlogImg/"), fileName);
                    category.BImageFill.SaveAs(fileName);

                    category.BlCreatedDate = DateTime.Now;

                    //int temp = (int)(Session["BId"]);
                    //category.BId = (int)(Session["BId"]);

                    db.Tbl_Blog.Add(category);
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

        // GET: AdminBlog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_Blog cat = db.Tbl_Blog.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        // POST: AdminBlog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tbl_Blog category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(category.BImageFill.FileName);
                    string extension = Path.GetExtension(category.BImageFill.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    category.BlogImage = "~/BlogImg/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/BlogImg/"), fileName);
                    category.BImageFill.SaveAs(fileName);

                    category.BlCreatedDate = DateTime.Now;

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

        // GET: AdminBlog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_Blog cat = db.Tbl_Blog.Find(id);
            db.Tbl_Blog.Remove(cat);
            db.SaveChanges();
            if (cat == null)
                return HttpNotFound();
            return RedirectToAction("Index");
        }

        // POST: AdminBlog/Delete/5
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
