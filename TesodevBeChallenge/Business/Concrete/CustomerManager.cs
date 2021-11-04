using Business.Abstract;
using Business.Constants;
using Business.ValidationResolvers.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        IAddressService _addressService;
        public CustomerManager(ICustomerDal customerDal, IAddressService addressService)
        {
            _customerDal = customerDal;
            _addressService = addressService;
        }
        [Validation(typeof(CustomerAddValidator))]
        public IResult AddCustomer(CustomerForAddDto customer)
        {
            Address address = new Address()
            {
                AddressLine = customer.Address.AddressLine,
                City = customer.Address.City,
                Country = customer.Address.Country,
                CityCode = customer.Address.CityCode
            };
            _addressService.AddAddress(address);
            Customer addCustomer = new Customer()
            {
                Email = customer.Email,
                Name = customer.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AddressId = address.Id,
                Approve=false
            };
            _customerDal.Add(addCustomer);
            return new SuccessResult(Messages.CustomerAdded);
        }
        public IResult ApproveCustomer(Guid customerId)
        {
            var customerApprove = _customerDal.Get(x => x.Id == customerId);
            if (customerApprove == null) return new ErrorResult(Messages.CustomerNotFound);
            customerApprove.Approve = true;
            _customerDal.Update(customerApprove);
            return new SuccessResult(Messages.CustomerApproved);
        }

        public IResult DeleteCustomer(Guid customerId)
        {
            var customerDelete = _customerDal.Get(x=>x.Id==customerId);
            if (customerDelete == null) return new ErrorResult(Messages.CustomerNotFound);
            _customerDal.Delete(customerDelete);
            return new SuccessResult(Messages.CustomerDeleted);
        }

        public IDataResult<List<Customer>> GetAllCustomers()
        {
            var result = _customerDal.GetAll();
            return new SuccessDataResult<List<Customer>>(result);
        }

        public IDataResult<Customer> GetCustomerById(Guid customerId)
        {
            var result = _customerDal.Get(x=>x.Id==customerId);
            if (result == null) return new ErrorDataResult<Customer>(Messages.CustomerNotFound);
            return new SuccessDataResult<Customer>(result);
        }
        [Validation(typeof(CustomerUpdateValidator))]
        public IResult UpdateCustomer(CustomerForUpdateDto customer)
        {
            var result = _addressService.GetAddressById(customer.Address.Id);
            if (result.Data == null) return new ErrorResult(Messages.AddressNotFound);
            _addressService.UpdateAddress(customer.Address);
            Customer customerUpdate = _customerDal.Get(x=>x.Id==customer.Id);
            if (customerUpdate == null) return new ErrorResult(Messages.CustomerNotFound);
            customerUpdate.Name = customer.Name;
            customerUpdate.UpdatedAt = DateTime.Now;
            customerUpdate.Email = customer.Email;
            customerUpdate.AddressId = customer.Address.Id;
            _customerDal.Update(customerUpdate);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
