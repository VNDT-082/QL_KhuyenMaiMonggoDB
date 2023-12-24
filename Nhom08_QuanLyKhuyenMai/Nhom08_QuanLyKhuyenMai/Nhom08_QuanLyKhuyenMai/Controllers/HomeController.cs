using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;
using Nhom08_QuanLyKhuyenMai.Models;
using Nhom08_QuanLyKhuyenMai.Models.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Nhom08_QuanLyKhuyenMai.Controllers
{
    public class HomeController : Controller
    {
        private LoaiSanPhamService _loaiSanPhamService = new LoaiSanPhamService();
        private KhuyenMaiService _khuyenMaiService = new KhuyenMaiService();
        private SanPhamService _sanPhamService = new SanPhamService();
        private LoaiKhuyenMaiService _loaiKhuyenMaiService = new LoaiKhuyenMaiService();

        public ConnectionToMongoDB _dbContext = new ConnectionToMongoDB();

        private XacThuc _xacThuc = new XacThuc();
        public ActionResult Index()
        {
            var dsLoai=_loaiSanPhamService.DuyTan_GetAll();
            return View(dsLoai);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult QLKhuyenMai()
        {
            List<LoaiSanPham> dsLoai = _loaiSanPhamService.DuyTan_GetAll();
            double soLuongKM = _khuyenMaiService.DuyTan_Count();
            ViewBag.SL = soLuongKM;
            ViewBag.dsLoai = dsLoai;
            ViewBag.page = 1;
            List<KhuyenMai> listKhuyenMai = _khuyenMaiService.DuyTan_GetTop5Frist();
            ViewBag.listKhuyenMai = listKhuyenMai;
            return View();
        }
        public ActionResult QLKhuyenMaiPhanTrang(int id)
        {

            List<LoaiSanPham> dsLoai = _loaiSanPhamService.DuyTan_GetAll();
            double soLuongKM = _khuyenMaiService.DuyTan_Count();
            ViewBag.SL = soLuongKM;
            ViewBag.dsLoai = dsLoai;
            ViewBag.page = id;
            List<KhuyenMai> listKhuyenMai = _khuyenMaiService.DuyTan_PhanTrang(id);
            ViewBag.listKhuyenMai = listKhuyenMai;
            return View("QLKhuyenMai");
        }
        public ActionResult CRUDKhuyenMai(string id)
        {
            List<SanPham> dsSanPham = _sanPhamService.DuyTan_GetListSanPham();
            List<SanPham> dsSanPhamTheoMa = _sanPhamService.DuyTan_GetListSanPhamTheoMaKhuyenMai(id);
            List<LoaiKhuyenMai> dsLoaiKhuyenMai = _loaiKhuyenMaiService.DuyTan_GetAll();
            ViewBag.dsLoaiKhuyenMai = dsLoaiKhuyenMai;
            ViewBag.dsSanPhamTheMa = dsSanPhamTheoMa;
            ViewBag.dsSanPham = dsSanPham;
            KhuyenMai khuyenMai = _khuyenMaiService.DuyTan_GetTopOneByID(id);
            return View(khuyenMai);
        }
       
        public ActionResult Errorpage()
        {
            return View();
        }
        public ActionResult Errorpage(string ex)
        {
            ViewBag.ex = ex;
            return View("Errorpage");
        }


        //
        public ActionResult LocSanPhamHetHan()
        {
            List<LoaiSanPham> dsLoai = _loaiSanPhamService.DuyTan_GetAll();
            double soLuongKM = _khuyenMaiService.DuyTan_Count();
            ViewBag.SL = soLuongKM;
            ViewBag.dsLoai = dsLoai;
            ViewBag.page = 1;
            List<KhuyenMai> listKhuyenMai = _khuyenMaiService.ThanhPhuc_KhuyenMaiHetHan();
            ViewBag.listKhuyenMai = listKhuyenMai;
            return View("QLKhuyenMai");
        }
        public ActionResult LocSanPhamConHan()
        {
            List<LoaiSanPham> dsLoai = _loaiSanPhamService.DuyTan_GetAll();
            double soLuongKM = _khuyenMaiService.DuyTan_Count();
            ViewBag.SL = soLuongKM;
            ViewBag.dsLoai = dsLoai;
            ViewBag.page = 1;
            List<KhuyenMai> listKhuyenMai = _khuyenMaiService.ThanhPhuc_KhuyenMaiConHan();
            ViewBag.listKhuyenMai = listKhuyenMai;
            return View("QLKhuyenMai");
        }
        public ActionResult LocSanPhamMaTangDan(int id)
        {
            List<LoaiSanPham> dsLoai = _loaiSanPhamService.DuyTan_GetAll();
            double soLuongKM = _khuyenMaiService.DuyTan_Count();
            ViewBag.SL = soLuongKM;
            ViewBag.dsLoai = dsLoai;
            ViewBag.page = id;
            List<KhuyenMai> listKhuyenMai = _khuyenMaiService.DuyTan_PhanTrangTangDan(id);
            ViewBag.listKhuyenMai = listKhuyenMai;
            return View("QLKhuyenMai");
        }
        public ActionResult LocSanPhamMaGiamDan(int id)
        {
            List<LoaiSanPham> dsLoai = _loaiSanPhamService.DuyTan_GetAll();
            double soLuongKM = _khuyenMaiService.DuyTan_Count();
            ViewBag.SL = soLuongKM;
            ViewBag.dsLoai = dsLoai;
            ViewBag.page = id;
            List<KhuyenMai> listKhuyenMai = _khuyenMaiService.DuyTan_PhanTrangGiamDan(id);
            ViewBag.listKhuyenMai = listKhuyenMai;
            return View("QLKhuyenMai");
        }

        ///insert
        ///
        public ActionResult ThemKhuyenMai(FormCollection collect)
        {
            string action = collect.Get("submit");
            if (action== "Thêm khuyến mãi")
            {
                string maKhuyenMaiMax = _khuyenMaiService.FindMaxID();
                KhuyenMai kmNew = new KhuyenMai();
                kmNew.maKhuyenMai = SetIdKhuyenMai(maKhuyenMaiMax);
                kmNew.tieuDe = collect.Get("tieuDe");
                kmNew.moTa = collect.Get("moTa");
                kmNew.ngayBatDau = DateTime.Parse(collect.Get("ngayBatDau"));
                kmNew.ngayKetThuc = DateTime.Parse(collect.Get("ngayKetThuc"));
                kmNew.phanTramKhuyenMai = byte.Parse(collect.Get("phanTramKhuyenMai"));

                if(collect.Get("loaiKhuyenMai")== "addNew")
                {
                    DateTime currentDateTime = DateTime.Now;
                    string guidString = currentDateTime.ToString("yyyyMMddHHmmss");
                    LoaiKhuyenMai loaiKhuyenMai = new LoaiKhuyenMai();
                    loaiKhuyenMai.maLoai ="LKM"+ guidString;
                    loaiKhuyenMai.tenLoai = collect.Get("loaiKhuyenMaiMoi");
                    kmNew.loaiKhuyenMai = loaiKhuyenMai;
                    
                }
                else {
                    kmNew.loaiKhuyenMai = _loaiKhuyenMaiService.DuyTan_GetAll().FirstOrDefault(i => i.maLoai == collect.Get("loaiKhuyenMai"));
                }
                
                return _khuyenMaiService.DuyTan_InsertKhuyenMai(kmNew)== true? RedirectToAction("CRUDKhuyenMai") : RedirectToAction("About");
            }
            else if (action == "Cập nhật thay đổi")
            {
                KhuyenMai kmNew = new KhuyenMai();
                kmNew.maKhuyenMai = collect.Get("maKhuyenMai");
                kmNew.tieuDe = collect.Get("tieuDe");
                kmNew.moTa = collect.Get("moTa");
                kmNew.ngayBatDau = DateTime.Parse(collect.Get("ngayBatDau"));
                kmNew.ngayKetThuc = DateTime.Parse(collect.Get("ngayKetThuc"));
                kmNew.phanTramKhuyenMai = byte.Parse(collect.Get("phanTramKhuyenMai"));
                kmNew.loaiKhuyenMai = _loaiKhuyenMaiService.DuyTan_GetAll().FirstOrDefault(i => i.maLoai == collect.Get("loaiKhuyenMai"));

                return _khuyenMaiService.DuyTan_UpdateKhuyenMai(kmNew) == true 
                    ? RedirectToAction("CRUDKhuyenMai", "Home", new { id = kmNew.maKhuyenMai }): RedirectToAction("About");
            }
            else if (action == "Xóa khuyến mãi")
            {
                return _khuyenMaiService.DuyTan_DeleteOneKhuyenMai(collect.Get("maKhuyenMai")) == true
                    ? RedirectToAction("CRUDKhuyenMai", "Home", new { id = collect.Get("maKhuyenMai") }) : RedirectToAction("About");
            }



            List<LoaiSanPham> dsLoai = _loaiSanPhamService.DuyTan_GetAll();
            ViewBag.dsLoai = dsLoai;

            List<KhuyenMai> listKhuyenMai = _khuyenMaiService.DuyTan_GetTop5Frist();
            ViewBag.listKhuyenMai = listKhuyenMai;

            return RedirectToAction("Index");
        }
        //them san pham vao khuyen mai
        public ActionResult AddSanPhamToKhuyenMai(string maSanPham, string maKhuyenMai)
        {
            SanPham sanPham = _sanPhamService.HaiDang_GetOneSanPhamByMaSanPham(maSanPham);
            bool kq = _khuyenMaiService.DuyTan_PushSanPhamVaoKhuyenMai(sanPham, maKhuyenMai);
            if (kq == true)
                return RedirectToAction("CRUDKhuyenMai", "Home", new {id=maKhuyenMai});

            return RedirectToAction("Errorpage");
        }
        //Xoa san pham khoi khuyen mai
        public ActionResult PullSanPhamKhoiKhuyenMai(string maKhuyenMai, string maSanPham)
        {
            bool kq = _khuyenMaiService.DuyTan_PullOneSanPham(maKhuyenMai, maSanPham);
            if (kq == true)
                return RedirectToAction("CRUDKhuyenMai", "Home", new { id = maKhuyenMai });

            return RedirectToAction("Errorpage");
        }







        //==========================================================================
        ///collectionsanPham
        /// <summary>
        /// collectionsanPham
        public ActionResult QLSanPham()
        {
            List<SanPham> dsSanPham = _sanPhamService.DuyTan_GetTop10Frist();
            List<LoaiSanPham> dsLoaiSanPham = _loaiSanPhamService.DuyTan_GetAllLoaiSanPham();
            ViewBag.dsSanPham = dsSanPham;
            ViewBag.dsLoaiSanPham = dsLoaiSanPham;
            return View(dsSanPham[0]);
        }
        public ActionResult ChiTietSanPham(string id)
        {
            List<SanPham> dsSanPham = _sanPhamService.DuyTan_GetTop10Frist();
            List<LoaiSanPham> dsLoaiSanPham = _loaiSanPhamService.DuyTan_GetAllLoaiSanPham();
            ViewBag.dsSanPham = dsSanPham;
            ViewBag.dsLoaiSanPham = dsLoaiSanPham;
            SanPham sp = _sanPhamService.DuyTan_GetListSanPham().FirstOrDefault(i => i.maSanPham == id);
            return View("QLSanPham", sp);
        }
        public ActionResult Refesh()
        {
            List<SanPham> dsSanPham = _sanPhamService.DuyTan_GetTop10Frist();
            List<LoaiSanPham> dsLoaiSanPham = _loaiSanPhamService.DuyTan_GetAllLoaiSanPham();
            ViewBag.dsSanPham = dsSanPham;
            ViewBag.dsLoaiSanPham = dsLoaiSanPham;
            SanPham sanPham = new SanPham();
            return View("QLSanPham", sanPham);
        }


        [HttpPost]
        public ActionResult ThemSanPham(FormCollection collect, HttpPostedFileBase fileUpLoad)
        {
            SanPham sanPham = new SanPham();
            sanPham.tenSanPham = collect.Get("tenSanPham");

            if (collect.Get("loaiSanPham") == "addNew")
            {
                LoaiSanPham loaiSanPham = new LoaiSanPham();

                loaiSanPham.maLoai = SetIdLoaiSanPham(_loaiSanPhamService.FindMaxID());
                loaiSanPham.tenLoai = collect.Get("loaiSanPhamMoi");
                sanPham.loaiSanPham = loaiSanPham;
            }
            else
            {
                sanPham.loaiSanPham = _loaiSanPhamService.DuyTan_GetAllLoaiSanPham().FirstOrDefault(i => i.maLoai == collect.Get("loaiSanPham"));
            }
            sanPham.sanXuatTai = collect.Get("sanXuatTai");
            sanPham.thuongHieu = collect.Get("thuongHieu");
            sanPham.moTa = collect.Get("moTa");
            sanPham.gia = long.Parse(collect.Get("gia"));
            if (collect.Get("submit") == "Thêm sản phẩm")
            {
                DateTime currentDateTime = DateTime.Now;
                string guidString = currentDateTime.ToString("yyyyMMddHHmmss");
                sanPham.maSanPham = "sp" + guidString;

                sanPham.hinhAnh = new string[1];
                if (fileUpLoad != null)
                    sanPham.hinhAnh[0] = fileUpLoad.FileName;
                else
                    sanPham.hinhAnh[0] = "default_image.png";
                bool kq = _sanPhamService.DuyTan_InsertSanPham(sanPham);
                if (kq == true)
                {
                    if (fileUpLoad != null)
                    {
                        string tenFile = Server.MapPath("/Images/" + fileUpLoad.FileName);
                        if (!Directory.Exists(tenFile))
                        {
                            fileUpLoad.SaveAs(tenFile);
                        }
                    }


                    return RedirectToAction("ChiTietSanPham", "Home", new { id = sanPham.maSanPham });
                }
                return RedirectToAction("Errorpage");
            }
            else if (collect.Get("submit") == "Xóa sản phẩm")
            {
                bool kq = _sanPhamService.DuyTan_DeleteOneSanPham(collect.Get("maSanPham"));
                if (kq == true)
                {
                    return RedirectToAction("ChiTietSanPham", "Home", new { id = _sanPhamService.FindMaxID() });
                }
                return RedirectToAction("Errorpage");
            }
            else if (collect.Get("submit") == "Cập nhật sản phẩm")
            {
                sanPham.maSanPham = collect.Get("maSanPham");
                sanPham.hinhAnh = new string[1];
                if(fileUpLoad != null)
                    sanPham.hinhAnh[0] = fileUpLoad.FileName;
                bool kq = _sanPhamService.DuyTan_UpdateSanPham(sanPham);
                if (kq == true)
                {
                    if (fileUpLoad != null)
                    {
                        string tenFile = Server.MapPath("/Images/" + fileUpLoad.FileName);
                        if (!Directory.Exists(tenFile))
                        {
                            fileUpLoad.SaveAs(tenFile);
                        }
                    }


                    return RedirectToAction("ChiTietSanPham", "Home", new { id = sanPham.maSanPham });
                }
                return RedirectToAction("Errorpage");
            }
            return RedirectToAction("Errorpage");
        }

        public ActionResult PullHinhAnhRaKhoiSanPham(string maSanPham,string tenAnh)
        {
            bool kq = _sanPhamService.DuyTan_PullHinhAnhRaSanPham(maSanPham,tenAnh);
            if(kq==true)
                return RedirectToAction("ChiTietSanPham", "Home", new { id = maSanPham});
            return RedirectToAction("Errorpage");
        }

        public ActionResult DeleteOneSanPham(string id)
        {
            bool kq = _sanPhamService.DuyTan_DeleteOneSanPham(id);
            if (kq == true)
                return RedirectToAction("ChiTietSanPham", "Home", new { id = _sanPhamService.FindMaxID() });
            return RedirectToAction("Errorpage");
        }
        //=====================================================================
        private string SetIdKhuyenMai(string id)
        {
            string maKhuyenMai = "";
            int maKhuyenMaiInt;
            for (int i = 2; i < id.Length; i++)
            {
                maKhuyenMai += id[i];
            }
            maKhuyenMaiInt = int.Parse(maKhuyenMai);
            if (maKhuyenMaiInt < 10)
                return "KM00" + ++maKhuyenMaiInt;
            if (maKhuyenMaiInt < 100)
                return "KM0" + ++maKhuyenMaiInt;
            return "KM" + ++maKhuyenMaiInt;
        }
        private string SetIdLoaiSanPham(string id)
        {
            string maSanPham = "";
            int maSanPhamInt;
            for (int i = 3; i < id.Length; i++)
            {
                maSanPham += id[i];
            }
            maSanPhamInt = int.Parse(maSanPham);
            if (maSanPhamInt < 10)
                return "spl00" + ++maSanPhamInt;
            if (maSanPhamInt < 100)
                return "spl0" + ++maSanPhamInt;
            return "spl" + ++maSanPhamInt;
        }
        private string SetIdSanPham(string id)
        {
            string maSanPham = "";
            int maSanPhamInt;
            for (int i = 2; i < id.Length; i++)
            {
                maSanPham += id[i];
            }
            maSanPhamInt = int.Parse(maSanPham);
            if (maSanPhamInt < 10)
                return "sp00" + ++maSanPhamInt;
            if (maSanPhamInt < 100)
                return "sp0" + ++maSanPhamInt;
            return "sp" + ++maSanPhamInt;
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TaiKhoan model)
        {
            TaiKhoanService tks = new TaiKhoanService();
            int result = IsValidUser(model.tenTaiKhoan, model.matKhau);
            if (result == -2)
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View(model);
            }
            if (result == -1)
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View(model);
            }
            if (result == 0)
            {
                ViewBag.ErrorMessage = "Tài khoản đã bị khóa.";
                return View(model);
            }
            TaiKhoan tk = tks.ThanhPhuoc_LayTaiKhoan(model.tenTaiKhoan);
            Session["Username"] = tk.tenTaiKhoan;
            Session["Auth"] = tk.quyen;
            return RedirectToAction("QLKhuyenMai", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        private int IsValidUser(string username, string password)
        {
            TaiKhoanService tks = new TaiKhoanService();
            TaiKhoan tk = tks.ThanhPhuoc_LayTaiKhoan(username);
            if (tk == null)
                return -2; //khong tim thay tk
            if (tk.matKhau != password)
                return -1; //tai khoan sai mk
            if (!tk.trangThai)
                return 0; //tai khoan bi khoa
            return 1;
        }
        public ActionResult NhanVienDetails(FormCollection collect)
        {
            NhanVienService nvs = new NhanVienService();
            TaiKhoanService tks = new TaiKhoanService();
            // Lấy danh sách tất cả nhân viên và tài khoản
            var nhanViens = nvs.ThanhPhuoc_GetAll();
            var taiKhoans = tks.ThanhPhuoc_GetAll();
            ViewBag.dsNV = nhanViens;
            ViewBag.dsTK = taiKhoans;
            return View();
        }
        [HttpGet]
        public ActionResult AddNhanVien()
        {
            NhanVienTaiKhoanViewModel viewModel = new NhanVienTaiKhoanViewModel();
            return View(viewModel);
        }
        

        [HttpPost]
        public ActionResult AddNhanVien(FormCollection collection)
        {
            _dbContext = new ConnectionToMongoDB();

            // Tìm mã nhân viên cuối cùng trong cơ sở dữ liệu
            var lastNhanVien = _dbContext.collectionNhanVien.Find(new BsonDocument())
                .Sort(Builders<BsonDocument>.Sort.Descending("maNV"))
                .FirstOrDefault();

            var newMaNV = "NV001"; // Giá trị mặc định nếu không có nhân viên nào

            if (lastNhanVien != null)
            {
                // Trích xuất mã nhân viên cuối cùng
                var lastMaNV = lastNhanVien["maNV"].AsString;

                // Lấy các số sau "NV" và tăng giá trị lên 1
                if (int.TryParse(lastMaNV.Substring(2), out int lastNumber))
                {
                    lastNumber++; // Tăng giá trị lên 1
                    newMaNV = "NV" + lastNumber.ToString("D3"); // Định dạng lại mã nhân viên
                }
            }

            var nhanVien = new NhanVien
            {
                maNV = newMaNV,
                tenNV = collection["TenNV"],
                diaChi = collection["DiaChi"],
                soDienThoai = collection["SoDienThoai"]
            };

            if (DateTime.TryParse(collection["NgaySinh"], out var ngaySinh))
            {
                nhanVien.ngaySinh = ngaySinh;
            }

            if (DateTime.TryParse(collection["NgayVaoLam"], out var ngayVaoLam))
            {
                nhanVien.ngayVaoLam = ngayVaoLam;
            }

            var taiKhoan = new TaiKhoan
            {
                tenTaiKhoan = collection["TenTaiKhoan"],
                matKhau = collection["MatKhau"],
                quyen = collection["Quyen"],
                trangThai = Boolean.Parse(collection["TrangThai"].ToString())
            };

            // Kết nối tài khoản với nhân viên
            nhanVien.TaiKhoan = taiKhoan;

            // Chuyển Nhân Viên thành một BsonDocument để thêm vào MongoDB
            BsonDocument nhanVienDocument = nhanVien.ToBsonDocument();

            _dbContext.collectionNhanVien.InsertOne(nhanVienDocument);

            return RedirectToAction("NhanVienDetails", "Home");
        }
        [HttpGet]
        public ActionResult EditNhanVien(string id)
        {
            NhanVienService nvs = new NhanVienService();
            TaiKhoanService tks = new TaiKhoanService();
            NhanVienTaiKhoanViewModel nvtk = new NhanVienTaiKhoanViewModel();
            NhanVien nv = nvs.ThanhPhuoc_GetByID(id);
            TaiKhoan tk = tks.ThanhPhuoc_GetByID(id);
            nvtk.NhanVien = nv;
            nvtk.TaiKhoan = tk;
            ViewBag.tt = nvtk;
            return View();
        }
        [HttpPost]
        public ActionResult EditNhanVien(FormCollection collection)
        {
            _dbContext = new ConnectionToMongoDB();
            DateTime.TryParse(collection["NgaySinh"], out var ngaySinh);
            DateTime.TryParse(collection["NgayVaoLam"], out var ngayVL);
            var maNV = collection["maNV"];
            var filter = Builders<BsonDocument>.Filter.Eq("maNV", maNV);
            var update = Builders<BsonDocument>.Update.Set("tenNV", collection["tenNV"])
            .Set("diaChi", collection["diaChi"])
            .Set("soDienThoai", collection["soDienThoai"])
            .Set("ngaySinh", ngaySinh)
            .Set("ngayVaoLam", ngayVL)
            .Set("TaiKhoan", new BsonDocument
            {
                    { "tenTaiKhoan", collection["TenTaiKhoan"] },
                    { "matKhau", collection["MatKhau"] },
                    { "quyen", collection["Quyen"] },
                    { "trangThai", Boolean.Parse(collection["TrangThai"]) }
            });
            var result = _dbContext.collectionNhanVien.UpdateOne(filter, update);
            if (result.IsAcknowledged && result.ModifiedCount > 0)
                return RedirectToAction("NhanVienDetails", "Home");
            else
                ModelState.AddModelError("", "Lỗi khi cập nhật Nhân Viên.");
            return RedirectToAction("EditNhanVien", "Home");
        }
        public ActionResult DeleteNhanVien(string id)
        {
            _dbContext = new ConnectionToMongoDB();
            var filter = Builders<BsonDocument>.Filter.Eq("maNV", id);
            var result = _dbContext.collectionNhanVien.DeleteOne(filter);
            return RedirectToAction("NhanVienDetails", "Home");
        }
    }
}
