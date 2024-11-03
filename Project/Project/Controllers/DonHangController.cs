using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.Migrations;
using Project.Models;

namespace Project.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DonHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DonHangController> _logger;
        public DonHangController(ApplicationDbContext db, ILogger<DonHangController> logger)
        {
            _db = db;
            _logger = logger;
        }
        public IActionResult Index()
        {
            IEnumerable<HoaDon> hoadon = _db.HoaDon
                                .Include(h => h.ApplicationUser)
                                .Where(h => !h.IsCancelled && (h.OrderStatus == "Đang xác nhận" || h.OrderStatus == "Đã xác nhận"))
                                .ToList();
            return View(hoadon);
        }
        public IActionResult ChiTiet(int id)
        {
            var hoadon = _db.HoaDon.Include(h => h.ApplicationUser).Include(h => h.ChiTietHoaDon).ThenInclude(ct => ct.SanPham).FirstOrDefault(h => h.Id == id);
            if (hoadon == null)
            {
                return NotFound();
            }
            return View(hoadon);
        }
        [HttpPost]
        public IActionResult XacNhanDonHang(int id)
        {
            var hoadon = _db.HoaDon.Find(id);
            if (hoadon == null)
            {
                return NotFound();
            }

            if (hoadon.OrderStatus == "Đang xác nhận")
            {
                hoadon.IsConfirmed = true;
                hoadon.ConfirmedDate = DateTime.Now;
                hoadon.OrderStatus = "Đã xác nhận";
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult HuyDonHang(int id, string cancellationReason)
        {
            var hoaDon = _db.HoaDon.Find(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            if (hoaDon.OrderStatus == "Đang xác nhận" || hoaDon.OrderStatus == "Đã xác nhận")
            {
                // Cập nhật trạng thái và lý do hủy
                hoaDon.IsCancelled = true;
                hoaDon.CancellationReason = cancellationReason;
                hoaDon.OrderStatus = "Đã bị hủy"; // Cập nhật trạng thái
                hoaDon.CancelledDate = DateTime.Now;

                _db.SaveChanges();
            }

            // Chuyển hướng đến trang đơn hàng đã hủy
            return RedirectToAction("DonHangDaHuy");
        }
        public IActionResult DonHangDaHuy()
        {
            var danhSachDonHangDaHuy = _db.HoaDon
                               .Include(h => h.ApplicationUser) // Nếu bạn cần thông tin người dùng
                               .Where(h => h.IsCancelled) // Chỉ lấy đơn hàng đã hủy
                               .ToList();
            return View(danhSachDonHangDaHuy);
        }
    }
}