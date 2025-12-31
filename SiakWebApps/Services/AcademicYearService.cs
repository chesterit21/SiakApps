using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class AcademicYearService
    {
        private readonly AcademicYearRepository _academicYearRepository;

        public AcademicYearService(AcademicYearRepository academicYearRepository)
        {
            _academicYearRepository = academicYearRepository;
        }

        public async Task<IEnumerable<AcademicYear>> GetAllAcademicYearsAsync()
        {
            return await _academicYearRepository.GetAllAcademicYearsAsync();
        }

        public async Task<AcademicYear?> GetActiveAcademicYearAsync()
        {
            return await _academicYearRepository.GetActiveAcademicYearAsync();
        }

        public async Task<AcademicYear?> GetAcademicYearByIdAsync(int id)
        {
            return await _academicYearRepository.GetAcademicYearByIdAsync(id);
        }

        public async Task<int> CreateAcademicYearAsync(AcademicYear academicYear)
        {
            return await _academicYearRepository.CreateAcademicYearAsync(academicYear);
        }

        public async Task<bool> UpdateAcademicYearAsync(AcademicYear academicYear)
        {
            return await _academicYearRepository.UpdateAcademicYearAsync(academicYear);
        }
    }
}