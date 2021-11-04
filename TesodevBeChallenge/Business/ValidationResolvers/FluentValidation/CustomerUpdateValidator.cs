using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationResolvers.FluentValidation
{
    public class CustomerUpdateValidator : AbstractValidator<CustomerForAddDto>
    {
        public CustomerUpdateValidator()
        {
            RuleFor(i => i.Email).EmailAddress();
        }
    }
}
