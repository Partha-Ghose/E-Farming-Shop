using EFarmingProject.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class HomeController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: Home
        public ActionResult Index(string search,int? i)
        {
            var res = db.Tbl_SellerProduct.Where(x => x.Tbl_Category.CategoryName.StartsWith(search) || search == null).ToList().OrderByDescending(x=>x.SCreatedDate).ToPagedList(i ?? 1, 8);
            
            return View(res);
        }

    }
}