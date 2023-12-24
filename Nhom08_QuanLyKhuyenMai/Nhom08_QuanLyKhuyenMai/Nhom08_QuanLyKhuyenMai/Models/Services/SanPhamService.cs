using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Nhom08_QuanLyKhuyenMai.Models.Services
{
    public class SanPhamService
    {
        private readonly ConnectionToMongoDB _DbContext;
        public SanPhamService()
        {
            this._DbContext = new ConnectionToMongoDB();
        }
        public string FindMaxID()
        {
            var sortDefinition = Builders<BsonDocument>.Sort.Descending("_id");
            var projectionDefinition = Builders<BsonDocument>.Projection.Exclude("_id").Include("maSanPham");
            var filter = Builders<BsonDocument>.Filter.Empty;

            var result = _DbContext.collectionSanPham.Find(filter)
                       .Sort(sortDefinition)
                       .Project(projectionDefinition)
                       .Limit(1)
                       .FirstOrDefault();
            return result["maSanPham"].AsString;
        }
        public List<SanPham> DuyTan_GetListSanPham()
        {
            List<SanPham> ds_SanPham = new List<SanPham>();
            var filter = Builders<BsonDocument>.Filter.Empty;
            //var projection = Builders<BsonDocument>.Projection;
            var result = _DbContext.collectionSanPham.Find(filter).ToList();
            if (result != null)
            {
                foreach (var item in result)
                {
                    SanPham sp = new SanPham();
                    sp.maSanPham = item["maSanPham"].AsString;
                    sp.tenSanPham = item["tenSanPham"].AsString;
                    if (item["gia"].IsInt32)
                        sp.gia = long.Parse(item["gia"].AsInt32.ToString());
                    if (item["gia"].IsInt64)
                        sp.gia = long.Parse(item["gia"].AsInt64.ToString());
                    BsonArray dsHinh = item["hinhAnh"].AsBsonArray;
                    if (dsHinh != null || dsHinh.Count > 0)
                    {
                        string[] hinhAnhs = new string[dsHinh.Count];
                        for(int i =0;i<dsHinh.Count;i++)
                        {
                            hinhAnhs[i]= dsHinh[i].AsBsonValue.AsString;
                            
                        }
                        sp.hinhAnh = hinhAnhs;
                    }
                    sp.moTa = item["moTa"].AsString;
                    sp.thuongHieu = item["thuongHieu"].AsString;
                    sp.sanXuatTai = item["sanXuatTai"].AsString;
                    sp.loaiSanPham = new LoaiSanPham();
                    sp.loaiSanPham.maLoai = item["loaiSanPham"]["maLoai"].AsString;
                    sp.loaiSanPham.tenLoai = item["loaiSanPham"]["tenLoai"].AsString;
                    ds_SanPham.Add(sp);
                }
            }

            return ds_SanPham;
        }

        public List<SanPham> DuyTan_GetListSanPhamTheoMaKhuyenMai(string maKhuyenMai)
        {
            List<SanPham> ds_SanPham = new List<SanPham>();
            var filter = Builders<BsonDocument>.Filter.Eq("maKhuyenMai",maKhuyenMai);
            var projection = Builders<BsonDocument>.Projection.Include("danhSachSanPham").Exclude("_id");
            var result = _DbContext.collection.Find(filter).Project(projection).FirstOrDefault();
            if (result != null)
            {
                BsonArray dsSanPham = result["danhSachSanPham"].AsBsonArray;
                if (dsSanPham != null || dsSanPham.Count > 0)
                {
                    foreach (var item in dsSanPham)
                    {
                        SanPham sp = new SanPham();
                        sp.maSanPham = item["maSanPham"].AsString;
                        sp.tenSanPham = item["tenSanPham"].AsString;
                        if (item["gia"].IsInt32)
                            sp.gia = long.Parse(item["gia"].AsInt32.ToString());
                        if (item["gia"].IsInt64)
                            sp.gia = long.Parse(item["gia"].AsInt64.ToString());
                        BsonArray dsHinh = item["hinhAnh"].AsBsonArray;
                        if (dsHinh != null || dsHinh.Count > 0)
                        {
                            string[] hinhAnhs = new string[dsHinh.Count];
                            for (int i = 0; i < dsHinh.Count; i++)
                            {
                                hinhAnhs[i] = dsHinh[i].AsBsonValue.AsString;

                            }
                            sp.hinhAnh = hinhAnhs;
                        }
                        sp.moTa = item["moTa"].AsString;
                        sp.thuongHieu = item["thuongHieu"].AsString;
                        sp.sanXuatTai = item["sanXuatTai"].AsString;
                        sp.loaiSanPham = new LoaiSanPham();
                        sp.loaiSanPham.maLoai = item["loaiSanPham"]["maLoai"].AsString;
                        sp.loaiSanPham.tenLoai = item["loaiSanPham"]["tenLoai"].AsString;
                        ds_SanPham.Add(sp);
                    }
                }
            }

            return ds_SanPham;
        }

        public List<SanPham> DuyTan_GetTop10Frist()
        {
            List<SanPham> ds_SanPham = new List<SanPham>();
            var filter = Builders<BsonDocument>.Filter.Empty;
            //var projection = Builders<BsonDocument>.Projection;

            var result = _DbContext.collectionSanPham.Find(filter).Limit(10).ToList();
            if (result != null)
            {
                foreach (var item in result)
                {
                    SanPham sp = new SanPham();
                    sp.maSanPham = item["maSanPham"].AsString;
                    sp.tenSanPham = item["tenSanPham"].AsString;
                    if (item["gia"].IsInt32)
                        sp.gia = long.Parse(item["gia"].AsInt32.ToString());
                    if (item["gia"].IsInt64)
                        sp.gia = long.Parse(item["gia"].AsInt64.ToString());
                    BsonArray dsHinh = item["hinhAnh"].AsBsonArray;
                    if (dsHinh != null || dsHinh.Count > 0)
                    {
                        string[] hinhAnhs = new string[dsHinh.Count];
                        for (int i = 0; i < dsHinh.Count; i++)
                        {
                            hinhAnhs[i] = dsHinh[i].AsBsonValue.AsString;

                        }
                        sp.hinhAnh = hinhAnhs;
                    }
                    sp.moTa = item["moTa"].AsString;
                    sp.thuongHieu = item["thuongHieu"].AsString;
                    sp.sanXuatTai = item["sanXuatTai"].AsString;
                    sp.loaiSanPham = new LoaiSanPham();
                    sp.loaiSanPham.maLoai = item["loaiSanPham"]["maLoai"].AsString;
                    sp.loaiSanPham.tenLoai = item["loaiSanPham"]["tenLoai"].AsString;
                    ds_SanPham.Add(sp);
                }
            }

            return ds_SanPham;
        }

        public SanPham HaiDang_GetOneSanPhamByMaSanPham(string maSanPham)
        {
            SanPham sp = new SanPham();
            var results = _DbContext.collectionSanPham.Find(new BsonDocument { { "maSanPham", maSanPham } }).FirstOrDefault();

            sp.maSanPham = results["maSanPham"].AsString;
            sp.tenSanPham = results["tenSanPham"].AsString;
            if (results["gia"].IsInt32)
                sp.gia = long.Parse(results["gia"].AsInt32.ToString());
            if (results["gia"].IsInt64)
                sp.gia = long.Parse(results["gia"].AsInt64.ToString());
            BsonArray dsHinh = results["hinhAnh"].AsBsonArray;
            if (dsHinh != null || dsHinh.Count > 0)
            {
                string[] hinhAnhs = new string[dsHinh.Count];
                for (int i = 0; i < dsHinh.Count; i++)
                {
                    hinhAnhs[i] = dsHinh[i].AsBsonValue.AsString;

                }
                sp.hinhAnh = hinhAnhs;
            }
            sp.moTa = results["moTa"].AsString;
            sp.thuongHieu = results["thuongHieu"].AsString;
            sp.sanXuatTai = results["sanXuatTai"].AsString;
            sp.loaiSanPham = new LoaiSanPham();
            sp.loaiSanPham.maLoai = results["loaiSanPham"]["maLoai"].AsString;
            sp.loaiSanPham.tenLoai = results["loaiSanPham"]["tenLoai"].AsString;
            
            return sp;
        }

        public bool DuyTan_InsertSanPham(SanPham sanPham)
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
                _DbContext.collectionSanPham.InsertOne(sanPhamBson);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }
        public bool DuyTan_UpdateSanPham(SanPham sanPham)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("maSanPham", sanPham.maSanPham);
                var update = Builders<BsonDocument>.Update.Set("tenSanPham", sanPham.tenSanPham).
                    Set("tenSanPham", sanPham.tenSanPham).
                    Set("gia", sanPham.gia).
                    Set("moTa", sanPham.moTa).
                    Set("thuongHieu", sanPham.thuongHieu).
                    Set("sanXuatTai", sanPham.sanXuatTai).
                    Set("loaiSanPham", new BsonDocument
                        {
                            { "maLoai", sanPham.loaiSanPham.maLoai },
                            { "tenLoai", sanPham.loaiSanPham.tenLoai}
                        });
                
                
                _DbContext.collectionSanPham.UpdateOne(filter,update);
                if (sanPham.hinhAnh[0] != null)
                {
                    var pushImg = Builders<BsonDocument>.Update.Push("hinhAnh", sanPham.hinhAnh[0]);
                    _DbContext.collectionSanPham.UpdateOne(filter, pushImg);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

        public bool DuyTan_PullHinhAnhRaSanPham(string maSanPham, string tenAnh)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("maSanPham", maSanPham);
                var pushImg = Builders<BsonDocument>.Update.Pull("hinhAnh", tenAnh);
                _DbContext.collectionSanPham.UpdateOne(filter, pushImg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }
        public bool DuyTan_DeleteOneSanPham(string maSanPham)
        {

            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("maSanPham", maSanPham);
                _DbContext.collectionSanPham.DeleteOne(filter);
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