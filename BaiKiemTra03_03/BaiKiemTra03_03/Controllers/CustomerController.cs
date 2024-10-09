using BaiKiemTra03_03.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace BaiKiemTra03_03.Controllers
{
    [Area("Admin")]

    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var customer = _db.Customer.ToList();
            ViewBag.TheLoai = customer;
            return View();
          
        }
    }
}
