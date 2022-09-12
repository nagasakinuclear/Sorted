using System.Collections.Generic;
using Kata.Checkout.Logic.Models;

namespace Kata.Checkout.Logic.Interfaces
{
    public interface IStoreInventory
    {
        IDictionary<string, InventoryItem> InventoryItems { get; }
    }
}
