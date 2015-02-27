using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Validation_I;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace vidu01.Models
{
    public class NhomHanghoa
    {
        public NhomHanghoa()
        {
            NhomHanghoaId = Guid.NewGuid();
        }

        [Key]
        public Guid NhomHanghoaId { get; set; }

        [MyRange(0,Int64.MaxValue ,ErrorMessage="ID của nhóm hàng hóa phải lớn hơn 0")]
        public Int64 Code { get; set; }

        [MyRequire(ErrorMessage="Tên không được để trống")]
        public string Ten { get; set; }

        public  ICollection<Hanghoa> Hanghoas { get; set; }
    }
}