using System.Collections.Generic;
using Kata.Checkout.Logic.Interfaces;
using Kata.Checkout.Logic.Models;

namespace Kata.Checkout.Logic.Services
{
    public class DefaultStoreInventory : IStoreInventory
    {
        public DefaultStoreInventory()
        {
            InventoryItems = new Dictionary<string, InventoryItem>
            {
                { "A99", new InventoryItem("A99", "Apple", 0.5m) },
                { "B15", new InventoryItem("B15", "Banana", 0.3m) },
                { "C40", new InventoryItem("C40", "Chocolate", 0.6m) }
            };
        }

        public IDictionary<string, InventoryItem> InventoryItems { get; }
    }
}
