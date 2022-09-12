using Kata.Checkout.Logic.Interfaces;
using Kata.Checkout.Logic.Models;
using Kata.Checkout.Logic.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Kata.Checkout.Tests
{
    public class DiscountServiceTests
    {
        private readonly IDiscountService discountService;

        private const string AppleSku = "A99";
        private const string BananaSku = "B15";
        private const string ChocolateSku = "C40";

        public DiscountServiceTests()
        {
            discountService =
                new DiscountService(new Dictionary<string, Discount>()
            {
                { "A99", new Discount(0.2m, 4, "A99") },
                { "B15", new Discount(0.1m, 2, "B15") },
                { "C40", new Discount(0.2m, 2, "C40") },
            });
        }

        [Fact]
        public void DiscountServiceReturnsDiskountNoError()
        {
            var discount = discountService.GetItemDiscount(AppleSku, 4);

            Assert.Equal(0.2m, discount);
        }

        [Fact]
        public void DiscountServiceReturnsZeroUnsupported()
        {
            var discount = discountService.GetItemDiscount("Unsupported sku", 4);

            Assert.Equal(0, discount);
        }


        [Fact]
        public void DiscountServiceReturnsZeroLessThanExpected()
        {
            var discount = discountService.GetItemDiscount(AppleSku, 2);

            Assert.Equal(0, discount);
        }


        [Fact]
        public void DiscountServiceReturnsValueMoreThanConfigured()
        {
            var discount = discountService.GetItemDiscount(AppleSku, 6);

            Assert.Equal(0.2m, discount);
        }
    }
}
