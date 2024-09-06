using baitap05.Models;
using Microsoft.AspNetCore.Mvc;

namespace baitap05.Controllers
{
    public class TheLoaiController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Ngay = "Ngày28";
            ViewBag.Thang = "Tháng02";
            ViewData["nam"] = "Năm2030";
            return View();
        }

        public IActionResult Index2()
        {
            var theloai = new TheLoaiViewModel
            {
                Id = 1,
                Name = "Trinh thám"
            };
                return View(theloai);
        }
    }
}
