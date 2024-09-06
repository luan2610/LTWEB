using BaiTapVeNha_20.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BaiTapVeNha_20.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult MayTinh(double a, double b, string pheptinh)
        {
            double ketqua = 0;

            switch (pheptinh)
            {
                case "cong":
                    ketqua = a + b;
                    break;
                case "tru":
                    ketqua = a - b;
                    break;
                case "nhan":
                    ketqua = a * b;
                    break;
                case "chia":

                    if (b == 0)
                    {
                        ViewBag.Error = "Không thể chia cho 0";
                        return View();
                    }
                    ketqua = a / b;
                    break;
                default:
                    ViewBag.Error = "Phép tính không hợp lệ";
                    return View();
            }

            ViewBag.KetQua = ketqua;
            return View();
        }
    }
}