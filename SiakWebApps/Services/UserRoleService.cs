using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class UserRoleService
    {
        private readonly UserRoleRepository _userRoleRepository;

        public UserRoleService(UserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _userRoleRepository.GetAllAsync();
        }

        public async Task<UserRole?> GetByUserIdAndRoleIdAsync(int userId, int roleId)
        {
            return await _userRoleRepository.GetByUserIdAndRoleIdAsync(userId, roleId);
        }

        public async Task<int> CreateAsync(UserRole userRole)
        {
            return await _userRoleRepository.CreateAsync(userRole);
        }

        public async Task<bool> DeleteAsync(int userId, int roleId)
        {
            return await _userRoleRepository.DeleteAsync(userId, roleId);
        }

        public async Task<bool> DeleteByUserIdAsync(int userId)
        {
            return await _userRoleRepository.DeleteByUserIdAsync(userId);
        }

        public async Task<bool> DeleteByRoleIdAsync(int roleId)
        {
            return await _userRoleRepository.DeleteByRoleIdAsync(roleId);
        }

        public async Task<UserRole?> GetRoleByUserIdAsync(long userId)
        {
            return await _userRoleRepository.GetWithRoleByUserIdAsync(userId);
        }
    }
}