namespace Kata.Checkout.Logic.Interfaces
{
    public interface IDiscountService
    {
        decimal GetItemDiscount(string sku, int itemAmount);
    }
}