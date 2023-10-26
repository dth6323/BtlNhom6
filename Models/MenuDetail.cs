namespace BtlNhom6.Models
{
    public class MenuDetail
    {
        public int MenuID { get; set; }
        public int DishID { get; set; }
        public int EmployeeID { get; set; }
        public int Quantity { get; set; }
        public float? TotalPrice { get; set; }
        public Dish? Dish { get; set; }
        public Menu? Menu { get; set; }
        public Employee? Employee { get; set; }
    }
}
