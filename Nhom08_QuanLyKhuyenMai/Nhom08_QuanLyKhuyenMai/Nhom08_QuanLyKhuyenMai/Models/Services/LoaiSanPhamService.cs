using Amazon.Runtime.Documents;
using Microsoft.Ajax.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Nhom08_QuanLyKhuyenMai.Models.Services
{
    public class LoaiSanPhamService
    {
        private ConnectionToMongoDB _DbContext;
        public LoaiSanPhamService()
        {
            _DbContext = new ConnectionToMongoDB();
        }
        public List<LoaiSanPham> DuyTan_GetAll()
        {
            List<LoaiSanPham> dsLoai = new List<LoaiSanPham>();
            ////truy van
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$danhSachSanPham" }
                }),
                new BsonDocument("$unwind", "$_id"),
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$_id.loaiSanPham" }
                })
            };
            var result = _DbContext.collection.Aggregate<BsonDocument>(pipeline).ToList();
            foreach (var item in result)
            {
                LoaiSanPham loai = new LoaiSanPham();
                loai.maLoai = item["_id"]["maLoai"].AsString;
                loai.tenLoai = item["_id"]["tenLoai"].AsString;
                dsLoai.Add(loai);
            }
            return dsLoai;
        }
        public List<LoaiSanPham> DuyTan_GetAllLoaiSanPham()
        {
            List<LoaiSanPham> dsLoai = new List<LoaiSanPham>();
            ////truy van
            ///
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", new BsonDocument
                        {
                            { "maLoai", "$loaiSanPham.maLoai" },
                            { "tenLoai", "$loaiSanPham.tenLoai" }
                        }
                    }
                })
            };
           
            var result = _DbContext.collectionSanPham.Aggregate<BsonDocument>(pipeline).ToList();
            foreach (var item in result)
            {
                LoaiSanPham loai = new LoaiSanPham();
                loai.maLoai = item["_id"]["maLoai"].AsString;
                loai.tenLoai = item["_id"]["tenLoai"].AsString;
                loai.dsSanPham = new List<SanPham>();
                dsLoai.Add(loai);
            }
            return dsLoai;
        }

        public string FindMaxID()
        {
            var sortDefinition = Builders<BsonDocument>.Sort.Descending("_id");
            var projectionDefinition = Builders<BsonDocument>.Projection.Exclude("_id").Include("loaiSanPham.maLoai");
            var filter = Builders<BsonDocument>.Filter.Empty;

            var result = _DbContext.collectionSanPham.Find(filter)
                       .Sort(sortDefinition)
                       .Project(projectionDefinition)
                       .Limit(1)
                       .FirstOrDefault();
            return result["loaiSanPham"]["maLoai"].AsString;
        }
    }
}