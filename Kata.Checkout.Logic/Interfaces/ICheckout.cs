namespace Kata.Checkout.Logic.Interfaces
{
    public interface ICheckoutService
    {
        void Scan(string sku);

        public decimal TotalPrice { get; }
    }
}
