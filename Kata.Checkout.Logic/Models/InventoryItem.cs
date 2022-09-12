using Kata.Checkout.Logic.Interfaces;

namespace Kata.Checkout.Logic.Models
{
    public class InventoryItem : ITransactable
    {
        public string Name { get; }
        public string ItemSku { get; }
        public decimal TransactionValue { get; }

        public InventoryItem(string sku, string name, decimal price)
        {
            Name = name;
            ItemSku = sku;
            TransactionValue = price;
        }
    }
}
