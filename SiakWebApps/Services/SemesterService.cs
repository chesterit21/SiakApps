using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class SemesterService
    {
        private readonly SemesterRepository _semesterRepository;

        public SemesterService(SemesterRepository semesterRepository)
        {
            _semesterRepository = semesterRepository;
        }

        public async Task<IEnumerable<Semester>> GetAllSemestersAsync()
        {
            return await _semesterRepository.GetAllSemestersAsync();
        }

        public async Task<IEnumerable<Semester>> GetSemestersByAcademicYearAsync(int academicYearId)
        {
            return await _semesterRepository.GetSemestersByAcademicYearAsync(academicYearId);
        }

        public async Task<Semester?> GetActiveSemesterAsync()
        {
            return await _semesterRepository.GetActiveSemesterAsync();
        }

        public async Task<Semester?> GetSemesterByIdAsync(int id)
        {
            return await _semesterRepository.GetSemesterByIdAsync(id);
        }

        public async Task<int> CreateSemesterAsync(Semester semester)
        {
            return await _semesterRepository.CreateSemesterAsync(semester);
        }

        public async Task<bool> UpdateSemesterAsync(Semester semester)
        {
            return await _semesterRepository.UpdateSemesterAsync(semester);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _semesterRepository.DeleteAsync(id);
        }
    }
}