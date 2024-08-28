using AutoMapper;
using Speridian.CMS.DAL.Repositories;
using Speridian.CMS.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.BL
{
    public class CustomerBL
    {
        private readonly IMapper _mapper;
        private readonly CustomerDAL _customerDAL;

        public CustomerBL(IMapper mapper, CustomerDAL customerDAL)
        {
            _mapper = mapper;
            _customerDAL = customerDAL;
        }
        public async Task<List<CustomerDto>> GetCustomersAsync(int? id, string? name, string? phno)
        {
            var customer = await _customerDAL.GetCustomers(id, name, phno);
            var customerdto = _mapper.Map<List<CustomerDto>>(customer);
            return customerdto;
        }
        public async Task<bool> AddCustomer(CustomerDto customerDto)
        {
            return await _customerDAL.AddCustomer(customerDto);
        }
        public async Task<bool> UpdateCustomer(CustomerDto customerDto)
        {
            return await _customerDAL.UpdateCustomer(customerDto);
        }
        public async Task<bool> DeleteCustomer(int id)
        {
            return await _customerDAL.DeleteCustomer(id);
        }
    }
}
