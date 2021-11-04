using Business.Abstract;
using Business.ValidationResolvers.FluentValidation;
using Core.Utilities.Results;
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
    public class CustomerTests
    {
        ICustomerService _customerService;
        CustomerAddValidator addValidator;
        public CustomerTests(ICustomerService customerService)
        {
            _customerService = customerService;
            addValidator = new CustomerAddValidator();
        }

        [Fact]
        public void ValidParameters_ShouldAdd_Customer()
        {
            var validCustomer = new CustomerForAddDto()
            {
                Address = new AddressForAddDto()
                {
                    AddressLine = "1",
                    City = "Sakarya",
                    CityCode = 54,
                    Country = "Turkey"
                },
                Email = "berk.cinaz@gmail.com",
                Name = "Berk Cinaz"
            };
            var result = _customerService.AddCustomer(validCustomer);
            Assert.True(result.Success);
        }

        [Fact]
        public void InvalidEmail_ShouldBe_Invalid()
        {
            var invalidCustomer = new CustomerForAddDto()
            {
                Address = new AddressForAddDto()
                {
                    AddressLine = "1",
                    City = "Sakarya",
                    CityCode = 54,
                    Country = "Turkey"
                },
                Email = "invalidemail",
                Name = "Berk Cinaz"
            };
            var result = addValidator.Validate(invalidCustomer);
            Assert.True(result.Errors.Any(i => i.PropertyName == "Email"));
        }
    }
}
