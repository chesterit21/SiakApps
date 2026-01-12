using SiakWebApps.DataAccess;
using SiakWebApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiakWebApps.Services
{
    public class MenuAppService
    {
        private readonly MenuAppRepository _menuAppRepository;

        public MenuAppService(MenuAppRepository menuAppRepository)
        {
            _menuAppRepository = menuAppRepository;
        }

        public async Task<IEnumerable<MenuApp>> GetAllAsync()
        {
            return await _menuAppRepository.GetAllAsync();
        }

        public async Task<MenuApp?> GetByIdAsync(int id)
        {
            return await _menuAppRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(MenuApp menuApp)
        {
            // Validasi UniqueCode harus unik
            var existingMenu = await GetByUniqueCodeAsync(menuApp.UniqueCode);
            if (existingMenu != null)
            {
                throw new InvalidOperationException($"Menu dengan UniqueCode '{menuApp.UniqueCode}' sudah ada.");
            }

            return await _menuAppRepository.CreateAsync(menuApp);
        }

        public async Task<bool> UpdateAsync(MenuApp menuApp)
        {
            // Validasi UniqueCode harus unik (kecuali untuk menu yang sedang diupdate)
            var existingMenu = await GetByUniqueCodeAsync(menuApp.UniqueCode);
            if (existingMenu != null && existingMenu.Id != menuApp.Id)
            {
                throw new InvalidOperationException($"Menu dengan UniqueCode '{menuApp.UniqueCode}' sudah ada.");
            }

            return await _menuAppRepository.UpdateAsync(menuApp);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _menuAppRepository.DeleteAsync(id);
        }

        // Method tambahan untuk mendapatkan menu berdasarkan UniqueCode
        public async Task<MenuApp?> GetByUniqueCodeAsync(string uniqueCode)
        {
            var allMenus = await _menuAppRepository.GetAllAsync();
            return allMenus.FirstOrDefault(m => m.UniqueCode.Equals(uniqueCode, StringComparison.OrdinalIgnoreCase));
        }
        
        public async Task<IEnumerable<MenuApp>> GetParentMenusAsync()
        {
            return await _menuAppRepository.GetParentMenusAsync();
        }

        public async Task<IEnumerable<MenuPermissionDto>> GetByRoleIdAsync(int roleId)
        {
            return await _menuAppRepository.GetByRoleIdAsync(roleId);
        }
    }
}