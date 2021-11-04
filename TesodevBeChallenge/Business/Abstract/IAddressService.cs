using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAddressService
    {
        IResult AddAddress(Address address);
        IDataResult<Address> GetAddressById(Guid addressId);
        IResult UpdateAddress(Address address);
    }
}
