using Business.Abstract;
using Business.ValidationResolvers.FluentValidation;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using FluentAssertions;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Customers.Tests.CustomerAddTest
{
    public class OrderTests
    {
        IOrderService _orderService;
        OrderAddValidator addValidator;
        public OrderTests(IOrderService orderService)
        {
            _orderService = orderService;
            addValidator = new OrderAddValidator();
        }

        [Fact]
        public void InvalidPrice_ShouldBe_Invalid()
        {
            var invalidOrder = new OrderForAddDto()
            {
                CustomerId = Guid.Parse("5857772d-8648-4fc0-9ae0-08d99eff0941"),
                Price = 0,
                ProductId = Guid.Parse("f574c632-5799-4a82-9fc8-201893d8ffff"),
                Quantity = 32,
                Status = "Not Ready"
            };
            var result = addValidator.Validate(invalidOrder);
            Assert.True(result.Errors.Any(i => i.PropertyName == "Price"));
        }
        [Fact]
        public void InvalidQuantity_ShouldBe_Invalid()
        {
            var invalidOrder = new OrderForAddDto()
            {
                CustomerId = Guid.Parse("5857772d-8648-4fc0-9ae0-08d99eff0941"),
                Price = 53,
                ProductId = Guid.Parse("f574c632-5799-4a82-9fc8-201893d8ffff"),
                Quantity = 0,
                Status = "Not Ready"
            };
            var result = addValidator.Validate(invalidOrder);
            Assert.True(result.Errors.Any(i => i.PropertyName == "Quantity"));
        }
    }
}
