using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAllCustomers();
        IResult AddCustomer(CustomerForAddDto customer);
        IResult UpdateCustomer(CustomerForUpdateDto customer);
        IResult DeleteCustomer(Guid customerId);
        IDataResult<Customer> GetCustomerById(Guid customerId);
        IResult ApproveCustomer(Guid customerId);
    }
}
