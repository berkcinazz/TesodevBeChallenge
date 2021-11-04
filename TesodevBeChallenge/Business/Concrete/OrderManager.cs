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
    public class OrderManager:IOrderService
    {
        IOrderDal _orderDal;
        ICustomerService _customerService;
        IProductService _productService;

        public OrderManager(IOrderDal orderDal, ICustomerService customerService, IProductService productService)
        {
            _orderDal = orderDal;
            _customerService = customerService;
            _productService = productService;
        }

        [Validation(typeof(OrderAddValidator))]
        public IResult AddOrder(OrderForAddDto order)
        {
            var customer = _customerService.GetCustomerById(order.CustomerId);
            if (customer.Data == null) return new ErrorResult(Messages.CustomerNotFound);
            var product = _productService.GetProductById(order.ProductId);
            if (product.Data == null) return new ErrorResult(Messages.ProductNotFound);
            Order orderAdd = new Order()
            {
                CustomerId = order.CustomerId,
                Price = order.Price,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                Status = "Not Ready",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AddressId = customer.Data.AddressId
            };
            _orderDal.Add(orderAdd);
            return new SuccessResult(Messages.OrderAdded);
        }

        public IResult ChangeStatus(Guid orderId, string status)
        {
            var changeStatus = _orderDal.Get(x=>x.Id==orderId);
            if (changeStatus == null) return new ErrorResult(Messages.OrderNotFound);
            if (status == null) return new ErrorResult(Messages.StatusInvalid);
            changeStatus.Status = status;
            _orderDal.Update(changeStatus);
            return new SuccessResult(Messages.StatusInvalid);
        }

        public IResult DeleteOrder(Guid orderId)
        {
            var orderDelete = _orderDal.Get(x=>x.Id == orderId);
            if (orderDelete == null) return new ErrorResult(Messages.OrderNotFound);
            _orderDal.Delete(orderDelete);
            return new SuccessResult(Messages.OrderDeleted);
        }

        public IDataResult<List<Order>> GetAllOrders()
        {
            var result = _orderDal.GetAll();
            return new SuccessDataResult<List<Order>>(result);
        }

        public IDataResult<Order> GetOrderById(Guid orderId)
        {
            var result = _orderDal.Get(x=>x.Id==orderId);
            if (result == null) return new ErrorDataResult<Order>(Messages.OrderNotFound);
            return new SuccessDataResult<Order>(result);
        }

        public IDataResult<List<Order>> GetOrdersByCustomerId(Guid customerId)
        {
            var result = _orderDal.GetAll(x=>x.CustomerId==customerId);
            if (result == null) return new ErrorDataResult<List<Order>>(Messages.OrderNotFound);
            return new SuccessDataResult<List<Order>>(result);
        }
        [Validation(typeof(OrderUpdateValidator))]
        public IResult UpdateOrder(OrderForUpdateDto order)
        {
            var orderUpdate = _orderDal.Get(x => x.Id == order.Id);
            if (orderUpdate == null) return new ErrorResult(Messages.OrderNotFound);
            var customer = _customerService.GetCustomerById(orderUpdate.CustomerId);
            if (customer.Data == null) return new ErrorResult(Messages.CustomerNotFound);
            var product = _productService.GetProductById(order.ProductId);
            if (product.Data == null) return new ErrorResult(Messages.ProductNotFound);
            orderUpdate.AddressId = customer.Data.AddressId;
            orderUpdate.Price = order.Price;
            orderUpdate.Status = order.Status;
            orderUpdate.UpdatedAt = DateTime.Now;
            orderUpdate.Quantity = order.Quantity;
            orderUpdate.ProductId = order.ProductId;
            _orderDal.Update(orderUpdate);
            return new SuccessResult(Messages.OrderUpdated);
        }
    }
}
