using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace EnterNameSpaceHere// EnterNameSpaceHere
{
    public class Nhanvien
    {
        public Nhanvien()
        {
            MaNhanvien = Guid.NewGuid();
        }
        public Datatype MaNhanvien {get;set;}
        public Datatype TenNhanvien {get;set;}
        public Datatype SoCMND {get;set;}
        public Datatype Ngayvaolam {get;set;}
        public Datatype Luongthang {get;set;}

    }
}

