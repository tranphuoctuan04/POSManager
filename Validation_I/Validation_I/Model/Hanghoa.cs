using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation_I
{
    public class Hanghoa
    {

        public Hanghoa()
        {
            HanghoaId = Guid.NewGuid();
        }

        public Guid HanghoaId { get; set; }
        public Int64? Maso { get; set; }
        [MyRequire(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string Ten { get; set; }
        [MyRequire(ErrorMessage = "sản phẩm phải có giá!")]
        [MyMinLength(5, ErrorMessage = "Lon hon 5")]
        public Int32? Giaban { get; set; }
        public DateTime? NgayGiaban { get; set; }

    }
}
