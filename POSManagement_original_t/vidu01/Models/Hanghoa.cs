using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Validation_I;

namespace vidu01.Models
{
    public class Hanghoa
    {
        public Hanghoa()
        {
            HanghoaId = Guid.NewGuid();
        }
        public Guid HanghoaId { get; set; }

        [MyRange(0,Int64.MaxValue,ErrorMessage = "*Code của hàng hóa không được âm.")]
        public Int64? Code { get; set; }

        [MyRequire(ErrorMessage = "*Tên hàng hóa không được để trống")]
        public string Ten { get; set; }

        public Int32? Giaban { get; set; }

        public DateTime? NgayGiaban { get; set; }

        public Guid? NhomHanghoaId { get; set; }

        public bool Dangban { get; set; }

        public ICollection<GiaHanghoa> GioHanghoas { get; set; }
    }
}