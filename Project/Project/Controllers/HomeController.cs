using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Diagnostics;
using System.Security.Claims;


namespace Project.Controllers 
{
	[Area("Customer")]


	public class HomeController : Controller
	{

		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _db;
		public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
		{
			_logger = logger;
			_db = db;
		}
		public IActionResult Index()
		{
			IEnumerable<SanPham> sanpham = _db.SanPham.Include(sp => sp.TheLoai).ToList();
			return View(sanpham);
		}
        [HttpGet]
        public IActionResult Details(int sanphamid)
        {
            var sanPham = _db.SanPham.Include("TheLoai").FirstOrDefault(sp => sp.Id == sanphamid);
            if (sanPham == null)
            {
                return NotFound();
            }
            GioHang giohang = new GioHang()
            {
                SanPhamId = sanphamid,
                SanPham = _db.SanPham.Include("TheLoai").FirstOrDefault(sp => sp.Id == sanphamid),
                Quantity = 1
            };
            return View(giohang);
        }
        [HttpGet]
        public IActionResult FilterByTheLoai(int id)
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.Include("TheLoai").Where(sp => sp.TheLoai.Id == id).ToList();
            return View("Index", sanpham);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(GioHang giohang)
        {
            // Lay thong tin dang nhap
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            giohang.ApplicationUserId = claim.Value;
            //kiem tra san pham da co trong gio hang chua
            var giohangdb = _db.GioHang.FirstOrDefault(sp => sp.SanPhamId == giohang.SanPhamId && sp.ApplicationUserId==giohang.ApplicationUserId);
            if (giohangdb == null)
            {
                _db.GioHang.Add(giohang);
            }
            else
            {
                giohangdb.Quantity += giohang.Quantity;
            }    

            // Luu xuong co so du lieu
            _db.GioHang.Add(giohang);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
