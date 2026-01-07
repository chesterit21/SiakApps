using SiakWebApps.DataAccess;
using SiakWebApps.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiakWebApps.Services
{
    public class RoleMenuService
    {
        private readonly RoleMenuRepository _roleMenuRepository;

        public RoleMenuService(RoleMenuRepository roleMenuRepository)
        {
            _roleMenuRepository = roleMenuRepository;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _roleMenuRepository.GetAllRolesAsync();
        }

        public async Task<IEnumerable<RoleMenu>> GetRoleMenusByRoleIdAsync(int roleId)
        {
            return await _roleMenuRepository.GetRoleMenusByRoleIdAsync(roleId);
        }

        public async Task DeleteRoleMenusByRoleIdAsync(int roleId)
        {
            await _roleMenuRepository.DeleteRoleMenusByRoleIdAsync(roleId);
        }

        public async Task CreateRoleMenuAsync(RoleMenu roleMenu)
        {
            await _roleMenuRepository.CreateRoleMenuAsync(roleMenu);
        }
    }
}