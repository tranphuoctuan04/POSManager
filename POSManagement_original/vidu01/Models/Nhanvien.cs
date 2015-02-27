using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Validation_I;

namespace vidu01.Models
{
    public class Nhanvien
    {
        public Nhanvien()
        {
            NhanvienId = Guid.NewGuid();
        }
        [Key]
        public Guid NhanvienId { get; set; }

        [MyRequire(ErrorMessage = "*Tên không được để trống.")]
        public string Hoten { get; set; }

        [MyRequire(ErrorMessage = "*Địa chỉ không được để trống.")]
        public string Diachi { get; set; }

        [MyRequire(ErrorMessage = "*Email không được để trống.")]
        [MyRegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$",ErrorMessage="*Phải nhập chính xác email")]
        public string Email { get; set; }

        [MyRequire(ErrorMessage = "*Số điện thoại không được để trống hoặc có chữ.")]
        public string Sdt { get; set; }

    }
}
// Nhanvien(NhanvienId, Hoten, Diachi, Email, Sdt)
// Chưa có cửa hàng Id