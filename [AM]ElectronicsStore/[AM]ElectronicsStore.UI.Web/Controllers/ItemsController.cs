using _AM_ElectronicsStore.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _AM_ElectronicsStore.UI.Web.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        public ActionResult Index()
        {
            var items = ItemsBLL.GetAll(null, null);
            ViewBag.Categories = new[] { new SelectListItem { Text = "Категория не выбрана", Value = "0", Selected = true } }.Concat(
                CategoriesBLL.GetAll().Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                }));
            return View(items);
        }

        [HttpPost]
        public ActionResult Index(string itemName, int? categoryId)
        {
            var items = ItemsBLL.GetAll(categoryId, itemName);
            ViewBag.Categories = new[] { new SelectListItem { Text = "Категория не выбрана", Value = "0", Selected = true } }.Concat(
                CategoriesBLL.GetAll().Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                }));
            return View(items);
        }

        [Authorize]
        public ActionResult Get(int id)
        {
            var item = ItemsBLL.GetById(id);
            return View(item);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int id)
        {
            ItemsBLL.Delete(id);
            TempData["Success"] = "Товар успешно удален.";
            return RedirectToAction("Index");
        }
    }
}