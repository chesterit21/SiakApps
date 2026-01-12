using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class MasterCityService
    {
        private readonly MasterCityRepository _repository;

        public MasterCityService(MasterCityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MasterCity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MasterCity?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(MasterCity city)
        {
            return await _repository.CreateAsync(city);
        }

        public async Task<bool> UpdateAsync(MasterCity city)
        {
            return await _repository.UpdateAsync(city);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MasterCity>> GetByProvinceIdAsync(int provinceId)
        {
            return await _repository.GetByProvinceIdAsync(provinceId);
        }
    }
}