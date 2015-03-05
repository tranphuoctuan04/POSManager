using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Validation_I;
// User(UserId, NhanvienId, Password)

namespace vidu01.Models
{
    public class User
    {
        [MyMinLength(6,ErrorMessage=("*Tài khoản phải lớn hơn 6 ký tự"))]
        [MyMaxLength(32,ErrorMessage=("*Tài khoản phải nhỏ hơn 32 ký tự"))]
        [MyRequire(ErrorMessage="*Bạn phải nhập vào tên tài khoản")]
        public string UserId { get; set; }

        [Key]
        public Guid NhanvienId { get; set; }

        [MyMinLength(6,ErrorMessage=("*Mật khẩu phải lớn hơn 6 ký tự"))]
        [MyMaxLength(32,ErrorMessage=("*Mật khẩu phải nhỏ hơn 32 ký tự"))]
        [MyRequire(ErrorMessage = "*Bạn phải nhập vào mật khẩu")]
        public string Password { get; set; }

        public Nhanvien Nhanvien { get; set; }
    }
}