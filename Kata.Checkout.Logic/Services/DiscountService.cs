using Kata.Checkout.Logic.Interfaces;
using Kata.Checkout.Logic.Models;
using System.Collections.Generic;

namespace Kata.Checkout.Logic.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly Dictionary<string, Discount> _discounts = new Dictionary<string, Discount>();

        public DiscountService(Dictionary<string, Discount> discounts)
        {
            _discounts = discounts;
        }

        public decimal GetItemDiscount(string sku, int itemAmount)
        {
            Discount discount;

            if (!_discounts.TryGetValue(sku, out discount))
            {
                return 0;
            }

            if (discount.ItemAmount <= itemAmount)
            {
                return discount.Value;
            }

            return 0;
        }
    }
}
