using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Validation_I;

namespace vidu01.Models
{
    public class GiaHanghoa
    {
        public GiaHanghoa()
        {
            GiaHanghoaId = Guid.NewGuid();
        }

        //GiaHanghoa(GiaHanghoaId, Code, HanghoaId, Gia, NgayApdung)
        [Key]
        public Guid GiaHanghoaId { get; set; }

        [MyRange(0, int.MaxValue,ErrorMessage="*Code của giá hàng hóa không được âm.")]
        [MyRequire(ErrorMessage="*Bạn phải nhập vào code của giá hàng hóa")]
        public int Code { get; set; }
        public Guid HanghoaId { get; set; }

        [MyRequire(ErrorMessage="*Bạn phải nhập giá của hàng hóa.")]
        [MyRange(1, Int32.MaxValue,ErrorMessage="*Giá của hàng hóa phải lớn hơn 0")]
        public Int32 Gia { get; set; }

        [MyRequire(ErrorMessage="*Bạn phải nhập vào ngày bắt đầu áp dụng")]
        public DateTime NgayApdung { get; set; }
    }
}