using System.ComponentModel.DataAnnotations;

namespace BT07.Models
{
    public class TheLoai
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="không được để trống tên thể loại!")]
        [Display(Name ="Thể Loại")]
        public string Name { get; set; }
		[Required(ErrorMessage = "không đúng định dạng ngày tháng năm")]
		[Display(Description = "Ngày Tạo")]
		public DateTime Description { get; set; } = DateTime.Now;
    }
}
