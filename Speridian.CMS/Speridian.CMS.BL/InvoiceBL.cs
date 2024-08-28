using AutoMapper;
using Speridian.CMS.DAL.Repositories;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.BL
{
    public class InvoiceBL
    {
        private readonly IMapper _mapper;
        private readonly InvoiceDAL _invoiceDAL;

        public InvoiceBL(IMapper mapper, InvoiceDAL invoiceDAL)
        {
            _mapper = mapper;
            _invoiceDAL = invoiceDAL;
        }
        public async Task<List<OrderItemDto>> GetOrderInvoice(int? invoiceNo)
        {
            var invoice = await _invoiceDAL.GetOrderInvoice(invoiceNo);
            var invoicedto = _mapper.Map<List<OrderItemDto>>(invoice);
            return invoicedto;
        }


        public async Task<List<OrderInvoiceDto>> GetFinalInvoice(int? invoiceNo, string? customerName)
        {
            var invoice = await _invoiceDAL.GetFinalInvoice(invoiceNo,customerName);
            var invoicedto = _mapper.Map<List<OrderInvoiceDto>>(invoice);
            return invoicedto;
        }

        public async Task<List<PaymentDTO>> GetPaymentMethods()
        {
            var pay = await _invoiceDAL.GetPaymentMethods();
            var paydto = _mapper.Map<List<PaymentDTO>>(pay);
            return paydto;
        }

        public async Task<bool> InsertNewOrder(OrderInvoiceDto order, List<OrderItemDto> orderItems)
        {
            return await _invoiceDAL.InsertNewOrder(order, orderItems);
        }
        public async Task<bool> UpdateOrder(OrderInvoiceDto order, List<OrderItemDto> orderItems)
        {
            return await _invoiceDAL.UpdateOrder(order, orderItems);
        }

        public async Task<bool> DeleteInvoice(int no)
        {
            return await _invoiceDAL.DeleteInvoice(no);
        }
    }
}
