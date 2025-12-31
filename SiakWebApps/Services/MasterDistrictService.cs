using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class MasterDistrictService
    {
        private readonly MasterDistrictRepository _repository;

        public MasterDistrictService(MasterDistrictRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MasterDistrict>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MasterDistrict?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(MasterDistrict district)
        {
            return await _repository.CreateAsync(district);
        }

        public async Task<bool> UpdateAsync(MasterDistrict district)
        {
            return await _repository.UpdateAsync(district);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}