using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class StudentScholarshipService
    {
        private readonly StudentScholarshipRepository _studentScholarshipRepository;

        public StudentScholarshipService(StudentScholarshipRepository studentScholarshipRepository)
        {
            _studentScholarshipRepository = studentScholarshipRepository;
        }

        public async Task<IEnumerable<StudentScholarship>> GetAllAsync()
        {
            return await _studentScholarshipRepository.GetAllAsync();
        }

        public async Task<StudentScholarship?> GetByIdAsync(int studentId, int scholarshipId, int academicYearId, int semesterId)
        {
            return await _studentScholarshipRepository.GetByIdAsync(studentId, scholarshipId, academicYearId, semesterId);
        }

        public async Task<int> CreateAsync(StudentScholarship studentScholarship)
        {
            return await _studentScholarshipRepository.CreateAsync(studentScholarship);
        }

        public async Task<bool> UpdateAsync(StudentScholarship studentScholarship)
        {
            return await _studentScholarshipRepository.UpdateAsync(studentScholarship);
        }

        public async Task<bool> DeleteAsync(int studentId, int scholarshipId, int academicYearId, int semesterId)
        {
            return await _studentScholarshipRepository.DeleteAsync(studentId, scholarshipId, academicYearId, semesterId);
        }
    }
}