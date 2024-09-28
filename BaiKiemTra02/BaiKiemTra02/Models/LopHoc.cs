using System.ComponentModel.DataAnnotations;
namespace BaiKiemTra02.Models
{
    public class LopHoc
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên lớp học là bắt buộc.")]
        public string TenLopHoc { get; set; }

        [Range(2000, 2024, ErrorMessage = "Năm nhập học phải nằm trong khoảng từ 2000 đến 2024.")]
        public int NamNhapHoc { get; set; }

        [Range(2000, 3000, ErrorMessage = "Năm ra trường phải nằm trong khoảng từ 2000 đến 3000.")]
        public int NamRaTruong { get; set; }

        [Range(1, 5000, ErrorMessage = "Số lượng sinh viên phải nằm trong khoảng từ 1 đến 5000.")]
        public int SoLuongSinhVien { get; set; }
    }
}

