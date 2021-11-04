using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("get-all-customers")]
        public IActionResult GetAllCustomers()
        {
            var result = _customerService.GetAllCustomers();
            return StatusCode(result.Success ? 200 : 400 ,result);
        }
        [HttpGet("get-customer-by-id")]
        public IActionResult GetCustomerById(Guid customerId)
        {
            var result = _customerService.GetCustomerById(customerId);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpPost("add-customer")]
        public IActionResult AddCustomer(CustomerForAddDto customer)
        {
            var result = _customerService.AddCustomer(customer);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpPut("update-customer")]
        public IActionResult UpdateCustomer(CustomerForUpdateDto customer)
        {
            var result = _customerService.UpdateCustomer(customer);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpDelete("delete-customer")]
        public IActionResult DeleteCustomer(Guid customerId)
        {
            var result = _customerService.DeleteCustomer(customerId);
            return StatusCode(result.Success ? 200 : 400, result);
        }
        [HttpPut("approve-customer")]
        public IActionResult ApproveCustomer(Guid customerId)
        {
            var result = _customerService.ApproveCustomer(customerId);
            return StatusCode(result.Success ? 200 :400,result);
        }
    }
}
