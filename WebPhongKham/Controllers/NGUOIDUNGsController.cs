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
    /*[Authorize]*/
    public class NGUOIDUNGsController : Controller
    {
        private qlpkEntities2 db = new qlpkEntities2();

        // GET: NGUOIDUNGs
        public ActionResult Index()
        {
            return View(db.NGUOIDUNGs.ToList());
        }
        public ActionResult LoginAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAccount(NGUOIDUNG _nguoidung)
        {
            if (_nguoidung.TenUser == "nhanvien.admin" && _nguoidung.MatKhauUser == "123456")// nếu là admin chuyển sang trang admin
            {
                return RedirectToAction("HomeAdmin", "Home");
            }
            else
            {
                var check = db.NGUOIDUNGs.Where(s => s.TenUser == _nguoidung.TenUser && s.MatKhauUser == _nguoidung.MatKhauUser).FirstOrDefault();
                if (check == null)
                {
                    ViewBag.ErrorInfo = "Sai tài khoản hoặc mật khẩu!";
                    return View("LoginAccount");
                }
                else
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    Session["HoTen"] = _nguoidung.HoTen;
                    Session["TenUser"] = _nguoidung.TenUser;
                    Session["MatKhauUser"] = _nguoidung.MatKhauUser;
                    return RedirectToAction("Index", "Home");
                  
                }
            }
            
        }
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]

        public ActionResult RegisterUser(NGUOIDUNG _nguoidung)
        {
            if (ModelState.IsValid)
            {
                var check_NameUser = db.NGUOIDUNGs.Where(s => s.TenUser == _nguoidung.TenUser).FirstOrDefault();
                if (check_NameUser == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.NGUOIDUNGs.Add(_nguoidung);
                    db.SaveChanges();
                    return RedirectToAction("LoginAccount");
                }

                else
                {
                    ViewBag.ErrorRegister = "Tài khoản đã có người sử dụng!";
                    return View();
                }
            }
            return View();
        }
        public ActionResult LogOutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}
