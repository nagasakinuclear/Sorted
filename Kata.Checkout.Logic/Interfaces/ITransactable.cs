namespace Kata.Checkout.Logic.Interfaces
{
    public interface ITransactable
    {
        string ItemSku { get; }
        decimal TransactionValue { get; }
    }
}
