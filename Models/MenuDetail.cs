using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BtlNhom6.Models
{
    public class MenuDetail
    {
        [Key]
        public int MenuID { get; set; }
        [Key]
        public int DishID { get; set; }
        public int Quantity { get; set; }
        public float? TotalPrice { get; set; }
        public Dish? Dish { get; set; }
        public Menu? Menu { get; set; }
    }
}
