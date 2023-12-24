using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom08_QuanLyKhuyenMai.Models.Services
{
    public class TaiKhoanService
    {
        private ConnectionToMongoDB _DbContext;
        public TaiKhoanService()
        {
            _DbContext = new ConnectionToMongoDB();
        }
        public TaiKhoan ThanhPhuoc_LayTaiKhoan(string pUsername)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("TaiKhoan.tenTaiKhoan", pUsername);
            var taiKhoan = _DbContext.collectionNhanVien.Find(filter).FirstOrDefault();
            if (taiKhoan != null)
            {
                TaiKhoan tk = new TaiKhoan
                {
                    tenTaiKhoan = taiKhoan["TaiKhoan"]["tenTaiKhoan"].AsString,
                    matKhau = taiKhoan["TaiKhoan"]["matKhau"].AsString,
                    quyen = taiKhoan["TaiKhoan"]["quyen"].AsString,
                    trangThai = taiKhoan["TaiKhoan"]["trangThai"].AsBoolean
                };

                return tk;
            }
            else
                return null;
        }

        public List<TaiKhoan> ThanhPhuoc_GetAll() //Lấy tất cả Nhân Viên
        {
            List<TaiKhoan> ds_TK = new List<TaiKhoan>();
            ////truy van
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$danhSachTaiKhoan" }
                }),
                new BsonDocument("$unwind", "$_id"),
                new BsonDocument("$group", new BsonDocument
                {
                    { "_id", "$_id.TaiKhoan" }
                })
            };
            var result = _DbContext.collection.Aggregate<BsonDocument>(pipeline).ToList();
            foreach (var item in result)
            {
                TaiKhoan tk = new TaiKhoan();
                tk.tenTaiKhoan = item["_id"]["tenTaiKhoan"].AsString;
                tk.matKhau = item["_id"]["matKhau"].AsString;
                tk.quyen = item["_id"]["quyen"].AsString;
                tk.trangThai = item["_id"]["trangThai"].AsBoolean;
                ds_TK.Add(tk);
            }
            return ds_TK;

            //foreach (var document in result)
            //{
            //    TaiKhoan tk = new TaiKhoan();
            //    tk.Username = document.GetValue("TaiKhoan.tenTaiKhoan").AsString;
            //    tk.Password = document.GetValue("TaiKhoan.matKhau").AsString;
            //    tk.Auth = document.GetValue("TaiKhoan.quyen").AsString;
            //    tk.Status = item["TaiKhoan.trangThai"].AsBoolean; document.GetValue("TaiKhoan.trangThai").AsBoolean;
            //    ds_TK.Add(tk);
            //}
            //return ds_TK;
        }
        public TaiKhoan ThanhPhuoc_GetByID(string maNV)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("maNV", maNV);
            var result = _DbContext.collectionNhanVien.Find(filter).FirstOrDefault();

            if (result != null)
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.tenTaiKhoan = result["TaiKhoan"]["tenTaiKhoan"].AsString;
                taiKhoan.matKhau = result["TaiKhoan"]["matKhau"].AsString;
                taiKhoan.quyen = result["TaiKhoan"]["quyen"].AsString;
                taiKhoan.trangThai = result["TaiKhoan"]["trangThai"].AsBoolean;
                return taiKhoan;
            }

            return null;
        }
    }
}