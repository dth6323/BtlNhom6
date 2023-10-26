namespace BtlNhom6.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public DateTime DateTime { get; set; }
        public float? Vat { get; set; }
        public float? Discount { get; set; }
        public float? totalprice { get; set; }
    }
}
