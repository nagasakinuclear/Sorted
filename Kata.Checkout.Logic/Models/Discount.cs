namespace Kata.Checkout.Logic.Models
{
    public class Discount
    {
        public decimal Value { get; }
        public int ItemAmount { get; }
        public string ItemSku { get; }

        public Discount(decimal value, int itemAmount, string itemSku)
        {
            Value = value;
            ItemAmount = itemAmount;
            ItemSku = itemSku;
        }
    }
}
