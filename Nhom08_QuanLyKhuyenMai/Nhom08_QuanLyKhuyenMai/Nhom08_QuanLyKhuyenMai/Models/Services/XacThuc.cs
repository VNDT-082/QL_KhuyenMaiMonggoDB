using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom08_QuanLyKhuyenMai.Models.Services
{
    public class XacThuc
    {
        private readonly ConnectionToMongoDB _DbContext;
        public XacThuc()
        {
            this._DbContext = new ConnectionToMongoDB();
        }
        public bool Login()
        { var credential = MongoCredential.CreateCredential(_DbContext.dataBase.DatabaseNamespace.ToString(), "duytan", "12345");

            return true;
        }
    }
}