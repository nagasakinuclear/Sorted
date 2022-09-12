using System;
using System.Collections.Generic;
using System.Linq;
using Kata.Checkout.Logic.Interfaces;
using Kata.Checkout.Logic.Models;

namespace Kata.Checkout.Logic
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IStoreInventory _inventory;
        private readonly IDiscountService _discountService;
        private readonly IList<ITransactable> _transactionItems;

        public CheckoutService(IStoreInventory inventory, IDiscountService salesService)
        {
            _inventory = inventory;
            _discountService = salesService;
            _transactionItems = new List<ITransactable>();
        }

        public void Scan(string sku)
        {
            if (!TryFindItemBySku(sku, out ITransactable scannedItem))
            {
                throw new ArgumentException($"{sku} is not a valid item SKU.", nameof(sku));
            }

            _transactionItems.Add(scannedItem);
        }

        private bool TryFindItemBySku(string sku, out ITransactable foundItem)
        {
            if (_inventory.InventoryItems.TryGetValue(sku, out InventoryItem searchItem))
            {
                foundItem = searchItem;
                return true;
            }

            foundItem = null;
            return false;
        }

        private decimal CalculateDiscounts()
        {
            var itemsByGroup = _transactionItems.GroupBy(ti => ti.ItemSku);

            decimal totalDiscount = 0;

            foreach (var itemByGroup in itemsByGroup)
            {
                totalDiscount += _discountService.GetItemDiscount(itemByGroup.Key, itemByGroup.Count()) * itemByGroup.Count();
            }

            return totalDiscount;
        }

        public decimal TotalPrice => _transactionItems.Sum(t => t.TransactionValue) - CalculateDiscounts();
    }
}
