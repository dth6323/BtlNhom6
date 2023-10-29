using BtlNhom6.Areas.Identity.Data;
using BtlNhom6.Data;
using BtlNhom6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System.Security.Claims;

namespace BtlNhom6.Controllers
{
    public class CartController : Controller
	{
		private AuthDbContext db;
		List<CartViewModel> cartItems;
		List<int> quantity;
		CartViewModel viewModel;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(AuthDbContext db, CartViewModel viewModel,
           UserManager<ApplicationUser> userManager)
		{
			this.db = db;
			this.viewModel = viewModel;
            _userManager = userManager;
        }

		public async Task<IActionResult> IndexAsync()
		{
            var currentUser = await _userManager.GetUserAsync(User);
			if(currentUser == null)
			{
                return Redirect("Identity/Account/Register");
            }
            if (HttpContext.Session.Get<List<CartViewModel>>("Cart") == null)
			{
				viewModel.CartItem = new List<Dish>();
				viewModel.Quantity = new List<int>();

			}
			else
			{
				viewModel.CartItem = HttpContext.Session.Get<List<Dish>>("Cart");
				viewModel.Quantity = HttpContext.Session.Get<List<int>>("Quantity");
			}
			ViewBag.cartNumber = viewModel.CartItem.Count;
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult AddToCart(int id)
		{
	
			if (HttpContext.Session.Get<List<Dish>>("Cart") == null)
			{
				viewModel.CartItem = new List<Dish>();
				viewModel.Quantity = new List<int>();
			}
			else
			{
				viewModel.CartItem = HttpContext.Session.Get<List<Dish>>("Cart");
				viewModel.Quantity = HttpContext.Session.Get<List<int>>("Quantity");
			}

			// chech trùng sản phẩm
			for (int i = 0; i < viewModel.CartItem.Count; i++)
			{
				if (viewModel.CartItem[i].DishID == id)
				{
					return RedirectToAction("Index");
				}
			}

			// Thêm sản phẩm vào danh sách giỏ hàng
			var product = db.dishes.FirstOrDefault(p => p.DishID == id);
			if (product != null)
			{
				viewModel.CartItem.Add(product);
				viewModel.Quantity.Add(1);
			}

			// Lưu danh sách sản phẩm vào session
			HttpContext.Session.Set("Cart", viewModel.CartItem);
			HttpContext.Session.Set("Quantity", viewModel.Quantity);

			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Checkout()
		{

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Id == null)
			{
				return RedirectToAction("Index", "Cart");
			}
			viewModel.CartItem = HttpContext.Session.Get<List<Dish>>("Cart");
			viewModel.Quantity = HttpContext.Session.Get<List<int>>("Quantity");
			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Order(float Vat,float Discount, float Total)
		{

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
			{
				return RedirectToAction("Index", "Cart");
			}

			// Lấy thông tin giỏ hàng từ Session
			viewModel.CartItem = HttpContext.Session.Get<List<Dish>>("Cart");
			viewModel.Quantity = HttpContext.Session.Get<List<int>>("Quantity");
			// Tạo một đối tượng đơn hàng và lưu thông tin
			var order = new Menu
			{
				UserId = currentUser.Id, // Thay thế bằng Id tài khoản của người dùng (nếu có hệ thống tài khoản)
				DateTime = DateTime.Now,
				Vat = Vat,
				Discount = Discount,
				EmployeeId=1,
				totalprice = CalculateTotal(viewModel.CartItem, viewModel.Quantity) // Tính tổng số tiền
			};

			// Lưu đơn hàng vào cơ sở dữ liệu
			db.Menus.Add(order);
			db.SaveChanges();

			// Lưu chi tiết đơn hàng (sản phẩm trong đơn hàng)
			for (int i = 0; i < viewModel.CartItem.Count; i++)
			{
				var menuDetail = new MenuDetail
				{
					MenuID = order.MenuId,
					DishID = viewModel.CartItem[i].DishID,
					TotalPrice = viewModel.CartItem[i].Price,
					Quantity = viewModel.Quantity[i]
				};

				db.MenuDetails.Add(menuDetail);
			}
			db.SaveChanges();

			// Xóa giỏ hàng sau khi đặt hàng
			HttpContext.Session.Remove("Cart");
			HttpContext.Session.Remove("Quantity");
			return RedirectToAction("Index", "Cart");
		}
		// Hàm tính tổng tiền đơn hàng
		private float CalculateTotal(List<Dish> products, List<int> quantities)
		{
			float total = 0;
			for (int i = 0; i < products.Count; i++)
			{
				total += (float)products[i].Price * quantities[i];
			}
			return total;
		}
		[HttpPost]
		public IActionResult UpdateQuantity(int id, int change)
		{
			// Retrieve cart items and quantities from the session
			var cartItems = HttpContext.Session.Get<List<Dish>>("Cart");
			var quantities = HttpContext.Session.Get<List<int>>("Quantity");

			// Find the product in the cart
			var product = cartItems.FirstOrDefault(p => p.DishID == id);

			if (product != null)
			{
				// Find the index of the product in the cart
				var index = cartItems.IndexOf(product);

				// Update the quantity
				if (quantities[index] >= 1)
				{
					quantities[index] += change;
				}

				// Save the updated quantities to the session
				HttpContext.Session.Set("Quantity", quantities);
			}
			// update total
			decimal sum = 0;
			for (int i = 0; i < cartItems.Count; i++)
			{
				sum += (decimal)cartItems[i].Price * quantities[i];
			}
			Console.WriteLine(sum);
			// Return a success response
			return Ok(sum);
		}
        public IActionResult DeleteQuantity(int id)
        {
            // Retrieve cart items and quantities from the session
            var cartItems = HttpContext.Session.Get<List<Dish>>("Cart");
            var quantities = HttpContext.Session.Get<List<int>>("Quantity");

            // Find the product in the cart
            var product = cartItems.FirstOrDefault(p => p.DishID == id);

            if (product != null)
            {
                // Find the index of the product in the cart
                var index = cartItems.IndexOf(product);

                // Remove the product and its corresponding quantity from the lists
                cartItems.RemoveAt(index);
                quantities.RemoveAt(index);

                // Save the updated cart items and quantities to the session
                HttpContext.Session.Set("Cart", cartItems);
                HttpContext.Session.Set("Quantity", quantities);
            }

            // Update the total
            decimal sum = 0;
            for (int i = 0; i < cartItems.Count; i++)
            {
                sum += (decimal)cartItems[i].Price * quantities[i];
            }

            // Return the updated total as a JSON response
            return Json(sum);
        }

    }
}
