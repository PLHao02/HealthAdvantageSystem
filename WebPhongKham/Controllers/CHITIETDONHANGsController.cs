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
    public class CHITIETDONHANGsController : Controller
    {
        private qlpkEntities2 db = new qlpkEntities2();

        // GET: CHITIETDONHANGs
        public ActionResult Index(string searchString)
        {
            var cHITIETDONHANGs = db.CHITIETDONHANGs.Include(c => c.DONHANG).Include(c => c.GOIKHAM);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                cHITIETDONHANGs = cHITIETDONHANGs.Where(n => n.DONHANG.TenKH.ToLower().Contains(searchString));
            }
            return View(cHITIETDONHANGs.ToList());
        }

        // GET: CHITIETDONHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDONHANG);
        }

        // GET: CHITIETDONHANGs/Create
        public ActionResult Create()
        {
            ViewBag.DonHangID = new SelectList(db.DONHANGs, "DonHangID", "TenKH");
            ViewBag.GoiKhamID = new SelectList(db.GOIKHAMs, "GoiKhamID", "TenGoiKham");
            return View();
        }

        // POST: CHITIETDONHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTDHID,DonHangID,GoiKhamID,SoLuongSP,TongTien")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETDONHANGs.Add(cHITIETDONHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DonHangID = new SelectList(db.DONHANGs, "DonHangID", "TenKH", cHITIETDONHANG.DonHangID);
            ViewBag.GoiKhamID = new SelectList(db.GOIKHAMs, "GoiKhamID", "TenGoiKham", cHITIETDONHANG.GoiKhamID);
            return View(cHITIETDONHANG);
        }

        // GET: CHITIETDONHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonHangID = new SelectList(db.DONHANGs, "DonHangID", "TenKH", cHITIETDONHANG.DonHangID);
            ViewBag.GoiKhamID = new SelectList(db.GOIKHAMs, "GoiKhamID", "TenGoiKham", cHITIETDONHANG.GoiKhamID);
            return View(cHITIETDONHANG);
        }

        // POST: CHITIETDONHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTDHID,DonHangID,GoiKhamID,SoLuongSP,TongTien")] CHITIETDONHANG cHITIETDONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETDONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonHangID = new SelectList(db.DONHANGs, "DonHangID", "TenKH", cHITIETDONHANG.DonHangID);
            ViewBag.GoiKhamID = new SelectList(db.GOIKHAMs, "GoiKhamID", "TenGoiKham", cHITIETDONHANG.GoiKhamID);
            return View(cHITIETDONHANG);
        }

        // GET: CHITIETDONHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            if (cHITIETDONHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDONHANG);
        }

        // POST: CHITIETDONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHITIETDONHANG cHITIETDONHANG = db.CHITIETDONHANGs.Find(id);
            db.CHITIETDONHANGs.Remove(cHITIETDONHANG);
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
