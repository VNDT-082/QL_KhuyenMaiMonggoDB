using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom08_QuanLyKhuyenMai.Models
{
    public class KhuyenMai
    {
        public string maKhuyenMai { get; set; }
        public string tieuDe { get; set; }
        public string moTa { get; set; }
        public DateTime ngayBatDau { get; set; }
        public DateTime ngayKetThuc{ get; set; }
        public byte phanTramKhuyenMai { get; set; }
        public LoaiKhuyenMai loaiKhuyenMai { get; set; }
        public virtual List<SanPham> dsSanPham { get; set; }
        public KhuyenMai() { }
    }
}