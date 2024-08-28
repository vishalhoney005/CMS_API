using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speridian.CMS.BL;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.PL.Models;

namespace Speridian.CMS.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBL _customerBL;

        public CustomerController(CustomerBL customerBL)
        {
            _customerBL = customerBL;
        }

        // GET: api/Customer
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("View-Customer")]
        public async Task<IActionResult> GetItems(int? Id, string? Name, string? Phno)
        {

            return Ok(await _customerBL.GetCustomersAsync(Id, Name, Phno));
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("Add-Customer")]
        public async Task<IActionResult> AddCustomer(CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                throw new BadHttpRequestException("Invalid Model");
            }
            if (await _customerBL.AddCustomer(customerDto))
            {
                return Ok(new { data="Added Successfully" });
            }
            else
            {
                throw new BadHttpRequestException("Opertion Error");
            }
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("Update-Customer")]
        public async Task<IActionResult> UpdateCustomer(CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                throw new BadHttpRequestException("Opertion Error");
            }
            if (await _customerBL.UpdateCustomer(customerDto))
            {
                return Ok(new { message = "Updated Successfully" });
            }
            else
            {   
                throw new BadHttpRequestException("Not Found");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Customer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id == 0)
            {
                throw new BadHttpRequestException("Opertion Error");
            }
            if (await _customerBL.DeleteCustomer(id))
            {
                return Ok(new { message = "Deleted Successfully" });
            }
            else
            {
                throw new BadHttpRequestException("Not Found");

            }
        }
    }
}
