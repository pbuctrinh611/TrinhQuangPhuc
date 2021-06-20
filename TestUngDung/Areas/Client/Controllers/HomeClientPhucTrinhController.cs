using ModelEF.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Client.Controllers
{
    public class HomeClientPhucTrinhController : Controller
    {
        // GET: Client/HomeClientPhucTrinh
        public ActionResult Index()
        {
            var id = new ProductDAO();
            var model = id.ListAll();
            return View(model);
        }
    }
}