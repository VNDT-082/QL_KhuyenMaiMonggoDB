using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom08_QuanLyKhuyenMai.Models.Services
{
    public class LoaiKhuyenMaiService
    {
        private ConnectionToMongoDB _DbContext;
        public LoaiKhuyenMaiService()
        {
            _DbContext = new ConnectionToMongoDB();
        }
        public List<LoaiKhuyenMai> DuyTan_GetAll()
        {
            List<LoaiKhuyenMai> dsLoai = new List<LoaiKhuyenMai>();
            //truy van
            var pipeline = new BsonDocument[]
           {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", new BsonDocument
                        {
                            { "MaLoai", "$LoaiKhuyenMai.MaLoai" },
                            { "TenLoai", "$LoaiKhuyenMai.TenLoai" }
                        }
                    }
                })
           };
            var result = _DbContext.collection.Aggregate<BsonDocument>(pipeline).ToList();
            foreach (var item in result)
            {
                LoaiKhuyenMai l = new LoaiKhuyenMai();
                l.maLoai = item["_id"]["MaLoai"].AsString;
                l.tenLoai = item["_id"]["TenLoai"].AsString;
                dsLoai.Add(l) ;
            }

            return dsLoai;
        }

    }
}