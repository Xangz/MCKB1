using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenDinhSang_241230832.Models;
using System.Diagnostics;

namespace NguyenDinhSang_241230832.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLTaiSanContext _db;

        public HomeController(QLTaiSanContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.TenSV = "Nguyễn Đình Sang";

            var danhSachTaiSan = await _db.TaiSans
                .Include(t => t.MaLoaiNavigation)
                .Where(t => t.NguyenGia != null && t.NguyenGia >= 20)
                .OrderBy(t => t.MaTs)
                .ToListAsync();

            return View(danhSachTaiSan);
        }
        public async Task<IActionResult> GetTaiSanByLoai(int maLoai)
        {
            List<TaiSan> danhSachTaiSan;

            if (maLoai == 0)
            {
                danhSachTaiSan = await _db.TaiSans
                    .Include(t => t.MaLoaiNavigation)
                    .Where(t => t.NguyenGia != null && t.NguyenGia >= 20)
                    .OrderBy(t => t.MaTs)
                    .ToListAsync();
            }
            else
            {
                danhSachTaiSan = await _db.TaiSans
                    .Include(t => t.MaLoaiNavigation)
                    .Where(t => t.MaLoai == maLoai)
                    .OrderBy(t => t.MaTs)
                    .ToListAsync();
            }

            return PartialView("NguyenDinhSang_MainContent", danhSachTaiSan);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.MaLoai = new SelectList(await _db.LoaiTaiSans.OrderBy(l => l.MaLoai).ToListAsync(), "MaLoai", "TenLoai");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaiSan taiSan)
        {
            if (ModelState.IsValid)
            {
                _db.TaiSans.Add(taiSan);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MaLoai = new SelectList(await _db.LoaiTaiSans.OrderBy(l => l.MaLoai).ToListAsync(), "MaLoai", "TenLoai", taiSan.MaLoai);
            return View(taiSan);
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
    }
}
