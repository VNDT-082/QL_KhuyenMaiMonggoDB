using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom08_QuanLyKhuyenMai.Models
{
    public class LoaiSanPham
    {
        public string maLoai { get; set; }
        public string tenLoai { get; set; }
        public virtual List<SanPham> dsSanPham { get; set; }
        public LoaiSanPham() { }
        
    }
}