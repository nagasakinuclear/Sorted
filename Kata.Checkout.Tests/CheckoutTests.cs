using System;
using System.Collections.Generic;
using Kata.Checkout.Logic.Interfaces;
using Kata.Checkout.Logic.Services;
using Moq;
using Xunit;

namespace Kata.Checkout.Tests
{
    public class CheckoutTests
    {
        private const string AppleSku = "A99";
        private const string BananaSku = "B15";
        private const string ChocolateSku = "C40";

        private readonly IStoreInventory _inventory;
        private readonly IDiscountService _looseSalesService;

        public CheckoutTests()
        {
            _inventory = new DefaultStoreInventory();
            _looseSalesService = 
                new Mock<IDiscountService>(MockBehavior.Loose)
                .Object;
        }

        [Theory]
        [InlineData(AppleSku)]
        [InlineData(BananaSku)]
        [InlineData(ChocolateSku)]
        public void CheckoutCanScanValidItemWithoutException(string sku)
        {
            ICheckoutService checkout = new Logic.CheckoutService(_inventory, _looseSalesService);

            checkout.Scan(sku);
        }

        [Fact]
        public void CheckoutScanInValidItemThrowsArgumentError()
        {
            ICheckoutService checkout = new Logic.CheckoutService(_inventory, _looseSalesService);

            const string invalidSku = "D32";

            Assert.Throws<ArgumentException>(() => checkout.Scan(invalidSku));
        }

        [Fact]
        public void CheckoutReturnsCorrectTotalPriceForNoItems()
        {
            ICheckoutService checkout = new Logic.CheckoutService(_inventory, _looseSalesService);

            const decimal anticipatedPrice = 0m;

            Assert.Equal(anticipatedPrice, checkout.TotalPrice);
        }

        [Fact]
        public void CheckoutReturnsCorrectTotalPriceForTwoItems()
        {
            ICheckoutService checkout = new Logic.CheckoutService(_inventory, _looseSalesService);

            checkout.Scan(AppleSku);
            checkout.Scan(ChocolateSku);

            const decimal anticipatedPrice = 1.1m;

            Assert.Equal(anticipatedPrice, checkout.TotalPrice);
        }

        [Theory]
        [InlineData(AppleSku)]
        [InlineData(BananaSku)]
        [InlineData(ChocolateSku)]
        public void CheckoutReturnsCorrectTotalPriceWithDiscounts(string sku)
        {
            var discountConfig = new Dictionary<string, (int amount, decimal discount)>()
            {
                { AppleSku, (4, 0.1m) },
                { BananaSku, (4, 0.1m) },
                { ChocolateSku, (4, 0.1m) },
            };

            var discountMock = new Mock<IDiscountService>();

            foreach (var itemConfig in discountConfig)
            {
                discountMock.Setup(x => x.GetItemDiscount(itemConfig.Key, itemConfig.Value.amount))
                    .Returns(itemConfig.Value.discount);
            }

            ICheckoutService checkout = new Logic.CheckoutService(_inventory, discountMock.Object);

            for (int i = 0; i < discountConfig[sku].amount; i++)
            {
                checkout.Scan(sku);
            }

            var currentPrice = _inventory.InventoryItems[sku].TransactionValue;
            var expectedPrice = (currentPrice - discountConfig[sku].discount) * discountConfig[sku].amount;

            Assert.Equal(expectedPrice, checkout.TotalPrice);
        }
    }
}