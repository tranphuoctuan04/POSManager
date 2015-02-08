using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Validation_I
{
    public class StudentModel
    {
        [Key]
        public Guid StudentId { get; set; }

        [MyRequire(ErrorMessage="Ten sinh vien khong duoc trong")]
        
        public string StudentName { get; set; }
        public string StudentInformation { get; set; }

        [MyRequire(ErrorMessage="Nam hoc khong duoc de trong")]
        [MyMinLength(5,ErrorMessage="Khong duoc nho hon 5")]
        [MyMaxLength(100, ErrorMessage = "Nho hon 100")]
        public int StudentGrade { get; set; }
    }
}
