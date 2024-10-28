using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPhongKham.Models;

namespace WebPhongKham.Controllers
{
    public class DATLICHesController : Controller
    {
        private qlpkEntities2 db = new qlpkEntities2();

        // GET: DATLICHes
        public ActionResult Index(string searchString)
        {
            var dATLICHes = db.DATLICHes.Include(d => d.BACSI).Include(d => d.XETNGHIEM);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                dATLICHes = dATLICHes.Where(n => n.TenKH.ToLower().Contains(searchString));
            }
            return View(dATLICHes.ToList());
        }

        // GET: DATLICHes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DATLICH dATLICH = db.DATLICHes.Find(id);
            if (dATLICH == null)
            {
                return HttpNotFound();
            }
            return View(dATLICH);
        }

        // GET: DATLICHes/Create
        public ActionResult Create(/*string MaCK*/)
        {
            /*if (MaCK == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bacSi = db.BACSIs.Where(b => b.MaCK == MaCK);
            if (bacSi == null)
            {
                return HttpNotFound();
            }*/
            ViewBag.ChuyenKhoaID = new SelectList(db.CHUYENKHOAs, "ChuyenKhoaID", "TenCK");
            ViewBag.XetNghiemID = new SelectList(db.XETNGHIEMs, "XetNghiemID", "TenXetNghiem");
            ViewBag.BacSiID = new SelectList(db.BACSIs, "BacSiID", "TenBS");
            return View();
        }

        // POST: DATLICHes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDLKH,TenKH,XetNghiemID,NgaySinh,GioiTinh,DiaChi,SDT,ChuyenKhoaID,BacSiID,NgayKham,GioKham")] DATLICH dATLICH)
        {
            if (ModelState.IsValid)
            {
                db.DATLICHes.Add(dATLICH);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            
            ViewBag.ChuyenKhoaID = new SelectList(db.CHUYENKHOAs, "ChuyenKhoaID", "TenCK", dATLICH.ChuyenKhoaID);
            ViewBag.XetNghiemID = new SelectList(db.XETNGHIEMs, "XetNghiemID", "TenXetNghiem", dATLICH.XetNghiemID);
            ViewBag.BacSiID = new SelectList(db.BACSIs, "BacSiID", "TenBS", dATLICH.BacSiID);
            return View(dATLICH);
        }

        // GET: DATLICHes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DATLICH dATLICH = db.DATLICHes.Find(id);
            if (dATLICH == null)
            {
                return HttpNotFound();
            }
            ViewBag.BacSiID = new SelectList(db.BACSIs, "BacSiID", "TenBS", dATLICH.BacSiID);
            return View(dATLICH);
        }

        // POST: DATLICHes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDLKH,TenKH,XetNghiemID,NgaySinh,GioiTinh,DiaChi,SDT,ChuyenKhoaID,BacSiID,NgayKham,GioKham")] DATLICH dATLICH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dATLICH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BacSiID = new SelectList(db.BACSIs, "MaBS", "TenBS", dATLICH.BacSiID);
            ViewBag.XetNghiemID = new SelectList(db.XETNGHIEMs, "XetNghiemID", "TenXetNghiem", dATLICH.XetNghiemID);
            return View(dATLICH);
        }

        // GET: DATLICHes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DATLICH dATLICH = db.DATLICHes.Find(id);
            if (dATLICH == null)
            {
                return HttpNotFound();
            }
            return View(dATLICH);
        }

        // POST: DATLICHes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DATLICH dATLICH = db.DATLICHes.Find(id);
            db.DATLICHes.Remove(dATLICH);
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

        public ActionResult AdminCreate(/*string MaCK*/)
        {
            /*if (MaCK == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bacSi = db.BACSIs.Where(b => b.MaCK == MaCK);
            if (bacSi == null)
            {
                return HttpNotFound();
            }*/
            ViewBag.ChuyenKhoaID = new SelectList(db.CHUYENKHOAs, "ChuyenKhoaID", "TenCK");
            ViewBag.XetNghiemID = new SelectList(db.XETNGHIEMs, "XetNghiemID", "TenXetNghiem");
            ViewBag.BacSiID = new SelectList(db.BACSIs, "BacSiID", "TenBS");
            return View();
        }

        // POST: DATLICHes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminCreate([Bind(Include = "MaDLKH,TenKH,XetNghiemID,NgaySinh,GioiTinh,DiaChi,SDT,ChuyenKhoaID,BacSiID,NgayKham,GioKham")] DATLICH dATLICH)
        {
            if (ModelState.IsValid)
            {
                db.DATLICHes.Add(dATLICH);
                db.SaveChanges();
                return RedirectToAction("Index","DATLICHes");
            }

            ViewBag.ChuyenKhoaID = new SelectList(db.CHUYENKHOAs, "ChuyenKhoaID", "TenCK", dATLICH.ChuyenKhoaID);
            ViewBag.XetNghiemID = new SelectList(db.XETNGHIEMs, "XetNghiemID", "TenXetNghiem", dATLICH.XetNghiemID);
            ViewBag.BacSiID = new SelectList(db.BACSIs, "BacSiID", "TenBS", dATLICH.BacSiID);
            return View(dATLICH);
        }
    }
}
