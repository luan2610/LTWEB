using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaiKiemTra03_03.Models
{
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }
        [Required]
        public string Customer_Name { get; set; }
        [Required]
        public string address { get; set; }
        public int phone_number { get; set; }
        public string email { get; set; }
        
    }
}
