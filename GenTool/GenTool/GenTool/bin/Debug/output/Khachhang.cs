using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace EnterNameSpaceHere// EnterNameSpaceHere
{
    public class Khachhang
    {
        public Khachhang()
        {
            KhachhangID = Guid.NewGuid();
        }
        public Datatype KhachhangID {get;set;}
        public Datatype TenKhachhang {get;set;}
        public Datatype Socmnd {get;set;}
        public Datatype SoTienTichluy {get;set;}

    }
}

