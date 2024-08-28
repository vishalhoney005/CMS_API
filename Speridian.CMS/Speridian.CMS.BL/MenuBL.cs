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
    public class MenuBL
    {
        private readonly MenuDAL _menuDAL;
        private readonly IMapper _mapper;

        public MenuBL(MenuDAL menuDAL, IMapper mapper)
        {
            _menuDAL = menuDAL;
            _mapper = mapper;
        }
        public async Task<List<MenuDto>> GetItemsAsync(int? id, string? name, int? rate)
        {
            var menu = await _menuDAL.GetItems(id, name, rate);
            var menudto = _mapper.Map<List<MenuDto>>(menu);
            return menudto;
        }
        public async Task<bool> InsertItem(MenuDto menuDto)
        {
            return await _menuDAL.InsertItem(menuDto);
        }
        public async Task<bool> UpdateItem(MenuDto menuDto)
        {
            return await _menuDAL.UpdateItem(menuDto);
        }
        public async Task<bool> DeleteItem(int id)
        {
            return await _menuDAL.DeleteItem(id);
        }

    }
}
