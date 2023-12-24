using System.Web;
using System.Web.Mvc;

namespace Nhom08_QuanLyKhuyenMai
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
