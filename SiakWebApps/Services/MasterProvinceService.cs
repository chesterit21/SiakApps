using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class MasterProvinceService
    {
        private readonly MasterProvinceRepository _repository;

        public MasterProvinceService(MasterProvinceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MasterProvince>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MasterProvince?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(MasterProvince province)
        {
            return await _repository.CreateAsync(province);
        }

        public async Task<bool> UpdateAsync(MasterProvince province)
        {
            return await _repository.UpdateAsync(province);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}