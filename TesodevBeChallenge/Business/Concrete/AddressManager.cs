using Business.Abstract;
using Business.Constants;
using Business.ValidationResolvers.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AddressManager : IAddressService
    {
        IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }
        [Validation(typeof(AdressAddValidator))]
        public IResult AddAddress(Address address)
        {
            _addressDal.Add(address);
            return new SuccessResult();
        }

        public IDataResult<Address> GetAddressById(Guid addressId)
        {
            var result = _addressDal.Get(x=>x.Id==addressId);
            if (result == null) return new ErrorDataResult<Address>(Messages.AddressNotFound);
            return new SuccessDataResult<Address>(result);
        }
        [Validation(typeof(AdressUpdateValidator))]
        public IResult UpdateAddress(Address address)
        {
            var addressUpdate = _addressDal.Get(x=>x.Id == address.Id);
            if (addressUpdate == null) return new ErrorDataResult<Address>(Messages.AddressNotFound);
            addressUpdate.Country = address.Country;
            addressUpdate.AddressLine = address.AddressLine;
            addressUpdate.CityCode = address.CityCode;
            addressUpdate.City = address.City;
            _addressDal.Update(addressUpdate);
            return new SuccessResult(Messages.AddressUpdated);
        }
    }
}
