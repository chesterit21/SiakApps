using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class SchoolYearService
    {
        private readonly SchoolYearRepository _schoolYearRepository;

        public SchoolYearService(SchoolYearRepository schoolYearRepository)
        {
            _schoolYearRepository = schoolYearRepository;
        }

        public async Task<IEnumerable<SchoolYear>> GetAllAsync()
        {
            return await _schoolYearRepository.GetAllAsync();
        }

        public async Task<SchoolYear?> GetByIdAsync(int id)
        {
            return await _schoolYearRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(SchoolYear schoolYear)
        {
            return await _schoolYearRepository.CreateAsync(schoolYear);
        }

        public async Task<bool> UpdateAsync(SchoolYear schoolYear)
        {
            return await _schoolYearRepository.UpdateAsync(schoolYear);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _schoolYearRepository.DeleteAsync(id);
        }
    }
}