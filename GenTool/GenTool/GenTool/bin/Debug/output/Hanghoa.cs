using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace EnterNameSpaceHere// EnterNameSpaceHere
{
    public class Hanghoa
    {
        public Hanghoa()
        {
            HanghoaId = Guid.NewGuid();
        }
        public Datatype HanghoaId {get;set;}
        public Datatype Ten {get;set;}
        public Datatype Giaban {get;set;}

    }
}

