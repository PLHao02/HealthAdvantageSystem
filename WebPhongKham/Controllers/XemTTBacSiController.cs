using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPhongKham.Models;

namespace WebPhongKham.Controllers
{
    public class XemTTBacSiController : Controller
    {
        private qlpkEntities2 db = new qlpkEntities2();
        // GET: XemTTBacSi
        public ActionResult Index()
        {
            var bACSIs = db.BACSIs;
            return View(bACSIs.ToList());
        }
    }
}