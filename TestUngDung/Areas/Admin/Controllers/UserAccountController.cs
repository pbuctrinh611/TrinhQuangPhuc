using ModelEF.DAO;
using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Common;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: Admin/UserAccount
        private TrinhQuangPhucContext db = new TrinhQuangPhucContext();
        [HttpGet]
        public ActionResult Index(string SearchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserAccountDAO();
            var model = dao.ListWhereAll(SearchString, page, pageSize);
            ViewBag.SearchString = SearchString;
            return View(model.ToPagedList(page, pageSize));
        }


       /* [HttpPost]
        public ActionResult Index(string SearchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserAccountDAO();
            var model = dao.ListWhereAll(SearchString, page, pageSize);
            ViewBag.SearchString = SearchString;
            return View(model.ToPagedList(page, pageSize));
        }*/
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserAccountDAO();
                var encryptedMd5pass = Encryptor.EncryptMD5(user.Password);
                user.Password = encryptedMd5pass;
                int id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Create", "UserAccount");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thành công");
                }
            }
            return View("Create");
        }





        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }


        // GET: Admin/u/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }


        // POST: Admin/u/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Password,Status")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAccount);
        }

        // GET: Admin/u/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.UserAccounts.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }
            return View(userAccount);
        }

        // POST: Admin/u/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccount userAccount = db.UserAccounts.Find(id);
            db.UserAccounts.Remove(userAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}