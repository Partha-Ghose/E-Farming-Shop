using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class AdminSupportController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: AdminSupport
        public ActionResult Index()
        {
            return View(db.Tbl_Support.ToList());
        }

        // GET: AdminSupport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_Support cat = db.Tbl_Support.Find(id);
            db.Tbl_Support.Remove(cat);
            db.SaveChanges();
            if (cat == null)
                return HttpNotFound();
            return RedirectToAction("Index");
        }

        // POST: AdminSupport/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int? id, Tbl_Support category)
        //{
        //    try
        //    {
        //        Tbl_Support cate = new Tbl_Support();
        //        if (ModelState.IsValid == false)
        //        {
        //            if (id == null)
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            using (var context = new dbMyEFarmingProjectEntities())
        //            {
        //                var result = context.Tbl_Support.Where(x => x.SupportId == id).FirstOrDefault();
        //                if (result != null)
        //                {
        //                    context.Tbl_Support.Remove(result);
        //                    context.SaveChanges();
        //                }
        //                else
        //                {
        //                    return HttpNotFound();
        //                }
        //            }
        //        }
        //        return View(cate);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
