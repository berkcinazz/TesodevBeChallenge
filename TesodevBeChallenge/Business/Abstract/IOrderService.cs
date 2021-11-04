using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IResult AddOrder(OrderForAddDto order);
        IResult UpdateOrder(OrderForUpdateDto order);
        IResult DeleteOrder(Guid orderId);
        IDataResult<Order> GetOrderById(Guid orderId);
        IDataResult<List<Order>> GetAllOrders();
        IDataResult<List<Order>> GetOrdersByCustomerId(Guid customerId);
        IResult ChangeStatus(Guid orderId,string status);
    }
}
