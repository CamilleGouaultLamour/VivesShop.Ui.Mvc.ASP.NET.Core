namespace VivesShop.Ui.Mvc.Models
{
    public class ShoppingCart
    {
        public List<ShopItem> ShopItems { get; set; }
        public List<ShopItem> ShoppingCartItems { get; set; }
        public double? TotalAmount { get; set; }

        public bool IsPaid { get; set; }

        public string PaymentMethod { get; set; }

        private static ShoppingCart? ShoppingCartInstance;


        private ShoppingCart(List<ShopItem> shopItems)
        {
            ShopItems = shopItems;

            ShoppingCartItems = new List<ShopItem>();

            TotalAmount = 0;

            IsPaid = false;

            PaymentMethod = "";
        }

        public static ShoppingCart getInstance(List<ShopItem> shopItems)
        {
            if (ShoppingCartInstance == null)
            {
                return ShoppingCartInstance = new ShoppingCart(shopItems);
            }
            else
            {
                return ShoppingCartInstance;
            }
        }

        public void AddToShoppingCart(int id)
        {
            id = id - 1;

            ShoppingCartItems.Add(ShopItems[id]);

            TotalAmount += ShopItems[id].Price;
        }

        public void RemoveFromShoppingCart(int id)
        {
            id = id - 1;

            ShoppingCartItems.Remove(ShopItems[id]);

            TotalAmount -= ShopItems[id].Price;
        }

    }
}