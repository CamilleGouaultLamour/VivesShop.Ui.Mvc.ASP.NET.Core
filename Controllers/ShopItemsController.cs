using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivesShop.Ui.Mvc.Core;
using VivesShop.Ui.Mvc.Models;
using System;
using VivesShop.Ui.Mvc.Services;

namespace VivesShop.Ui.Mvc.Controllers
{
    public class ShopItemsController : Controller
    {
        private readonly VivesShopDbContext _vivesShopDbContext;
        private readonly ShoppingCartService _shoppingCartService;

        public ShopItemsController(VivesShopDbContext vivesShopDbContext, ShoppingCartService shoppingCartService)
        {
            _vivesShopDbContext = vivesShopDbContext;
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var shopItems = _vivesShopDbContext.ShopItems.ToList();
            return View(shopItems);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ShopItem shopItem)
        {
            _vivesShopDbContext.ShopItems.Add(shopItem);
            _vivesShopDbContext.SaveChanges();

            _shoppingCartService.NotifyShopItemAdded(shopItem);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var shopItem = _vivesShopDbContext.ShopItems
                .FirstOrDefault(p => p.Id == id);

            if (shopItem == null)
            {
                return RedirectToAction("Index");
            }

            return View(shopItem);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, [FromForm] ShopItem shopItem)
        {
            var dbShopItem = _vivesShopDbContext.ShopItems
                .FirstOrDefault(p => p.Id == id);

            if (dbShopItem == null)
            {
                return RedirectToAction("Index");
            }

            //Mapping
            dbShopItem.Name = shopItem.Name;
            dbShopItem.Price = shopItem.Price;

            _vivesShopDbContext.SaveChanges();

            _shoppingCartService.NotifyShopItemEdited(id, shopItem);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var shopItem = _vivesShopDbContext.ShopItems
                .FirstOrDefault(p => p.Id == id);

            if (shopItem == null)
            {
                return RedirectToAction("Index");
            }

            return View(shopItem);
        }

        [HttpPost("[controller]/Delete/{id:int}")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            var shopItem = _vivesShopDbContext.ShopItems
                .FirstOrDefault(p => p.Id == id);

            if (shopItem != null)
            {
                _vivesShopDbContext.ShopItems.Remove(shopItem);

                _vivesShopDbContext.SaveChanges();

                _shoppingCartService.NotifyShopItemRemoved(id);
            }


            return RedirectToAction("Index");
        }
    }
}