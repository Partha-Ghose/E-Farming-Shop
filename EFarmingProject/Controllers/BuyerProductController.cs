using EFarmingProject.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{

    public class BuyerProductController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: BuyerProduct
        [Authorize]
        public ActionResult Index(int? i)
        {
            var IsBuyer = Session["RoleAsBuyer"];
            if ((bool)IsBuyer && IsBuyer != null)
            {
                return View(db.Tbl_SellerProduct.Where(x => x.IsAdminApproved == true && x.IsAdminApproved != null).ToList().OrderByDescending(x => x.SCreatedDate).ToPagedList(i ?? 1, 8));
            }
            else
            {
                ViewBag.Message = "You don't have enough permission. Please contact admin panel";
                return View();
            }
        }
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cat = db.Tbl_SellerProduct.Find(id);
            if (cat == null)
                return HttpNotFound();
            return View(cat);
        }

        [Authorize]
        public ActionResult ProfileView(int id)
        {
            return RedirectToAction("ProductOwnerProfile", "BuyerView", new { id = id });
        }
        
        public ActionResult Search(string id,int? i)
        {

            return View(db.Tbl_SellerProduct.Where(x => x.Tbl_Category.CategoryName.StartsWith(id) || id == null).ToList().OrderByDescending(x => x.SCreatedDate).ToPagedList(i ?? 1, 8));
        }

    }
}