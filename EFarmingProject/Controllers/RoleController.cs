using EFarmingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFarmingProject.Controllers
{
    public class RoleController : Controller
    {
        dbMyEFarmingProjectEntities db = new dbMyEFarmingProjectEntities();
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
    }
}