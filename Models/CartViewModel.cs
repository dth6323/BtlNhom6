using System.ComponentModel.DataAnnotations.Schema;

namespace BtlNhom6.Models
{
	[NotMapped]
	public class CartViewModel
	{
		public List<Dish> CartItem { get; set; }
		public List<int> Quantity { get; set; }
		public List<int> EmployeeID { get; set; }
	}
}
