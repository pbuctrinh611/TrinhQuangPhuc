using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelEF.Model;
using ModelEF.DAO;
using PagedList;
using System.Net;
using System.IO;
using System.Data.Entity;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private TrinhQuangPhucContext db = new TrinhQuangPhucContext();
        // GET: Admin/Product
        [HttpGet]
        public ActionResult Index(string SearchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDAO();
            var model = dao.ListWhereAll(SearchString);
            ViewBag.SearchString = SearchString;
            return View(model.ToPagedList(page, pageSize));
        }
        /*[HttpPost]
        public ActionResult Index(string SearchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductDAO();
            var model = dao.ListWhereAll(SearchString, page, pageSize);
            ViewBag.SearchString = SearchString;
            return View(model.ToPagedList(page, pageSize));
        }*/
        // GET: Admin/KhuyenMais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: Admin/pro/Create
        public ActionResult Create()
        {
            ViewBag.CateID = new SelectList(db.Categories, "CateID", "Name");
            return View();
        }

        // POST: Admin/pro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,UnitCost,Quantity,Image,Description,Status,CateID")] Product product, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = int.Parse(db.Products.ToList().Last().ProductID.ToString());
                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "product" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Assets/Admin/Image/"), _FileName);
                    uploadhinh.SaveAs(_path);
                    Product ulsp = db.Products.FirstOrDefault(x => x.ProductID == id);
                    ulsp.Image = _FileName;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            ViewBag.CateID = new SelectList(db.Categories, "CateID", "Name", product.CateID);
            return View(product);
        }

        // GET: Admin/pro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CateID = new SelectList(db.Categories, "CateID", "Name", product.CateID);
            return View(product);
        }

        // POST: Admin/pro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,UnitCost,Quantity,Image,Description,Status,CateID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateID = new SelectList(db.Categories, "CateID", "Name", product.CateID);
            return View(product);
        }

        // GET: Admin/pro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/pro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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