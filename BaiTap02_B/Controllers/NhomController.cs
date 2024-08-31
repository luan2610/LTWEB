using Microsoft.AspNetCore.Mvc;

namespace BaiTap02_B.Controllers
{
    public class NhomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
