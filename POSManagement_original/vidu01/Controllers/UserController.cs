using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidu01.Models;

namespace vidu01.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetUserItems()
        {
            List<User> items = POSRepository.GetUserItems();
            return new JsonResult() { Data = new { Data = items }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}
