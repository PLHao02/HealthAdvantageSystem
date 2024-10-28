using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPhongKham.Models;

namespace WebPhongKham.Controllers
{
    public class DatLichController : Controller
    {
        qlpkEntities2 db = new qlpkEntities2();
        // GET: DatLich
        public ActionResult Index()
        {
            ViewBag.MaCK = new SelectList(db.CHUYENKHOAs, "MaCK", "TenCK");
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MABS,TenBS,MaCK,NgaySinhBS,GioiTinhBS,SDTBS")] BACSI bACSI)
        {
            ViewBag.MaCK = new SelectList(db.CHUYENKHOAs, "MaCK", "TenCK", bACSI.ChuyenKhoaID);
            ViewBag.MaBS = new SelectList(db.BACSIs, "MaBS", "TenBS", bACSI.BacSiID);
            return View(bACSI);
        }

















        public ActionResult TiepNhan()
        {
            return View();
        }

    }
}