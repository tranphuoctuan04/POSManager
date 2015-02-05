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
    }
}