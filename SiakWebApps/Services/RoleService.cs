using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Role role)
        {
            return await _roleRepository.CreateAsync(role);
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            return await _roleRepository.UpdateAsync(role);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _roleRepository.DeleteAsync(id);
        }
    }
}