using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project.Models
{
    public class TheLoai
    {
        [Key] 
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được để trống tên thể loại")]
        [Display (Name= "Thể loại")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không đúng định dạng ngày tạo!")]
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
