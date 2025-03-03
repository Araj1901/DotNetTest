using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CartManagementMVC.Models;
using CartManagementMVC.Repositories;

namespace CartManagementMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        private int GetUserId()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return 0; 
            }

            return userId.Value;
        }

        public IActionResult Index()
        {
            int userId = GetUserId();
            if (userId == 0) return RedirectToAction("Login", "User"); // Redirect if user not logged in

            List<Cart> cartItems = _cartRepository.GetCartListByUserId(userId);
            return View(cartItems);
        }

        public IActionResult AddToCart(int productId)
        {
            int userId = GetUserId();
            if (userId == 0) return RedirectToAction("Login", "User");

            _cartRepository.SaveProductInCart(userId, productId, 1);
            return RedirectToAction("Index", "Product");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            int userId = GetUserId();
            if (userId == 0) return RedirectToAction("Login", "User");

            _cartRepository.RemoveProductFromCart(userId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCartQuantity(int productId, int quantity)
        {
            int userId = GetUserId();
            if (userId == 0) return RedirectToAction("Login", "User");

            _cartRepository.UpdateCartQuantity(userId, productId, quantity);
            return RedirectToAction("Index");
        }
    }
}
