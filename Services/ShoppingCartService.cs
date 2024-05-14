using System;
using VivesShop.Ui.Mvc.Models;

namespace VivesShop.Ui.Mvc.Services
{
    public class ShoppingCartService
    {
        public event EventHandler<ShopItem> ShopItemAdded;
        public event EventHandler<int> ShopItemRemoved;

        public delegate void ShopItemEditedEventHandler(int itemId, ShopItem shopItem);
        public event ShopItemEditedEventHandler ShopItemEdited;

        public void NotifyShopItemAdded(ShopItem shopItem)
        {
            ShopItemAdded?.Invoke(this, shopItem);
        }

        public void NotifyShopItemRemoved(int itemId)
        {
            ShopItemRemoved?.Invoke(this, itemId);
        }

        public void NotifyShopItemEdited(int itemId, ShopItem shopItem)
        {
            ShopItemEdited?.Invoke(itemId, shopItem);
        }

    }
}
