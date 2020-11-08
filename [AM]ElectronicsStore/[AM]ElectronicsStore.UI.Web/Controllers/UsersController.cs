using _AM_ElectronicsStore.BLL;
using _AM_ElectronicsStore.Entities;
using _AM_ElectronicsStore.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _AM_ElectronicsStore.UI.Web.Controllers
{
    public class UsersController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var users = UsersBLL.GetAll();
            return View(users);
        }

        public ActionResult Registr()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registr(User model)
        {
            if (ModelState.IsValid)
            {
                model.RoleId = Entities.RoleUser.RoleType.User;
                model.Password = Entities.User.GetStringHash(model.Password);
                UsersBLL.AddOrUpdate(model);
                TempData["Success"] = "Регистрация прошла успешно.";
                return RedirectToAction("Index", "Items");
            }
            return RedirectToAction("Registr", model);
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var user = UsersBLL.GetByLogin(model.Login);
            if (user != null && Entities.User.VerifyPassword(user.Password, model.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(model.Login, false);
                return Redirect("/");
            }
            TempData["Error"] = "Невозможно войти в систему с указанными данными";
            return RedirectToAction("Index", "Items");
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Items");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            var user = UsersBLL.GetByLogin(User.Identity.Name);
            if (user != null && Entities.User.VerifyPassword(user.Password, model.OldPassword))
            {
                UsersBLL.UpdatePassword(user.Id, Entities.User.GetStringHash(model.NewPassword));
                TempData["Success"] = "Пароль успешно изменен.";
                return Redirect("/");
            }
            TempData["Error"] = "Невозможно изменить пароль. Старый пароль не верен.";
            return View();
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (User.IsInRole("User"))
            {
                UsersBLL.Delete(UsersBLL.GetByLogin(User.Identity.Name).Id);
                FormsAuthentication.SignOut();
                TempData["Success"] = "Пользователь успешно удален";
                return RedirectToAction("Index", "Items");
            }
            if(User.IsInRole("Admin") && id.HasValue)
            {
                var user = UsersBLL.GetById(id.Value);
                if(user != null)
                {
                    UsersBLL.Delete(user.Id);
                    TempData["Success"] = "Пользователь успешно удален";
                    return RedirectToAction("Index");
                }
            }
            TempData["Success"] = "Ошибка при удалении пользователя";
            return RedirectToAction("Index", "items");
        }
    }
}