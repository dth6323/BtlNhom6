using BtlNhom6.Areas.Identity.Data;

namespace BtlNhom6.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int EmployeeId { get; set; }
        public float? Vat { get; set; }
        public float? Discount { get; set; }
        public float? totalprice { get; set; }
        public ApplicationUser User { get; set; }
        public Employee? Employee { get; set; }
    }
}
