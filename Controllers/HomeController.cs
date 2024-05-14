using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VivesShop.Ui.Mvc.Core;
using VivesShop.Ui.Mvc.Models;
using VivesShop.Ui.Mvc.Services;

namespace VivesShop.Ui.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly VivesShopDbContext _vivesShopDbContext;

        private readonly ShoppingCartService _shoppingCartService;

        private ShoppingCart _shoppingCart;

        public HomeController(VivesShopDbContext vivesShopDbContext, ShoppingCartService shoppingCartService)
        {
            _vivesShopDbContext = vivesShopDbContext;
            _shoppingCartService = shoppingCartService;

            // Shopping cart updates
            _shoppingCartService.ShopItemAdded += (sender, args) =>
            {
                AddItem(args);
            };

            _shoppingCartService.ShopItemRemoved += (sender, args) =>
            {
                RemoveItem(args);
            };

            _shoppingCartService.ShopItemEdited += (itemId, shopItem) =>
            {
                EditItem(itemId, shopItem);
            };



            // Initialize shopping cart
            var shopItems = _vivesShopDbContext.ShopItems.ToList();
            _shoppingCart = ShoppingCart.getInstance(shopItems);
        }

        public IActionResult Index()
        {
            return View(_shoppingCart);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View(_shoppingCart);
        }

        public void RemoveItem(int id)
        {
            var shopItem = _shoppingCart.ShopItems
                .FirstOrDefault(item => item.Id == id);

            if (shopItem != null)
            {
                _shoppingCart.ShopItems.Remove(shopItem);
            }
        }

        public void AddItem(ShopItem shopItem)
        {
            if (shopItem != null)
            {
                _shoppingCart.ShopItems.Add(shopItem);
            }
        }

        public void EditItem(int id, ShopItem shopItem)
        {
            var cartShopItem = _shoppingCart.ShopItems
                .FirstOrDefault(item => item.Id == id);

            if (cartShopItem != null)
            {
                //Mapping
                cartShopItem.Name = shopItem.Name;
                cartShopItem.Price = shopItem.Price;
            }
        }

        [HttpPost]
        public IActionResult AddToCart([FromForm] int shopItemId)
        {
            _shoppingCart.IsPaid = false;
            _shoppingCart.AddToShoppingCart(shopItemId);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFromCart([FromForm] int shopItemId)
        {
            _shoppingCart.RemoveFromShoppingCart(shopItemId);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Pay([FromForm] string paymentMethod)
        {
            if (!_shoppingCart.IsPaid)
            {
                _shoppingCart.IsPaid = true;
                _shoppingCart.PaymentMethod = paymentMethod;
                _shoppingCart.ShoppingCartItems.Clear();
                _shoppingCart.TotalAmount = 0;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
