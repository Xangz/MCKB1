using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace NguyenDinhSang_241230832.Models
{
    [ModelMetadataType(typeof(TaiSanMetadata))]
    public partial class TaiSan
    {
    }

    public class TaiSanMetadata
    {
        [Display(Name = "Loại tài sản")]
        [Required(ErrorMessage = "Vui lòng chọn loại tài sản")]
        public int MaLoai { get; set; }

        [Display(Name = "Tên tài sản")]
        [Required(ErrorMessage = "Tên tài sản không được để trống")]
        public string TenTs { get; set; } = null!;

        [Display(Name = "Nguyên giá (triệu)")]
        [Required(ErrorMessage = "Nguyên giá là bắt buộc")]
        [Range(1, 2000, ErrorMessage = "Nguyên giá phải là số từ 1 đến 2000")]
        public decimal? NguyenGia { get; set; }

        [Display(Name = "Tên file ảnh")]
        [RegularExpression(@"^.+\.(jpg|jpeg|png|webp)$", ErrorMessage = "Ảnh phải có đuôi: .jpg, .jpeg, .png hoặc .webp")]
        public string? Anh { get; set; }
    }
}
