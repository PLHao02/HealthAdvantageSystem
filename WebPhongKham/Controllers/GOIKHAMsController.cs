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
    public class GOIKHAMsController : Controller
    {
        private qlpkEntities2 db = new qlpkEntities2();

        // GET: GOIKHAMs
        public ActionResult Index(string searchString)
        {
            var gOIKHAMs = db.GOIKHAMs.Include(g => g.LOAIGOIKHAM);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                gOIKHAMs = gOIKHAMs.Where(n => n.TenGoiKham.ToLower().Contains(searchString));
            }
            return View(gOIKHAMs.ToList());
        }

        // GET: GOIKHAMs/Details/5
        public ActionResult Details(int? id)
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

        // GET: GOIKHAMs/Create
        public ActionResult Create()
        {
            ViewBag.LGKID = new SelectList(db.LOAIGOIKHAMs, "LGKID", "TenLoaiGoiKham");
            return View();
        }

        // POST: GOIKHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGK,TenGoiKham,ChiTietGK,LGKID,ChiPhi,Imgae")] GOIKHAM gOIKHAM, 
            HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                db.GOIKHAMs.Add(gOIKHAM);
                db.SaveChanges();
                if (uploadImage != null && uploadImage.ContentLength > 0)
                {
                    int id = db.GOIKHAMs.ToList().Last().GoiKhamID;                   
                    string _FileName = "";
                    int index = uploadImage.FileName.IndexOf('.');
                    _FileName ="gk" + id.ToString() + "." + uploadImage.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Image/goikham/"), _FileName);
                    uploadImage.SaveAs(_path);

                    GOIKHAM items = db.GOIKHAMs.FirstOrDefault(x => x.GoiKhamID == id);
                    items.Anh = _FileName;
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }

            ViewBag.LGKID = new SelectList(db.LOAIGOIKHAMs, "LGKID", "TenLoaiGoiKham", gOIKHAM.LGKID);
            return View(gOIKHAM);
        }

        // GET: GOIKHAMs/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.LGKID = new SelectList(db.LOAIGOIKHAMs, "LGKID", "TenLoaiGoiKham", gOIKHAM.LGKID);
            return View(gOIKHAM);
        }

        // POST: GOIKHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGK,TenGoiKham,ChiTietGK,LGKID,ChiPhi,Imgae")] GOIKHAM gOIKHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gOIKHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LGKID = new SelectList(db.LOAIGOIKHAMs, "LGKID", "TenLoaiGoiKham", gOIKHAM.LGKID);
            return View(gOIKHAM);
        }

        // GET: GOIKHAMs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: GOIKHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GOIKHAM gOIKHAM = db.GOIKHAMs.Find(id);
            db.GOIKHAMs.Remove(gOIKHAM);
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
