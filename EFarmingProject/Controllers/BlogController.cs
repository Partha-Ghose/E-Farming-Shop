using EFarmingProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class BlogController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: Blog
        public ActionResult Index(int? page)
        {
            return View(db.Tbl_Blog.ToList().OrderByDescending(x => x.BlCreatedDate).ToPagedList(page ?? 1, 3));
        }
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    Tbl_Blog cat = db.Tbl_Blog.Find(id);
        //    var blogcontent = new ViewBlogComment()

        //        Blog = db.Tbl_Blog.ToList();
        //       Comment = db.Tbl_Comment.ToList();

        //    if (cat == null)
        //        return HttpNotFound();

        //    return View(cat);
        //}

        //GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var blogcontent = new ViewBlogComment()
            {
                Blog = db.Tbl_Blog.Single(x => x.BLogId == id),
                Comment = db.Tbl_Comment.Where(x => x.BLogId == id).ToList(),
            };
            Session["ID"] = id;
            if (blogcontent == null)
                return HttpNotFound();

            return View(blogcontent);
        }

        //public ActionResult Comment(int? id, Tbl_Comment comment)
        //{
        //    // save to database
        //    // comment table e comment ta save krte hbe
        //    if (ModelState.IsValid)
        //    {
        //        comment.BLogId = id;
        //        db.Tbl_Comment.Add(comment);
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("Details", new { id = id });
        //}

        // GET: Blog/Create
        [Authorize]
        public ActionResult Comment()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [Authorize]
        public ActionResult Comment(Tbl_Comment category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.CommentDate = DateTime.Now;
                    category.BId = (int)Session["BId"];
                    category.BLogId = (int)(Session["ID"]);
                    db.Tbl_Comment.Add(category);
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

        // GET: Blog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Blog/Edit/5
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

        // GET: Blog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Blog/Delete/5
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
