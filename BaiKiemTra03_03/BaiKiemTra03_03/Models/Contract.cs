using System.ComponentModel.DataAnnotations;

namespace BaiKiemTra03_03.Models
{
    public class Contract
    {
        [Key]
        public int Contract_Id { get; set; }
        [Required]
        public string Contract_Name { get; set; }
        [Required]
        public DateTime signing_date { get; set; }
        public string customer {  get; set; }
        public int contract_value { get; set; }
    }
}
