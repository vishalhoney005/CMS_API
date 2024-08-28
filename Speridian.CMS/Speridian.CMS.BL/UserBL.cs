using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class UserBL
    {
        private readonly UserDAL _userDAL;
        private readonly IMapper _mapper;

        public UserBL(UserDAL userDAL, IMapper mapper)
        {
            _userDAL = userDAL;
            _mapper = mapper;
        }
        public async Task<bool> RegisterUserAsync(UserDto userDto)
        {
            return await _userDAL.RegisterUserAsync(userDto);
        }
        public async Task<List<UserDto>> GetUsersAsync(int? id, int? roleId)
        {
            var res = await _userDAL.GetUsersAsync(id, roleId);

            var user = _mapper.Map<List<UserDto>>(res);
            return user;
        }
        public async Task<List<RoleDto>> GetAllRoles()
        {
            var role = await _userDAL.GetAllRoles();
            var roledto = _mapper.Map<List<RoleDto>>(role);
            return roledto;
        }
        public async Task<bool> UpdateUserAsync(UserDto2 userDto)
        {
            return await _userDAL.UpdateUser(userDto);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userDAL.DeleteUser(id);
        }
    }
}

