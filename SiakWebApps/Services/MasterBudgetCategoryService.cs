using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class MasterBudgetCategoryService
    {
        private readonly MasterBudgetCategoryRepository _repository;

        public MasterBudgetCategoryService(MasterBudgetCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MasterBudgetCategory>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MasterBudgetCategory?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(MasterBudgetCategory category)
        {
            return await _repository.CreateAsync(category);
        }

        public async Task<bool> UpdateAsync(MasterBudgetCategory category)
        {
            return await _repository.UpdateAsync(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}