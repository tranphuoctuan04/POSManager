using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace vidu01.Models
{
    public class POSDBContext: DbContext
    {
        public DbSet<Hanghoa> HanghoaItems { get; set; }
        public DbSet<NhomHanghoa> NhomHanghoaItems { get; set; }
        public DbSet<GiaHanghoa> GiaHanghoaItems { get; set; }
        public DbSet<Nhanvien> NhanvienItems { get; set; }
        public DbSet<User> UserItems { get; set; }
    }
}