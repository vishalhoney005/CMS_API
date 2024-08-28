using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.Exceptions;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.DAL.Repositories
{
    public class InvoiceDAL
    {
        private readonly CmsDbContext _context;

        public InvoiceDAL(CmsDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderItem>> GetOrderInvoice(int? invoiceNo)
        {
            string sqlQuery = "GetInvoice  @Invoice_No";
            var noParam = invoiceNo.HasValue ? new SqlParameter("@Invoice_No", invoiceNo) : new SqlParameter("@Invoice_No", DBNull.Value);
            return await _context.OrderItems.FromSqlRaw(sqlQuery, noParam).ToListAsync();

        }


        public async Task<List<OrderInvoice>> GetFinalInvoice(int? invoiceNo, string? customerName)
        {
            string sqlQuery = "GetFinalInvoice  @Invoice_No, @Customer_Name";
            var noParam = invoiceNo.HasValue ? new SqlParameter("@Invoice_No", invoiceNo) : new SqlParameter("@Invoice_No", DBNull.Value);
            var NameParam = string.IsNullOrEmpty(customerName) ? new SqlParameter("@Customer_Name", DBNull.Value) : new SqlParameter("@Customer_Name", customerName);
            return await _context.OrderInvoices.FromSqlRaw(sqlQuery, noParam, NameParam).ToListAsync();

        }

        public async Task<List<Payment>> GetPaymentMethods()
        {
            return await _context.Payments.FromSqlRaw($" Select * from Payments").ToListAsync();

        }

        public async Task<bool> InsertNewOrder(OrderInvoiceDto order, List<OrderItemDto> orderItems)
        {

            var outputParameter = new SqlParameter("@GeneratedInvoiceNo", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };


            await _context.Database.ExecuteSqlRawAsync(
                $"EXEC USP_OrderInvoice_InsertUpdate @GeneratedInvoiceNo OUTPUT, '{null}', {order.PhoneNo}, {order.PayType}",
                outputParameter
                );

            //Retrieve the generated Invoice_no from the output parameter
            var generatedInvoiceNo = (int)outputParameter.Value;

            foreach (var item in orderItems)
            {
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC USP_OrderItem_InsertUpdate " +
                    "@Invoice_No=@gin, @Item_Name=@item_name, @Qty=@qty",
                    new SqlParameter("@gin", generatedInvoiceNo),
                    new SqlParameter("@item_name", item.ItemName),
                    new SqlParameter("@qty", item.Qty)
                );


                //await _context.Database.ExecuteSqlRawAsync($"EXEC USP_OrderItem_InsertUpdate {generatedInvoiceNo},{item.ItemName},{item.Qty}");
            }

            await _context.Database.ExecuteSqlRawAsync($"EXEC USP_OrderInvoice_CalculateAmount {generatedInvoiceNo}");

            return true;




        }
        public async Task<bool> UpdateOrder(OrderInvoiceDto order, List<OrderItemDto> orderItems)
        {


            await _context.Database.ExecuteSqlRawAsync(
                    $"EXEC USP_OrderInvoice_InsertUpdate {order.InvoiceNo}, '{order.Date}', {order.PhoneNo}, {order.PayType}"
                    );

            foreach (var item in orderItems)
            {
                await _context.Database.ExecuteSqlRawAsync(
                   "EXEC USP_OrderItem_InsertUpdate " +
                   "@Invoice_No=@gin, @Item_Name=@item_name, @Qty=@qty",
                   new SqlParameter("@gin", order.InvoiceNo),
                   new SqlParameter("@item_name", item.ItemName),
                   new SqlParameter("@qty", item.Qty));

            }

            await _context.Database.ExecuteSqlRawAsync($"EXEC USP_OrderInvoice_CalculateAmount {order.InvoiceNo}");

            return true;



        }
        public async Task<bool> DeleteInvoice(int no)
        {
            var item = await GetOrderInvoice(no);

            if (item == null)
            {

                return false;
            }

            var res = await _context.Database.ExecuteSqlRawAsync($"EXEC USP_Invoice_Delete {no}");
            if (res != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            //await _context.Database.ExecuteSqlRawAsync(
            //      "EXEC USP_Invoice_Delete " +
            //      "@Invoice_No=@gin, @Item_No=@item_no",
            //      new SqlParameter("@gin", no),
            //      new SqlParameter("@item_no",id));

            return true;

        }
    }
}
