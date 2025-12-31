using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class ParentService
    {
        private readonly ParentRepository _parentRepository;

        public ParentService(ParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<IEnumerable<Parent>> GetAllAsync()
        {
            return await _parentRepository.GetAllAsync();
        }

        public async Task<Parent?> GetByIdAsync(int id)
        {
            return await _parentRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Parent parent)
        {
            return await _parentRepository.CreateAsync(parent);
        }

        public async Task<bool> UpdateAsync(Parent parent)
        {
            return await _parentRepository.UpdateAsync(parent);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _parentRepository.DeleteAsync(id);
        }
    }
}