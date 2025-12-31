using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class StudentRegistrationService
    {
        private readonly StudentRegistrationRepository _studentRegistrationRepository;

        public StudentRegistrationService(StudentRegistrationRepository studentRegistrationRepository)
        {
            _studentRegistrationRepository = studentRegistrationRepository;
        }

        public async Task<IEnumerable<StudentRegistration>> GetAllAsync()
        {
            return await _studentRegistrationRepository.GetAllAsync();
        }

        public async Task<StudentRegistration?> GetByIdAsync(int id)
        {
            return await _studentRegistrationRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(StudentRegistration registration)
        {
            return await _studentRegistrationRepository.CreateAsync(registration);
        }

        public async Task<bool> UpdateAsync(StudentRegistration registration)
        {
            return await _studentRegistrationRepository.UpdateAsync(registration);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _studentRegistrationRepository.DeleteAsync(id);
        }
    }
}