using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPhongKham.Models;
using PayPal.Core;
using PayPal.v1.Payments;
using System.Threading.Tasks;

namespace WebPhongKham.Controllers
{
    public class ShoppingCartController : Controller
    {
        qlpkEntities2 db = new qlpkEntities2();
        public readonly string clientId;
        public readonly string secretKey;

        public int TyGiaUSD = 23300;
        public ShoppingCartController(IConfiguration config)
        {
            clientId = config["PaypalSetting:ClientID"];
            secretKey = config["PaypalSetting:SecretKey"];
        }
        public ShoppingCartController() { }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddToCart(int? id)
        {
            var goikham = db.GOIKHAMs.SingleOrDefault(s => s.GoiKhamID == id);
            if(goikham != null)
            {
                GetCart().Add(goikham);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult ShowToCart()
        {
            if(Session["Cart"] == null)
            {
                return View();
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult RemoveCart(int? id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult Update(FormCollection form /*int id, int soluong*/ )
        {
            Cart cart = (Cart)Session["Cart"];
            int goikhamid = int.Parse(form["ID_Goikham"]);
            int soluong = int.Parse(form["Soluong"]);
            cart.Update(goikhamid, soluong);
            return RedirectToAction("ShowToCart", "ShoppingCart");

        }
        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if(cart != null)            
                total_item = cart.TotalQuantitity();
                ViewBag.QuantityCart = total_item;
                return PartialView("BagCart");
        }
        public ActionResult Shopping_Sucess()
        {
            return View();
        }
        public ActionResult Checkout(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            DONHANG _donhang = new DONHANG();
            _donhang.TenKH = form["NameCustomer"];
            _donhang.SDT = int.Parse(form["Phone"]);
            _donhang.DiaChiNhan = form["Address"];
            db.DONHANGs.Add(_donhang);
            foreach(var item in cart.Items)
            {
                CHITIETDONHANG _ctdh = new CHITIETDONHANG();
                _ctdh.DonHangID = _donhang.DonHangID;
                _ctdh.GoiKhamID = item._goikham.GoiKhamID;
                _ctdh.TongTien = item._goikham.ChiPhi;
                _ctdh.SoLuongSP = item._soluong;
                db.CHITIETDONHANGs.Add(_ctdh);
            }
            db.SaveChanges();
            cart.ClearCart();
            return RedirectToAction("Shopping_Sucess", "ShoppingCart");
        }

        public async Task<ActionResult> PaypalCheckout()
        {
            var environment = new SandboxEnvironment(clientId, secretKey);
            var client = new PayPalHttpClient(environment);

            #region Create Paypal Order
            Cart cart = Session["Cart"] as Cart;
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            var total = Math.Round(cart.Items.Sum(p => p._total) / TyGiaUSD, 2);
            foreach (var item in cart.Items)
            {
                itemList.Items.Add(new Item()
                {
                    Name = item._goikham.TenGoiKham,
                    Currency = "USD",
                    Price = Math.Round((double)(item._goikham.ChiPhi / TyGiaUSD), 2).ToString(),
                    Quantity = item._soluong.ToString(),
                    Sku = "sku",
                    Tax = "0"
                });
            }
            #endregion
            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Url.Scheme}://{HttpContext.Request.Url.Host}";
            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString(),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = total.ToString()
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    //CancelUrl = $"{hostname}/ShoppingCart/CheckoutFail",
                    //ReturnUrl = $"{hostname}/ShoppingCart/Shopping_Sucess"
                    ReturnUrl = Url.Action("Shopping_Sucess", "ShoppingCart", null, Request.Url.Scheme)
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };
            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }

                return Redirect(paypalRedirectUrl);
            }
            catch (BraintreeHttp.HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                return Redirect("/ShoppingCart/CheckoutFail");
            }
        }
    }
}