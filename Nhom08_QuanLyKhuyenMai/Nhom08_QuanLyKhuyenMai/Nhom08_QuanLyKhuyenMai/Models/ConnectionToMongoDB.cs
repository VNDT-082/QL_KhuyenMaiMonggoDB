using Microsoft.Ajax.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom08_QuanLyKhuyenMai.Models
{
    public class ConnectionToMongoDB
    {
        private string connectionString = "mongodb://localhost:27017";
        private MongoClient client;
        public IMongoDatabase dataBase;
        public IMongoCollection<BsonDocument> collection;
        public IMongoCollection<BsonDocument> collectionSanPham;
        public IMongoCollection<BsonDocument> collectionNhanVien;
        public ConnectionToMongoDB()
        {
            client = new MongoClient(connectionString);
            dataBase = client.GetDatabase("Nhom8_QLKhuyenMai");
            collection = dataBase.GetCollection<BsonDocument>("km");
            collectionSanPham = dataBase.GetCollection<BsonDocument>("Sp");
            collectionNhanVien = dataBase.GetCollection<BsonDocument>("nv");
            Console.WriteLine("Ket noi thanh cong");
        }
    }
}