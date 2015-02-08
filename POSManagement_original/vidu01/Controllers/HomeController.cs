using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidu01.Models;

namespace vidu01.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetHanghoaItems()
        {
            //List<Hanghoa> items = new List<Hanghoa>();
            //items.Add(new Hanghoa
            //{
            //    Maso = 1,
            //    Ten = "Coca",
            //    Giaban = 10000,
            //    NgayGiaban = new DateTime(2015,1,2)
            //});
            //items.Add(new Hanghoa
            //{
            //    Maso = 2,
            //    Ten = "Pepsi",
            //    Giaban = 12000,
            //    NgayGiaban = new DateTime(2015, 1, 3)
            //});
            List<Hanghoa> items = POSRepository.GetHanghoaItems();
            return new JsonResult() { Data = new { Data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        public ActionResult EditHanghoaItem(Hanghoa item)
        {
            try
            {
                POSRepository.editHanghoaItem(item);
                return new JsonResult()
                {
                    Data = new { Data = "Sửa thành công" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch(Exception)
            {
                return new JsonResult()
                {
                    Data = new { Data = "Chưa sửa được" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public ActionResult addHanghoaItem(Hanghoa item)
        {
            try
            {
                POSRepository.addHanghoaItem(item);
                return new JsonResult()
                {
                    Data = new { Data = String.Format("Thêm thành công-+-{0}",item.HanghoaId) },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception)
            {
                return new JsonResult()
                {
                    Data = new { Data = "Chưa thêm được" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public ActionResult deleteHanghoaItem(Hanghoa item)
        {
            try
            {
                POSRepository.deleteHanghoaItem(item);
                return new JsonResult()
                {
                    Data = new { Data = "Xóa thành công" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception)
            {
                return new JsonResult()
                {
                    Data = new { Data = "Chưa xóa được" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}
