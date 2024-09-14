using BaiTapKiemTra01.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapKiemTra01.Controllers
{
	public class TaiKhoanController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult DangKy(TaiKhoanViewModel taiKhoan )
		{
            if (taiKhoan.Username != null)
            {
                return Content(taiKhoan.Username);
            }

            return View();
		}
	}
}
