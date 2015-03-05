using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidu01.Models;

namespace vidu01.Controllers
{
    public class GiaHanghoaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetGiaHanghoaItems()
        {
            List<GiaHanghoa> items = POSRepository.GetGiaHanghoaItems();
            return new JsonResult() { Data = new { Data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult addGiaHanghoaItem(GiaHanghoa item)
        {
            try
            {
                POSRepository.addGiaHanghoaItem(item);
                return new JsonResult() { Data = new { Data = ("success-+-" + item.GiaHanghoaId) }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception)
            {
                return new JsonResult() { Data = new { Data = "fail" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
           
        }

        public ActionResult deleteGiaHanghoaItem(GiaHanghoa item)
        {
            try
            {
                POSRepository.deleteGiaHanghoaItem(item);
                return new JsonResult() { Data = new { Data = "success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception)
            {
                return new JsonResult() { Data = new { Data = "fail" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
        public ActionResult editGiaHanghoaItem(GiaHanghoa item)
        {
            try
            {
                POSRepository.editGiaHanghoaItem(item);
                return new JsonResult() { Data = new { Data = "success" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception)
            {
                return new JsonResult() { Data = new { Data = "fail" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
  

    }
}
