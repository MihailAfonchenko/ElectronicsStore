using _AM_ElectronicsStore.BLL;
using _AM_ElectronicsStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _AM_ElectronicsStore.UI.Web.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            var i = CategoriesBLL.GetAll();
            return View();
        }
    }
}