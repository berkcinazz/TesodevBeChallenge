using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationResolvers.FluentValidation
{
    public class AdressAddValidator:AbstractValidator<Address>
    {
        public AdressAddValidator()
        {
            RuleFor(i => i.CityCode).GreaterThan(1).WithMessage(Messages.CityCodeInvalid);
        }
    }
}
