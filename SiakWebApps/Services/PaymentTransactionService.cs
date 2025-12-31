using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class PaymentTransactionService
    {
        private readonly PaymentTransactionRepository _paymentTransactionRepository;

        public PaymentTransactionService(PaymentTransactionRepository paymentTransactionRepository)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
        }

        public async Task<IEnumerable<PaymentTransaction>> GetAllAsync()
        {
            return await _paymentTransactionRepository.GetAllAsync();
        }

        public async Task<PaymentTransaction?> GetByIdAsync(int id)
        {
            return await _paymentTransactionRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(PaymentTransaction transaction)
        {
            return await _paymentTransactionRepository.CreateAsync(transaction);
        }

        public async Task<bool> UpdateAsync(PaymentTransaction transaction)
        {
            return await _paymentTransactionRepository.UpdateAsync(transaction);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _paymentTransactionRepository.DeleteAsync(id);
        }
    }
}