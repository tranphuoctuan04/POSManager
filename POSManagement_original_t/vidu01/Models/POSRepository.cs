using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidu01.Models
{
    public class POSRepository
    {
        // ~~ Hanghoa ~~ 
        public static List<Hanghoa> GetHanghoaItems()
        {
            using (POSDBContext db = new POSDBContext())
            {

                return db.HanghoaItems.ToList();
            }
        }
        public static void editHanghoaItem(Hanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                Hanghoa hangHoaFromDB = db.HanghoaItems.Single(x => x.HanghoaId == item.HanghoaId);

                if (db.HanghoaItems.SingleOrDefault(x => x.Code == item.Code) != null &&
                    item.Code != hangHoaFromDB.Code) // Kiểm tra thêm trường hợp nó edit chính nó
                {
                    throw new Exception("Code của hàng hóa (" + item.Code + ") bị trùng");
                }
                else
                {
                    hangHoaFromDB.Code = item.Code;
                    hangHoaFromDB.Ten = item.Ten;
                    hangHoaFromDB.NhomHanghoaId = item.NhomHanghoaId;
                    hangHoaFromDB.Dangban = item.Dangban;
                    hangHoaFromDB.NgayGiaban = item.NgayGiaban;
                    hangHoaFromDB.Giaban = item.Giaban;

                    db.SaveChanges();
                }
            }
        }
        public static void addHanghoaItem(Hanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                if (db.HanghoaItems.SingleOrDefault(x => x.Code == item.Code) == null)
                {
                    db.HanghoaItems.Add(item);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Code của hàng hóa (" + item.Code + ") bị trùng");
                }
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

        // ~~ NhomHanghoa ~~ 

        public static List<NhomHanghoa> GetNhomHanghoaItems()
        {
            using (POSDBContext db = new POSDBContext())
            {
                return db.NhomHanghoaItems.ToList();
            }
        }
        public static void editNhomHanghoaItem(NhomHanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                NhomHanghoa nhomHangHoaFromDB = db.NhomHanghoaItems.Single(x => x.NhomHanghoaId == item.NhomHanghoaId);

                if (db.NhomHanghoaItems.SingleOrDefault(x => x.Code == item.Code) != null &&
                    item.Code != nhomHangHoaFromDB.Code) // Kiểm tra thêm trường hợp nó edit chính nó
                {
                    throw new Exception("Code của nhóm hàng hóa (" + item.Code + ") bị trùng");
                }
                else
                {
                    nhomHangHoaFromDB.Ten = item.Ten;
                    nhomHangHoaFromDB.Code = item.Code;
                    db.SaveChanges();
                }
            }
        }
        public static void addNhomHanghoaItem(NhomHanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                // Kiểm tra xem NhomHanghoa có trùng code không.
                if (db.NhomHanghoaItems.SingleOrDefault(x => x.Code == item.Code) == null)
                {
                    db.NhomHanghoaItems.Add(item);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Code của nhóm hàng hóa (" + item.Code + ") bị trùng");
                }
            }
        }
        public static void deleteNhomHanghoaItem(NhomHanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                NhomHanghoa nhomHangHoaFromDB = db.NhomHanghoaItems.Single(x => x.NhomHanghoaId == item.NhomHanghoaId);
                db.NhomHanghoaItems.Remove(nhomHangHoaFromDB);
                db.SaveChanges();
            }
        }

        // ~GiaHanghoa
        public static List<GiaHanghoa> GetGiaHanghoaItems()
        {
            using (POSDBContext db = new POSDBContext())
            {
                return db.GiaHanghoaItems.ToList();
            }
        }
        public static void addGiaHanghoaItem(GiaHanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                // Kiểm tra xem Giá hàng hóa có trùng code không.
                if (db.GiaHanghoaItems.SingleOrDefault(x => x.Code == item.Code) == null)
                {
                    db.GiaHanghoaItems.Add(item);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Code của giá hàng hóa (" + item.Code + ") bị trùng");
                }
            }
        }
        public static void deleteGiaHanghoaItem(GiaHanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                GiaHanghoa giaHangHoaFromDB = db.GiaHanghoaItems.Single(x => x.GiaHanghoaId == item.GiaHanghoaId);
                db.GiaHanghoaItems.Remove(giaHangHoaFromDB);
                db.SaveChanges();
            }
        }
        public static void editGiaHanghoaItem(GiaHanghoa item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                GiaHanghoa giaHangHoaFromDB = db.GiaHanghoaItems.Single(x => x.GiaHanghoaId == item.GiaHanghoaId);

                if (db.GiaHanghoaItems.SingleOrDefault(x => x.Code == item.Code) != null &&
                    item.Code != giaHangHoaFromDB.Code) // Kiểm tra thêm trường hợp nó edit chính nó
                {
                    throw new Exception("Code của giá hàng hóa (" + item.Code + ") bị trùng");
                }
                else
                {
                    giaHangHoaFromDB.Gia = item.Gia;
                    giaHangHoaFromDB.NgayApdung = item.NgayApdung;
                    giaHangHoaFromDB.Code = item.Code;
                    db.SaveChanges();
                }
            }
        }

        // ~ Nhanvien ~
        public static List<Nhanvien> GetNhanvienItems()
        {
            using (POSDBContext db = new POSDBContext())
            {
                return db.NhanvienItems.ToList();
            }
        }
        public static void addNhanvienItem(Nhanvien item, User user)
        {
            using (POSDBContext db = new POSDBContext())
            {
                // Kiểm tra xem tên user này đã có chưa?
                if (db.UserItems.SingleOrDefault(x => x.UserId == user.UserId) != null)
                {
                    throw new Exception("Tên tài khoản \"" + user.UserId + "\" bị trùng.");
                }

                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        user.NhanvienId = item.NhanvienId;

                        db.NhanvienItems.Add(item);
                        db.UserItems.Add(user);

                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }
        public static void editNhanvienItems(Nhanvien item, User user)
        {
            using (POSDBContext db = new POSDBContext())
            {
                
                User userFromDB = db.UserItems.SingleOrDefault(x => x.NhanvienId == item.NhanvienId);

                if (db.UserItems.SingleOrDefault(x => x.UserId == user.UserId) != null &&
                    user.UserId != userFromDB.UserId) // Kiểm tra thêm trường hợp nó edit chính nó
                {
                    throw new Exception("Tên tài khoản (" + user.UserId + ") bị trùng");
                }

                Nhanvien nhanvienFromDB = db.NhanvienItems.Single(x => x.NhanvienId == item.NhanvienId);
                nhanvienFromDB.Hoten = item.Hoten;
                nhanvienFromDB.Diachi = item.Diachi;
                nhanvienFromDB.Email = item.Email;
                nhanvienFromDB.Sdt = item.Sdt;

                if (userFromDB != null)
                {
                    userFromDB.UserId = user.UserId;
                    userFromDB.Password = user.Password;
                }
                else
                {
                    db.UserItems.Add(new User
                   {
                       UserId = user.UserId,
                       NhanvienId = item.NhanvienId,
                       Password = user.Password
                   });
                }
                db.SaveChanges();
            }
        }
        public static void deleteNhanvienItem(Nhanvien item)
        {
            using (POSDBContext db = new POSDBContext())
            {
                Nhanvien nhanvienFromDB = db.NhanvienItems.Single(x => x.NhanvienId == item.NhanvienId);
                db.NhanvienItems.Remove(nhanvienFromDB);
                db.SaveChanges();
            }
        }

        //  ~ User ~
        public static List<User> GetUserItems()
        {
            using (POSDBContext db = new POSDBContext())
            {
                return db.UserItems.ToList();
            }
        }
    }
}