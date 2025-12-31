using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class MasterSubdistrictService
    {
        private readonly MasterSubdistrictRepository _repository;

        public MasterSubdistrictService(MasterSubdistrictRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MasterSubdistrict>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MasterSubdistrict?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(MasterSubdistrict subdistrict)
        {
            return await _repository.CreateAsync(subdistrict);
        }

        public async Task<bool> UpdateAsync(MasterSubdistrict subdistrict)
        {
            return await _repository.UpdateAsync(subdistrict);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}