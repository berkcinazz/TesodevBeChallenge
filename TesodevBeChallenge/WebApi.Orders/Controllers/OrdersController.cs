using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get-all-orders")]
        public IActionResult GetAllOrders()
        {
            var result = _orderService.GetAllOrders();
            return StatusCode(result.Success ? 200 : 400 ,result);
        }
        [HttpGet("get-order-by-id")]
        public IActionResult GetOrderById(Guid orderId)
        {
            var result = _orderService.GetOrderById(orderId);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpPost("add-order")]
        public IActionResult AddOrder(OrderForAddDto order)
        {
            var result = _orderService.AddOrder(order);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpPut("update-order")]
        public IActionResult UpdateOrder(OrderForUpdateDto order)
        {
            var result = _orderService.UpdateOrder(order);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpDelete("delete-order")]
        public IActionResult DeleteOrder(Guid orderId)
        {
            var result = _orderService.DeleteOrder(orderId);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpGet("get-orders-by-customer-id")]
        public IActionResult GetOrdersByCustomerId(Guid customerId)
        {
            var result = _orderService.GetOrdersByCustomerId(customerId);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpPut("change-status")]
        public IActionResult ChangeStatus(Guid orderId,string status)
        {
            var result = _orderService.ChangeStatus(orderId, status);
            return StatusCode(result.Success ? 200 : 400 ,result);
        }
    }
}
