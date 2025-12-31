using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class ExpenseService
    {
        private readonly ExpenseRepository _expenseRepository;

        public ExpenseService(ExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _expenseRepository.GetAllAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _expenseRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(Expense expense)
        {
            return await _expenseRepository.CreateAsync(expense);
        }

        public async Task<bool> UpdateAsync(Expense expense)
        {
            return await _expenseRepository.UpdateAsync(expense);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _expenseRepository.DeleteAsync(id);
        }
    }
}