using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace vidu01.Models
{
    public class Hanghoa
    {
        public Hanghoa()
        {
            HanghoaId = Guid.NewGuid();
        }
        
        public Guid HanghoaId { get; set; }
        public Int64? Maso { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        [Remote("TenKhongDuocTrung", "Home", AdditionalFields = "HanghoaId", ErrorMessage = "Tên hàng hóa không được trùng")]
        public string Ten { get; set; }
        [Required(ErrorMessage = "sản phẩm phải có giá!")]
        [Range(1000, 1000000), DataType(DataType.Currency)]
        public Int32? Giaban { get; set; }
        public DateTime? NgayGiaban { get; set; }
    }
}