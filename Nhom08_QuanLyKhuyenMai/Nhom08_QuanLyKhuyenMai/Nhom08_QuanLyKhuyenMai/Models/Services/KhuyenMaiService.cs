using MongoDB.Bson;
using MongoDB.Driver;
using Nhom08_QuanLyKhuyenMai.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom08_QuanLyKhuyenMai.Models.Services
{
    public class KhuyenMaiService
    {
        private ConnectionToMongoDB _DbContext;
        public KhuyenMaiService()
        {
            _DbContext = new ConnectionToMongoDB();
        }
        public double DuyTan_Count()
        {
            double documentCount = _DbContext.collection.CountDocuments(FilterDefinition<BsonDocument>.Empty);
            return documentCount;
        }
        public List<KhuyenMai> DuyTan_GetTop5Frist()
        {
            List<KhuyenMai> list = new List<KhuyenMai>();
            var filter = Builders<BsonDocument>.Filter.Empty;
            var projection = Builders<BsonDocument>.Projection
            .Exclude("_id")
            .Include("maKhuyenMai")
            .Include("tieuDe")
            .Include("moTa")
            .Include("ngayBatDau")
            .Include("ngayKetThuc")
            .Include("PhanTramKhuyenMai")
            .Include("LoaiKhuyenMai");

            var result = _DbContext.collection.Find(filter).Project(projection).Limit(5).ToList();
            if (result != null || result.Count > 0)
            {
                foreach (var item in result)
                {
                    KhuyenMai value = new KhuyenMai();
                    value.maKhuyenMai = item["maKhuyenMai"].AsString;
                    value.tieuDe = item["tieuDe"].AsString;
                    value.moTa = item["moTa"].AsString;
                    value.ngayBatDau = item["ngayBatDau"].AsDateTime;
                    value.ngayKetThuc = item["ngayKetThuc"].AsDateTime;
                    //if (item["PhanTramKhuyenMai"].IsInt32)
                    //    value.phanTramKhuyenMai = long.Parse(item["PhanTramKhuyenMai"].AsInt32.ToString());
                    //if (item["PhanTramKhuyenMai"].IsInt64)
                    //    value.phanTramKhuyenMai = long.Parse(item["PhanTramKhuyenMai"].AsInt64.ToString());
                    value.phanTramKhuyenMai = (byte)item["PhanTramKhuyenMai"].AsInt32;
                    value.loaiKhuyenMai = new LoaiKhuyenMai();
                    value.loaiKhuyenMai.maLoai = item["LoaiKhuyenMai"]["MaLoai"].AsString;
                    value.loaiKhuyenMai.tenLoai = item["LoaiKhuyenMai"]["TenLoai"].AsString;
                    list.Add(value);
                }
            }
            return list;
        }
        public List<KhuyenMai> DuyTan_PhanTrang(int page)
        {
            List<KhuyenMai> list = new List<KhuyenMai>();
            var filter = Builders<BsonDocument>.Filter.Empty;
            var projection = Builders<BsonDocument>.Projection
            .Exclude("_id")
            .Include("maKhuyenMai")
            .Include("tieuDe")
            .Include("moTa")
            .Include("ngayBatDau")
            .Include("ngayKetThuc")
            .Include("PhanTramKhuyenMai")
            .Include("LoaiKhuyenMai");

            var result = _DbContext.collection.Find(filter).Project(projection).Skip((page-1)*5).Limit(5).ToList();
            if (result != null || result.Count > 0)
            {
                foreach (var item in result)
                {
                    KhuyenMai value = new KhuyenMai();
                    value.maKhuyenMai = item["maKhuyenMai"].AsString;
                    value.tieuDe = item["tieuDe"].AsString;
                    value.moTa = item["moTa"].AsString;
                    value.ngayBatDau = item["ngayBatDau"].AsDateTime;
                    value.ngayKetThuc = item["ngayKetThuc"].AsDateTime;
                    value.phanTramKhuyenMai = (byte)item["PhanTramKhuyenMai"].AsInt32;
                    value.loaiKhuyenMai = new LoaiKhuyenMai();
                    value.loaiKhuyenMai.maLoai = item["LoaiKhuyenMai"]["MaLoai"].AsString;
                    value.loaiKhuyenMai.tenLoai = item["LoaiKhuyenMai"]["TenLoai"].AsString;
                    list.Add(value);
                }
            }
            return list;
        }

        public List<KhuyenMai> DuyTan_PhanTrangTangDan(int page)
        {
            List<KhuyenMai> list = new List<KhuyenMai>();

            var sortDefinition = Builders<BsonDocument>.Sort.Descending("_id");

            var filter = Builders<BsonDocument>.Filter.Empty;
            var projection = Builders<BsonDocument>.Projection
            .Exclude("_id")
            .Include("maKhuyenMai")
            .Include("tieuDe")
            .Include("moTa")
            .Include("ngayBatDau")
            .Include("ngayKetThuc")
            .Include("PhanTramKhuyenMai")
            .Include("LoaiKhuyenMai");

            var result = _DbContext.collection.Find(filter).Sort(sortDefinition).Project(projection).Skip((page - 1) * 5).Limit(5).ToList();
            if (result != null || result.Count > 0)
            {
                foreach (var item in result)
                {
                    KhuyenMai value = new KhuyenMai();
                    value.maKhuyenMai = item["maKhuyenMai"].AsString;
                    value.tieuDe = item["tieuDe"].AsString;
                    value.moTa = item["moTa"].AsString;
                    value.ngayBatDau = item["ngayBatDau"].AsDateTime;
                    value.ngayKetThuc = item["ngayKetThuc"].AsDateTime;
                    value.phanTramKhuyenMai = (byte)item["PhanTramKhuyenMai"].AsInt32;
                    value.loaiKhuyenMai = new LoaiKhuyenMai();
                    value.loaiKhuyenMai.maLoai = item["LoaiKhuyenMai"]["MaLoai"].AsString;
                    value.loaiKhuyenMai.tenLoai = item["LoaiKhuyenMai"]["TenLoai"].AsString;
                    list.Add(value);
                }
            }
            return list;
        }

        public List<KhuyenMai> DuyTan_PhanTrangGiamDan(int page)
        {
            List<KhuyenMai> list = new List<KhuyenMai>();

            var sortDefinition = Builders<BsonDocument>.Sort.Ascending("_id");

            var filter = Builders<BsonDocument>.Filter.Empty;
            var projection = Builders<BsonDocument>.Projection
            .Exclude("_id")
            .Include("maKhuyenMai")
            .Include("tieuDe")
            .Include("moTa")
            .Include("ngayBatDau")
            .Include("ngayKetThuc")
            .Include("PhanTramKhuyenMai")
            .Include("LoaiKhuyenMai");

            var result = _DbContext.collection.Find(filter).Sort(sortDefinition).Project(projection).Skip((page - 1) * 5).Limit(5).ToList();
            if (result != null || result.Count > 0)
            {
                foreach (var item in result)
                {
                    KhuyenMai value = new KhuyenMai();
                    value.maKhuyenMai = item["maKhuyenMai"].AsString;
                    value.tieuDe = item["tieuDe"].AsString;
                    value.moTa = item["moTa"].AsString;
                    value.ngayBatDau = item["ngayBatDau"].AsDateTime;
                    value.ngayKetThuc = item["ngayKetThuc"].AsDateTime;
                    value.phanTramKhuyenMai = (byte)item["PhanTramKhuyenMai"].AsInt32;
                    value.loaiKhuyenMai = new LoaiKhuyenMai();
                    value.loaiKhuyenMai.maLoai = item["LoaiKhuyenMai"]["MaLoai"].AsString;
                    value.loaiKhuyenMai.tenLoai = item["LoaiKhuyenMai"]["TenLoai"].AsString;
                    list.Add(value);
                }
            }
            return list;
        }
        public KhuyenMai DuyTan_GetTopOneByID(string maKhuyenMai)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("maKhuyenMai", maKhuyenMai);
            var projection = Builders<BsonDocument>.Projection
            .Exclude("_id")
            .Include("maKhuyenMai")
            .Include("tieuDe")
            .Include("moTa")
            .Include("ngayBatDau")
            .Include("ngayKetThuc")
            .Include("PhanTramKhuyenMai")
            .Include("LoaiKhuyenMai");

            var result = _DbContext.collection.Find(filter).Project(projection).FirstOrDefault();
            KhuyenMai value = new KhuyenMai();
            if(result!=null)
            {
                
                value.maKhuyenMai = result["maKhuyenMai"].AsString;
                value.tieuDe = result["tieuDe"].AsString;
                value.moTa = result["moTa"].AsString;
                value.ngayBatDau = result["ngayBatDau"].AsDateTime;
                value.ngayKetThuc = result["ngayKetThuc"].AsDateTime;
                value.phanTramKhuyenMai = (byte)result["PhanTramKhuyenMai"].AsInt32;
                value.loaiKhuyenMai = new LoaiKhuyenMai();
                value.loaiKhuyenMai.maLoai = result["LoaiKhuyenMai"]["MaLoai"].AsString;
                value.loaiKhuyenMai.tenLoai = result["LoaiKhuyenMai"]["TenLoai"].AsString;
            }
            return value;
        }
        public string FindMaxID()
        {
            var sortDefinition = Builders<BsonDocument>.Sort.Descending("_id");
            var projectionDefinition = Builders<BsonDocument>.Projection.Exclude("_id").Include("maKhuyenMai");
            var filter = Builders<BsonDocument>.Filter.Empty;

            var result = _DbContext.collection.Find(filter)
                       .Sort(sortDefinition)
                       .Project(projectionDefinition)
                       .Limit(1)
                       .FirstOrDefault();
            return result["maKhuyenMai"].AsString;
        }
        public bool DuyTan_InsertKhuyenMai(KhuyenMai khuyenMai)
        {
            try
            {
                var khuyenMaiBson = new BsonDocument { 
                    { "maKhuyenMai",khuyenMai.maKhuyenMai },
                    { "tieuDe", khuyenMai.tieuDe},
                    { "moTa",khuyenMai.moTa},
                    { "ngayBatDau", new BsonDateTime(khuyenMai.ngayBatDau)},
                    { "ngayKetThuc",new BsonDateTime(khuyenMai.ngayKetThuc)},
                    { "PhanTramKhuyenMai",khuyenMai.phanTramKhuyenMai},
                    {
                        "LoaiKhuyenMai", new BsonDocument
                        {
                            { "MaLoai", khuyenMai.loaiKhuyenMai.maLoai },
                            { "TenLoai", khuyenMai.loaiKhuyenMai.tenLoai }
                        }
                    },
                    { "danhSachSanPham",new BsonArray()}
                    };
                _DbContext.collection.InsertOne(khuyenMaiBson);
                return true;
            }
            catch(Exception ex) { Console.WriteLine(ex);
                return false;
            }
           
        }
        public bool DuyTan_PushSanPhamVaoKhuyenMai(SanPham sanPham,string maKhuyenMai)
        {
            try
            {
                var sanPhamBson = new BsonDocument {
                    { "maSanPham",sanPham.maSanPham},
                    { "tenSanPham", sanPham.tenSanPham},
                    { "gia",sanPham.gia},
                    { "hinhAnh",new BsonArray(sanPham.hinhAnh)},
                    { "moTa",sanPham.moTa},
                    { "thuongHieu", sanPham.thuongHieu },
                    { "sanXuatTai", sanPham.sanXuatTai },
                    {
                        "loaiSanPham", new BsonDocument
                        {
                            { "maLoai", sanPham.loaiSanPham.maLoai },
                            { "tenLoai", sanPham.loaiSanPham.tenLoai}
                        }
                    }
                    };
                var filter = Builders<BsonDocument>.Filter.Eq("maKhuyenMai", maKhuyenMai);
                var update = Builders<BsonDocument>.Update.AddToSet("danhSachSanPham", sanPhamBson);
                _DbContext.collection.UpdateOne(filter,update);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //HomeController h = new HomeController();
                //h.Errorpage(ex.ToString());
                return false;
            }
        }
        public bool DuyTan_UpdateKhuyenMai(KhuyenMai khuyenMai)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("maKhuyenMai", khuyenMai.maKhuyenMai);
                var update = Builders<BsonDocument>.Update.Set("tieuDe", khuyenMai.tieuDe)
                .Set("moTa", khuyenMai.moTa)
                .Set("ngayBatDau", new BsonDateTime(khuyenMai.ngayBatDau))
                .Set("ngayKetThuc", new BsonDateTime(khuyenMai.ngayKetThuc))
                .Set("PhanTramKhuyenMai", khuyenMai.phanTramKhuyenMai)
                .Set("LoaiKhuyenMai", new BsonDocument
                {
                    { "MaLoai", khuyenMai.loaiKhuyenMai.maLoai },
                    { "TenLoai", khuyenMai.loaiKhuyenMai.tenLoai }
                });
                _DbContext.collection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public bool DuyTan_DeleteOneKhuyenMai(string maKhyenMai)
        {

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("maKhuyenMai", maKhyenMai);
                _DbContext.collection.DeleteOne(filter);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public bool DuyTan_PullOneSanPham(string maKhuyenMai, string maSanPham)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("maKhuyenMai", maKhuyenMai);
                var update = Builders<BsonDocument>.Update.Pull("danhSachSanPham", new BsonDocument("maSanPham", maSanPham));
                _DbContext.collection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public List<KhuyenMai> ThanhPhuc_KhuyenMaiHetHan()
        {
            List<KhuyenMai> list = new List<KhuyenMai>();
            var filter = Builders<BsonDocument>.Filter.Lt("ngayKetThuc", DateTime.Now);
            var projection = Builders<BsonDocument>.Projection
                .Exclude("_id")
                .Include("maKhuyenMai")
                .Include("tieuDe")
                .Include("moTa")
                .Include("ngayBatDau")
                .Include("ngayKetThuc")
                .Include("PhanTramKhuyenMai")
                .Include("LoaiKhuyenMai");

            var result = _DbContext.collection.Find(filter).Project(projection).ToList();

            if (result != null || result.Count > 0)
            {
                foreach (var item in result)
                {
                    KhuyenMai value = new KhuyenMai();
                    value.maKhuyenMai = item["maKhuyenMai"].AsString;
                    value.tieuDe = item["tieuDe"].AsString;
                    value.moTa = item["moTa"].AsString;
                    value.ngayBatDau = item["ngayBatDau"].AsDateTime;
                    value.ngayKetThuc = item["ngayKetThuc"].AsDateTime;
                    value.phanTramKhuyenMai = (byte)item["PhanTramKhuyenMai"].AsInt32;
                    value.loaiKhuyenMai = new LoaiKhuyenMai();
                    value.loaiKhuyenMai.maLoai = item["LoaiKhuyenMai"]["MaLoai"].AsString;
                    value.loaiKhuyenMai.tenLoai = item["LoaiKhuyenMai"]["TenLoai"].AsString;
                    list.Add(value);
                }
            }
            return list;
        }
        public List<KhuyenMai> ThanhPhuc_KhuyenMaiConHan()
        {
            List<KhuyenMai> list = new List<KhuyenMai>();
            var filter = Builders<BsonDocument>.Filter.Gte("ngayKetThuc", DateTime.Now);
            var projection = Builders<BsonDocument>.Projection
                .Exclude("_id")
                .Include("maKhuyenMai")
                .Include("tieuDe")
                .Include("moTa")
                .Include("ngayBatDau")
                .Include("ngayKetThuc")
                .Include("PhanTramKhuyenMai")
                .Include("LoaiKhuyenMai");

            var result = _DbContext.collection.Find(filter).Project(projection).ToList();

            if (result != null || result.Count > 0)
            {
                foreach (var item in result)
                {
                    KhuyenMai value = new KhuyenMai();
                    value.maKhuyenMai = item["maKhuyenMai"].AsString;
                    value.tieuDe = item["tieuDe"].AsString;
                    value.moTa = item["moTa"].AsString;
                    value.ngayBatDau = item["ngayBatDau"].AsDateTime;
                    value.ngayKetThuc = item["ngayKetThuc"].AsDateTime;
                    value.phanTramKhuyenMai = (byte)item["PhanTramKhuyenMai"].AsInt32;
                    value.loaiKhuyenMai = new LoaiKhuyenMai();
                    value.loaiKhuyenMai.maLoai = item["LoaiKhuyenMai"]["MaLoai"].AsString;
                    value.loaiKhuyenMai.tenLoai = item["LoaiKhuyenMai"]["TenLoai"].AsString;
                    list.Add(value);
                }
            }
            return list;
        }
    }
}