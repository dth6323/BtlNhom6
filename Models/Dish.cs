namespace BtlNhom6.Models
{
    public class Dish
    {
        public Dish() {
            MenuDetails = new HashSet<MenuDetail>();
        }
        public int DishID { get; set; }
        public string DishName { get; set; }
        public float Price { get; set; }
        public string? NameImage { get; set; }
        public string? Making { get; set; }
        public string? Request { get; set; }
        public float? Discount { get; set; }
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
