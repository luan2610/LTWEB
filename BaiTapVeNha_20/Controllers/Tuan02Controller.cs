using Microsoft.AspNetCore.Mvc;

namespace BaiTapVeNha_20.Controllers
{
    public class Tuan02Controller : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Hoten = "Nguyễn Quốc Luân";
            ViewBag.MSSV = "1822041279";
            ViewData["Nam"] = "2004";
            return View();
        }
    }
}
