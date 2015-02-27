using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidu01.Models;

namespace vidu01.Controllers
{
    public class HanghoaController : Controller
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
            CapNhatGiaHanghoa();
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
                    Data = new { Data = "success" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch(Exception ex)
            {
                return new JsonResult()
                {
                    Data = new { Data = ex.Message},
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
                    Data = new { Data = String.Format("success-+-{0}",item.HanghoaId) },
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

        public ActionResult deleteHanghoaItem(Hanghoa item)
        {
            try
            {
                POSRepository.deleteHanghoaItem(item);
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

        public ActionResult batdauBan(Hanghoa item)
        {
            try
            {
                if (item.Giaban == null )
                {
                    throw new Exception("Sản phẩm muốn đem bán phải có giá bán.");
                }

                item.Dangban = true;
                POSRepository.editHanghoaItem(item);
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

        public ActionResult ngungBan(Hanghoa item)
        {
            try
            {
                item.Dangban = false;
                POSRepository.editHanghoaItem(item);
                return new JsonResult()
                {
                    Data = new { Data = "success" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception)
            {
                return new JsonResult()
                {
                    Data = new { Data = "Không thể bắt đầu bán" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public void CapNhatGiaHanghoa()
        {
            List<GiaHanghoa> GiaHanghoas = POSRepository.GetGiaHanghoaItems().Where(x=>x.NgayApdung<=DateTime.Now).OrderByDescending(x => x.NgayApdung).ToList();
            List<Hanghoa> Hanghoas = POSRepository.GetHanghoaItems();
            foreach (Hanghoa h in Hanghoas)
            {
                bool found = false;
                foreach (GiaHanghoa g in GiaHanghoas)
                {
                    if (h.HanghoaId == g.HanghoaId)
                    {
                        h.Giaban = g.Gia;
                        h.NgayGiaban = g.NgayApdung;
                        POSRepository.editHanghoaItem(h);
                        found = true;
                        break;
                    }
                }
                if (!found) // Nghĩa là không có giá bán thích hợp
                {
                    h.Giaban = null;
                    h.NgayGiaban = null;
                    POSRepository.editHanghoaItem(h);
                }
            }
        }
    }
}
