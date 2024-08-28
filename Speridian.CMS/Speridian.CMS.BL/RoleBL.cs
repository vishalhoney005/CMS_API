using AutoMapper;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speridian.CMS.BL
{
    public class RoleBL
    {
        private readonly RoleDAL _roleDAL;
        private readonly IMapper _mapper;

        public RoleBL(RoleDAL roleDAL, IMapper mapper)
        {
            _roleDAL = roleDAL;
            _mapper = mapper;
        }
        public async Task<List<RoleDto>> GetRolesById(int? id)
        {
            var res = await _roleDAL.GetRoleById(id);
            var role = _mapper.Map<List<RoleDto>>(res);
            return role;
        }
    }
}
