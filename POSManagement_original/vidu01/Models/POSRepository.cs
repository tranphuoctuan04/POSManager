using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidu01.Models
{
    public class POSRepository
    {
        public static List<Hanghoa> GetHanghoaItems()
        {
            using (POSDBContext db = new POSDBContext())
            {
                return db.HanghoaItems.ToList();
            }
        }
        public static void editHanghoaItem(Hanghoa item)
        {
            using(POSDBContext db = new POSDBContext())
            {
                Hanghoa hangHoaFromDB = db.HanghoaItems.Single(x => x.HanghoaId == item.HanghoaId);
                hangHoaFromDB.Ten = item.Ten;
                hangHoaFromDB.Giaban = item.Giaban;
                hangHoaFromDB.Maso = item.Maso;
                db.SaveChanges();
            }
        }
        public static void addHanghoaItem(Hanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                db.HanghoaItems.Add(item);
                db.SaveChanges();
            }
        }
        public static void deleteHanghoaItem(Hanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                Hanghoa hangHoaFromDB = db.HanghoaItems.Single(x => x.HanghoaId == item.HanghoaId);
                db.HanghoaItems.Remove(hangHoaFromDB);
                db.SaveChanges();
            }
        }
    }
}