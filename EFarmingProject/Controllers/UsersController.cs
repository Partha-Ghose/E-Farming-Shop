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
    public class UsersController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: Users
        public ActionResult Index()
        {
            var users = db.Tbl_BuyerProfile.ToList();
            return View(users);
        }
        public ActionResult Details(int id)
        {
            var user = db.Tbl_BuyerProfile.Single(x => x.BId == id);
            return View(user);
        }
        public ActionResult ActivateSeller(int id)
        {
            var user = db.Tbl_BuyerProfile.Find(id);
            user.IsSellerActivated = true;
            user.BModifiedDate = DateTime.Now;
            user.BConfirmPassword = user.BPassword;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ActivateBuyer(int id)
        {
            try
            {
                var user = db.Tbl_BuyerProfile.Find(id);
                user.IsBuyerActivated = true;
                user.BModifiedDate = DateTime.Now;
                user.BConfirmPassword = user.BPassword;
                //db.Tbl_BuyerProfile.Add(user);
                //db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
        public ActionResult DeactivateSeller(int id)
        {
            var user = db.Tbl_BuyerProfile.Single(x => x.BId == id);
            user.IsSellerActivated = false;
            user.BConfirmPassword = user.BPassword;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeactivateBuyer(int id)
        {
            var user = db.Tbl_BuyerProfile.Single(x => x.BId == id);
            user.IsBuyerActivated = false;
            user.BConfirmPassword = user.BPassword;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            using (dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities())
            {
                
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                Tbl_BuyerProfile cat = db.Tbl_BuyerProfile.Find(id);
                if (cat == null)
                    return HttpNotFound();
                return View(cat);
            }

        }
        [HttpPost]
        public ActionResult Edit(Tbl_BuyerProfile category)
        {
            dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(category.ImageFile.FileName);
                    string extension = Path.GetExtension(category.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    category.BImage = "~/BImage/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/BImage/"), fileName);
                    category.ImageFile.SaveAs(fileName);

                    category.BModifiedDate = DateTime.Now;
                    category.NidTypeId = 1;
                    category.DistrictId = 1;
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

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tbl_BuyerProfile cat = db.Tbl_BuyerProfile.Find(id);
            db.Tbl_BuyerProfile.Remove(cat);
            db.SaveChanges();
            if (cat == null)
                return HttpNotFound();
            return RedirectToAction("Index");
        }

    }
}