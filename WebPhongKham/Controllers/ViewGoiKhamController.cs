using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPhongKham.Models;

namespace WebPhongKham.Controllers
{
    public class ViewGoiKhamController : Controller
    {
        private qlpkEntities2 db = new qlpkEntities2();

        // GET: ViewGoiKham
        public ActionResult Index()
        {
            var gOIKHAMs = db.GOIKHAMs;
           
            return View(gOIKHAMs.ToList());
        }
        public ActionResult ChiTietGoiKham(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOIKHAM gOIKHAM = db.GOIKHAMs.Find(id);
            if (gOIKHAM == null)
            {
                return HttpNotFound();
            }
            return View(gOIKHAM);
        }
    }
}