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
    public class KEDONTHUOCsController : Controller
    {
        private qlpkEntities2 db = new qlpkEntities2();

        // GET: KEDONTHUOCs
        public ActionResult Index()
        {
            var kEDONTHUOCs = db.KEDONTHUOCs.Include(k => k.THUOC);
            return View(kEDONTHUOCs.ToList());
        }

        // GET: KEDONTHUOCs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KEDONTHUOC kEDONTHUOC = db.KEDONTHUOCs.Find(id);
            if (kEDONTHUOC == null)
            {
                return HttpNotFound();
            }
            return View(kEDONTHUOC);
        }

        // GET: KEDONTHUOCs/Create
        public ActionResult Create()
        {
            ViewBag.THUOCID = new SelectList(db.THUOCs, "THUOCID", "TENTHUOC");
            return View();
        }

        // POST: KEDONTHUOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KeDonThuocID,TenKH,TenBenh,THUOCID,SoLuong")] KEDONTHUOC kEDONTHUOC)
        {
            if (ModelState.IsValid)
            {
                db.KEDONTHUOCs.Add(kEDONTHUOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.THUOCID = new SelectList(db.THUOCs, "THUOCID", "TENTHUOC", kEDONTHUOC.THUOCID);
            return View(kEDONTHUOC);
        }

        // GET: KEDONTHUOCs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KEDONTHUOC kEDONTHUOC = db.KEDONTHUOCs.Find(id);
            if (kEDONTHUOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.THUOCID = new SelectList(db.THUOCs, "THUOCID", "TENTHUOC", kEDONTHUOC.THUOCID);
            return View(kEDONTHUOC);
        }

        // POST: KEDONTHUOCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KeDonThuocID,TenKH,TenBenh,THUOCID,SoLuong")] KEDONTHUOC kEDONTHUOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kEDONTHUOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.THUOCID = new SelectList(db.THUOCs, "THUOCID", "TENTHUOC", kEDONTHUOC.THUOCID);
            return View(kEDONTHUOC);
        }

        // GET: KEDONTHUOCs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KEDONTHUOC kEDONTHUOC = db.KEDONTHUOCs.Find(id);
            if (kEDONTHUOC == null)
            {
                return HttpNotFound();
            }
            return View(kEDONTHUOC);
        }

        // POST: KEDONTHUOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KEDONTHUOC kEDONTHUOC = db.KEDONTHUOCs.Find(id);
            db.KEDONTHUOCs.Remove(kEDONTHUOC);
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
