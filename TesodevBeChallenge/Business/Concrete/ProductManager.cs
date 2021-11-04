using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<Product> GetProductById(Guid productId)
        {
            var result = _productDal.Get(x=>x.Id == productId);
            if (result == null) return new ErrorDataResult<Product>(Messages.ProductNotFound);
            return new SuccessDataResult<Product>(result);
        }
    }
}
