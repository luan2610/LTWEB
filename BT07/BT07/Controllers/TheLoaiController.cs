using BT07.Data;
using BT07.Models;
using Microsoft.AspNetCore.Mvc;

namespace BT07.Controllers
{
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TheLoaiController(ApplicationDbContext db)
        {
              _db = db;
        }
        public IActionResult Index()
        {
            var theloai = _db.TheLoai.ToList();
            ViewBag.theloai = theloai;
            return View();
        }
        [HttpGet]
		public IActionResult Create()
		{
			
			return View();
		}
        [HttpPost]
		public IActionResult Create(TheLoai theloai)
		{
            if (ModelState.IsValid) 
            {//thêm thông tin thể loại
                _db.TheLoai.Add(theloai);
                //lưu lại
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
			return View();
		}
        [HttpGet]
		public IActionResult Edit(int Id)
		{
            if (Id == 0) 
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(Id);

			return View();
		}
         [HttpPost]
		public IActionResult Edit(TheLoai theloai)
		{
			if (ModelState.IsValid)
			{//thêm thông tin thể loại
				_db.TheLoai.Update(theloai);
				//lưu lại
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}
		[HttpGet]
		public IActionResult Delete(int Id)
		{
			if (Id == 0)
			{
				return NotFound();
			}
			var theloai = _db.TheLoai.Find(Id);

			return View();
		}
		[HttpPost]
		public IActionResult DeleteConfirm(int id)
		{
			var theloai =_db.TheLoai.Find(id);
			if (theloai == null)
			{
				return NotFound();
			}
			_db.TheLoai.Remove(theloai);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}

