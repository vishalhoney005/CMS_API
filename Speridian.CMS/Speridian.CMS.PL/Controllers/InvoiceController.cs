using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Speridian.CMS.BL;
using Speridian.CMS.Entities.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Speridian.CMS.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceBL _invoiceBL;

        public InvoiceController(InvoiceBL invoiceBL)
        {
            _invoiceBL = invoiceBL;
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("View-OrderInvoice")]
        public async Task<IActionResult> GetOrderInvoice(int? invoiceNo )
        { 
            return Ok(await _invoiceBL.GetOrderInvoice(invoiceNo));
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("View-FinalInvoice")]
        public async Task<IActionResult> GetFinalInvoice(int? invoiceNo, string? customerName)
        {
            return Ok(await _invoiceBL.GetFinalInvoice(invoiceNo,customerName));
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("View-PayType")]
        public async Task<IActionResult> GetPaymentType()
        {
            return Ok(await _invoiceBL.GetPaymentMethods());
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("Add-Order")]
        public async Task<IActionResult> AddOrder([FromBody] OrderInputDto orderInput)
        {
            if (orderInput == null || orderInput.Order == null || orderInput.OrderItems == null || orderInput.OrderItems.Count == 0)
            {
                throw new BadHttpRequestException("Invalid Model");
            }

            if (await _invoiceBL.InsertNewOrder(orderInput.Order, orderInput.OrderItems))
            {
                return Ok(new { message = "Order added successfully" });
            }
            else
                throw new BadHttpRequestException("Operation Error");
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("Update-Order")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderInputDto orderInput)
        {
            if (orderInput == null || orderInput.Order == null || orderInput.OrderItems == null || orderInput.OrderItems.Count == 0)
            {
                throw new BadHttpRequestException("Invalid Model");
            }

            var result = await _invoiceBL.UpdateOrder(orderInput.Order, orderInput.OrderItems);

            if (result)
            {
                return Ok(new { message = "Order updated successfully" });
            }

            throw new BadHttpRequestException("Order Not Found");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete-Invoice")]
        public async Task<IActionResult> DeleteInvoice(int no)
        {
            if (no == 0)
            {
                throw new BadHttpRequestException("Invalid No");
            }
            if (await _invoiceBL.DeleteInvoice(no))
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
