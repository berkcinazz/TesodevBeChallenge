using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationResolvers.FluentValidation
{
    public class CustomerAddValidator:AbstractValidator<CustomerForAddDto>
    {
        public CustomerAddValidator()
        {
            RuleFor(i=>i.Email).EmailAddress();
        }
    }
}
