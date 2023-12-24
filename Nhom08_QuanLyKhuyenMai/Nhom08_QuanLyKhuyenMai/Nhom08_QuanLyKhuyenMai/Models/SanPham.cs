using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom08_QuanLyKhuyenMai.Models
{
    public class SanPham
    {
        public string maSanPham{ get; set; }
        public string tenSanPham{ get; set; }
        public long gia { get; set; }
        public string[] hinhAnh{ get; set; }
        public string moTa { get; set; }
        public string thuongHieu{ get; set; }
        public string sanXuatTai { get; set; }
        public virtual LoaiSanPham loaiSanPham { get; set; }
        public SanPham() { }

    }
}