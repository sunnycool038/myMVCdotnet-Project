﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider AuthProvider;
        public AccountController(IAuthProvider Auth)
        {
            AuthProvider = Auth;
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (AuthProvider.Authenticate(model.username, model.password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }else
                {
                    ModelState.AddModelError("", "Incorrect Username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}