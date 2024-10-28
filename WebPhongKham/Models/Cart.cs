using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPhongKham.Models
{
    public class CartItem
    {
        public GOIKHAM _goikham { get; set; }
        public int _soluong { get; set; }
        public double _total { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(GOIKHAM _gk, int soluong = 1)
        {
            var item = items.FirstOrDefault(s => s._goikham.GoiKhamID == _gk.GoiKhamID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _goikham = _gk,
                    _soluong = soluong,
                });
            }
            else
                item._soluong += soluong;
        }
        public void Remove(int? id)
        {
            items.RemoveAll(s => s._goikham.GoiKhamID == id);
        }
        public void Update(int id, int _soluong)
        {
            var item = items.SingleOrDefault(s => s._goikham.GoiKhamID == id);
            if(item != null)
            {
                item._soluong = _soluong;            
            }
        }
        public int TotalMoney()
        {
            var total = items.Sum(s => s._goikham.ChiPhi * s._soluong);
            return (int)total;
        }
        // tổng số lượng sản phẩm trong giỏ hàng
        public int TotalQuantitity()
        {
            return items.Sum(s => s._soluong);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}