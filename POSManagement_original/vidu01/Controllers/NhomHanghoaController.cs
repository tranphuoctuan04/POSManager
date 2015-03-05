using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidu01.Models;

namespace vidu01.Controllers
{
    public class NhomHanghoaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetNhomHanghoaItems()
        {
            List<NhomHanghoa> items = POSRepository.GetNhomHanghoaItems();
            return new JsonResult() { Data = new { Data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult EditNhomHanghoaItem(NhomHanghoa item)
        {
            try
            {
                POSRepository.editNhomHanghoaItem(item);
                return new JsonResult()
                {
                    Data = new { Data = "success" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult()
                {
                    Data = new { Data = ex.Message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public ActionResult addNhomHanghoaItem(NhomHanghoa item)
        {
            try
            {
                POSRepository.addNhomHanghoaItem(item);
                return new JsonResult()
                {
                    Data = new { Data = String.Format("success-+-{0}", item.NhomHanghoaId) },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult()
                {
                    Data = new { Data = ex.Message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public ActionResult deleteNhomHanghoaItem(NhomHanghoa item)
        {
            try
            {
                POSRepository.deleteNhomHanghoaItem(item);
                return new JsonResult()
                {
                    Data = new { Data = "success" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult()
                {
                    Data = new { Data = ex.Message },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }

}
