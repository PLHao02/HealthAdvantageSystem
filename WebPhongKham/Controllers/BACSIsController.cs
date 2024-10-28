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
    public class BACSIsController : Controller
    {
        private qlpkEntities2 db = new qlpkEntities2();

        // GET: BACSIs
        public ActionResult Index(string searchString)
        {
            var bACSIs = db.BACSIs.Include(b => b.CHUYENKHOA);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                bACSIs = bACSIs.Where(n => n.TenBS.ToLower().Contains(searchString));
            }
            return View(bACSIs.ToList());
        }

        // GET: BACSIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            return View(bACSI);
        }

        // GET: BACSIs/Create
        public ActionResult Create()
        {
            ViewBag.ChuyenKhoaID = new SelectList(db.CHUYENKHOAs, "ChuyenKhoaID", "TenCK");
            return View();
        }

        // POST: BACSIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBS,TenBS,ChuyenKhoaID,NgaySinhBS,GioiTinhBS,SDTBS")] BACSI bACSI)
        {
            if (ModelState.IsValid)
            {
                db.BACSIs.Add(bACSI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChuyenKhoaID = new SelectList(db.CHUYENKHOAs, "ChuyenKhoaID", "TenCK", bACSI.ChuyenKhoaID);
            return View(bACSI);
        }

        // GET: BACSIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChuyenKhoaID = new SelectList(db.CHUYENKHOAs, "ChuyenKhoaID", "TenCK", bACSI.ChuyenKhoaID);
            return View(bACSI);
        }

        // POST: BACSIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBS,TenBS,ChuyenKhoaID,NgaySinhBS,GioiTinhBS,SDTBS")] BACSI bACSI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bACSI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChuyenKhoaID = new SelectList(db.CHUYENKHOAs, "ChuyenKhoaID", "TenCK", bACSI.ChuyenKhoaID);
            return View(bACSI);
        }

        // GET: BACSIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BACSI bACSI = db.BACSIs.Find(id);
            if (bACSI == null)
            {
                return HttpNotFound();
            }
            return View(bACSI);
        }

        // POST: BACSIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            BACSI bACSI = db.BACSIs.Find(id);
            db.BACSIs.Remove(bACSI);
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
