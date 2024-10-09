using BaiKiemTra03_03.Data;
using BaiKiemTra03_03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiKiemTra03_03.Controllers

{
    [Area("Admin")]
    public class ContractController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ContractController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Contract> contract = _db.Contract.Include("Contract").ToList();
            return View();
        }

    }
}
