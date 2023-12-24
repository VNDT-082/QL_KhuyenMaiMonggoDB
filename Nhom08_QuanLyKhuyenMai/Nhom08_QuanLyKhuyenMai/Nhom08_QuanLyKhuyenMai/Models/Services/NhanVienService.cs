using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nhom08_QuanLyKhuyenMai.Models.Services
{
    public class NhanVienService
    {
        private readonly ConnectionToMongoDB _DbContext;
        public NhanVienService()
        {
            this._DbContext = new ConnectionToMongoDB();
        }
        public List<NhanVien> ThanhPhuoc_GetAll() //Lấy tất cả Nhân Viên
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var projection = Builders<BsonDocument>.Projection
                .Exclude("_id")
                .Include("maNV")
                .Include("tenNV")
                .Include("ngaySinh")
                .Include("diaChi")
                .Include("soDienThoai")
                .Include("ngayVaoLam");

            var result = _DbContext.collectionNhanVien.Find(filter).Project(projection).ToList();
            var list = new List<NhanVien>();

            foreach (var item in result)
            {
                var nhanVien = new NhanVien
                {
                    maNV = item["maNV"].AsString,
                    tenNV = item["tenNV"].AsString,
                    ngaySinh = item["ngaySinh"].AsDateTime,
                    diaChi = item["diaChi"].AsString,
                    soDienThoai = item["soDienThoai"].AsString,
                    ngayVaoLam = item["ngayVaoLam"].AsDateTime
                };
                list.Add(nhanVien);
            }

            return list;
        }

        public NhanVien ThanhPhuoc_GetByID(string maNV)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("maNV", maNV);
            var result = _DbContext.collectionNhanVien.Find(filter).FirstOrDefault();

            if (result != null)
            {
                NhanVien nhanVien = new NhanVien();
                nhanVien.maNV = result["maNV"].AsString;
                nhanVien.tenNV = result["tenNV"].AsString;
                nhanVien.diaChi = result["diaChi"].AsString;
                nhanVien.soDienThoai = result["soDienThoai"].AsString;
                nhanVien.ngaySinh = result["ngaySinh"].AsDateTime;
                nhanVien.ngayVaoLam = result["ngayVaoLam"].AsDateTime;

                return nhanVien;
            }

            return null;
        }
        public bool ThanhPhuoc_InsertNhanVien(NhanVien nhanVien, TaiKhoan taiKhoan)
        {
            try
            {
                var nhanVienBson = new BsonDocument {
                    { "maNV",nhanVien.maNV},
                    { "tenNV", nhanVien.tenNV},
                    { "ngaySinh",new BsonDateTime(nhanVien.ngaySinh)},
                    { "diaChi",nhanVien.diaChi},
                    { "soDienThoai",nhanVien.soDienThoai},
                    { "ngayVaoLam", new BsonDateTime(nhanVien.ngayVaoLam)},
                    {
                        "TaiKhoan", new BsonDocument
                        {
                            { "tenTaiKhoan", taiKhoan.tenTaiKhoan },
                            { "matKhau", taiKhoan.matKhau},
                            { "quyen", taiKhoan.quyen},
                            { "trangThai", taiKhoan.trangThai}
                        }
                    }
                    };
                _DbContext.collectionNhanVien.InsertOne(nhanVienBson);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }
    }
}