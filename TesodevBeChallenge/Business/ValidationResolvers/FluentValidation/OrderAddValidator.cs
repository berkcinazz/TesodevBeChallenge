using Business.Constants;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationResolvers.FluentValidation
{
    public class OrderAddValidator:AbstractValidator<OrderForAddDto>
    {
        public OrderAddValidator()
        {
            RuleFor(i=>i.Quantity).GreaterThan(1).WithMessage(Messages.QuantityInvalid);
            RuleFor(i => i.Price).GreaterThan(1).WithMessage(Messages.PriceInvalid);
        }
    }
}
