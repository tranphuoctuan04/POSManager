using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidu01.Models;



namespace vidu01.Controllers
{
    public class Nhanvien_User
    {
        public Nhanvien_User(Nhanvien nhanvien, User user)
        {
            this.nhanvien = nhanvien;
            this.user = user;
        }
        public Nhanvien nhanvien;
        public User user;
    }

    public class NhanvienController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetNhanvienItems()
        {
            List<Nhanvien> nhanviens = POSRepository.GetNhanvienItems();
            List<User> users  = POSRepository.GetUserItems();
            List<Nhanvien_User> nhanvien_Users = new List<Nhanvien_User>();
            foreach (Nhanvien nhanvien in nhanviens)
            {
                try
                {
                    User user = users.Single(x => x.NhanvienId == nhanvien.NhanvienId);
                    nhanvien_Users.Add(new Nhanvien_User(nhanvien,user));
                    continue;
                }
                catch (Exception)
                {
                    nhanvien_Users.Add(new Nhanvien_User(nhanvien, new User { UserId=null, Password=null}));
                }
            }

            return new JsonResult() { Data = new { Data = nhanvien_Users }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult addNhanvienItem(Nhanvien item, User user)
        {      
            try
            {
                POSRepository.addNhanvienItem(item,user);
                return new JsonResult()
                {
                    Data = new { Data = String.Format("success-+-{0}", item.NhanvienId) },
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

        public ActionResult EditNhanvienItem(Nhanvien item, User user)
        {
            try
            {
                POSRepository.editNhanvienItems(item,user);
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
        public ActionResult deleteNhanvienItem(Nhanvien item)
        {
            try
            {
                POSRepository.deleteNhanvienItem(item);
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
                    Data = new { Data = ex.Message},
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }

}
