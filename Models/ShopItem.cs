using System.ComponentModel;

namespace VivesShop.Ui.Mvc.Models
{
    public class ShopItem
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public required string Name { get; set; }

        [DisplayName("Price")]
        public required double Price { get; set; }

    }
}
