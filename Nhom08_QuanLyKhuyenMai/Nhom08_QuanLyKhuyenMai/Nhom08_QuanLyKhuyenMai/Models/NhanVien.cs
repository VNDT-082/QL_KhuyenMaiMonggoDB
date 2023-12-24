using System;

namespace Nhom08_QuanLyKhuyenMai.Models
{
    public class NhanVien
    {
        public string maNV { get; set; }
        public string tenNV { get; set; }
        public DateTime ngaySinh { get; set; }
        public string diaChi { get; set; }
        public string soDienThoai { get; set; }
        public DateTime ngayVaoLam { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        public NhanVien() { }
    }
}