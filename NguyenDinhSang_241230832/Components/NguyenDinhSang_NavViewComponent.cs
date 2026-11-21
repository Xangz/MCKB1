using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenDinhSang_241230832.Models;
namespace NguyenDinhSang_241230832.Components
{
    public class NguyenDinhSang_NavViewComponent : ViewComponent
    {
        private readonly QLTaiSanContext _db;

        public NguyenDinhSang_NavViewComponent(QLTaiSanContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loai = await _db.LoaiTaiSans
                 .OrderBy(l => l.MaLoai)
                 .ToListAsync();

            ViewBag.TenSV = "Nguyễn Đình Sang";
            return View(loai);
        }
    }
}