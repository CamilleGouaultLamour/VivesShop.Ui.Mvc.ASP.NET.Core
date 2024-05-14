using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using VivesShop.Ui.Mvc.Models;

namespace VivesShop.Ui.Mvc.Core
{
    public class VivesShopDbContext : DbContext
    {
        public VivesShopDbContext(DbContextOptions<VivesShopDbContext> options) : base(options)
        {

        }

        public DbSet<ShopItem> ShopItems => Set<ShopItem>();

        public void Seed()
        {
            ShopItems.AddRange( new List<ShopItem>
            {
                new ShopItem { Name = "Medium friet", Price = 3},
                new ShopItem { Name = "Frikandel", Price = 2},
                new ShopItem { Name = "Cola Zero", Price = 2},
                new ShopItem { Name = "Water", Price = 1.5},
                new ShopItem { Name = "Mayonnaise", Price = 0.5},
            });

            SaveChanges();
        }
    }
}
