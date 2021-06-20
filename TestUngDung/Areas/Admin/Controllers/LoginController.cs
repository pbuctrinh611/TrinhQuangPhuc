﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Areas.Admin.Models;
using ModelEF.DAO;
using TestUngDung.Common;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        /* public ActionResult Index(LoginModel user)
         {
             if (ModelState.IsValid)
             {
                 var dao = new UserAccountDAO();
                 var result = dao.login(user.UserName, Encryptor.EncryptMD5(user.Password));
                 if (result == 1)
                 {
                     ModelState.AddModelError("", "Đăng nhập thành công");
                     Session.Add(Constanst.USER_SESSION, user.UserName);
                     return RedirectToAction("Index", "Home");
                 }
                 else
                 {
                     ModelState.AddModelError("", "Đăng nhập không thành công");
                 }
             }
             return View("Index");
         }*/
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserAccountDAO();
                var result = dao.login(model.UserName, Encryptor.EncryptMD5(model.Password));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new LoginModel();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.UserID;
                     
                    Session.Add(Constanst.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                } else if (result == 0)
                {
                    ModelState.AddModelError("","Tài khoản không tồn tại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("","Tài khoản đang bị khóa!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("","Mật khẩu không chính xác!");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
            }
            return View("Index");
        }
    }
}