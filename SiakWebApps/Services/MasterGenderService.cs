using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class MasterGenderService
    {
        private readonly MasterGenderRepository _repository;

        public MasterGenderService(MasterGenderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MasterGender>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MasterGender?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(MasterGender gender)
        {
            return await _repository.CreateAsync(gender);
        }

        public async Task<bool> UpdateAsync(MasterGender gender)
        {
            return await _repository.UpdateAsync(gender);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}