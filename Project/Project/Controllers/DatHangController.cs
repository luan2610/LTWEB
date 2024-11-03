using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.Migrations;
using Project.Models;
using System.Security.Claims;

namespace Project.Controllers
{
    [Area("Customer")]
    public class DatHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DatHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<HoaDon> hoadon = _db.HoaDon
                    .Include(h => h.ApplicationUser)
                    .Where(h => !h.IsCancelled && (h.OrderStatus == "Đang xác nhận" || h.OrderStatus == "Đã xác nhận" || h.OrderStatus == "Đang chờ hủy")) // Điều chỉnh điều kiện lọc
                    .ToList();
            return View(hoadon);
        }
        public IActionResult ChiTiet(int id)
        {
            var dathang = _db.HoaDon
                             .Include(h => h.ApplicationUser)
                             .Include(h => h.ChiTietHoaDon)
                             .ThenInclude(ct => ct.SanPham)
                             .FirstOrDefault(h => h.Id == id);

            if (dathang == null)
            {
                return NotFound();
            }

            // Kiểm tra quyền truy cập (chỉ chủ sở hữu đơn hàng mới được xem chi tiết)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (dathang.ApplicationUserId != userId)
            {
                return Unauthorized(); // Trả về 401 Unauthorized
            }

            return View(dathang);
        }
        [HttpPost]
        public async Task<IActionResult> HuyDonHang(int id, string cancellationReason)
        {
            var hoaDon = await _db.HoaDon.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (hoaDon.ApplicationUserId != userId)
            {
                return Unauthorized();
            }

            if (hoaDon.OrderStatus == "Đang xác nhận")
            {
                // Hủy đơn hàng ngay
                hoaDon.IsCancelled = true;
                hoaDon.CancellationReason = cancellationReason;
                hoaDon.OrderStatus = "Đã hủy";
                hoaDon.CancelledDate = DateTime.Now;
            }
            else if (hoaDon.OrderStatus == "Đã xác nhận")
            {
                // Đặt trạng thái thành "Đang chờ hủy"
                hoaDon.IsCancelled = false;
                hoaDon.CancellationReason = cancellationReason;
                hoaDon.OrderStatus = "Đang chờ hủy";
                hoaDon.CancelledDate = DateTime.Now;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
