using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class StudentClassService
    {
        private readonly StudentClassRepository _studentClassRepository;

        public StudentClassService(StudentClassRepository studentClassRepository)
        {
            _studentClassRepository = studentClassRepository;
        }

        public async Task<IEnumerable<StudentClass>> GetAllAsync()
        {
            return await _studentClassRepository.GetAllAsync();
        }

        public async Task<StudentClass?> GetByIdAsync(int studentId, int classId, int semesterId)
        {
            return await _studentClassRepository.GetByIdAsync(studentId, classId, semesterId);
        }

        public async Task<int> CreateAsync(StudentClass studentClass)
        {
            return await _studentClassRepository.CreateAsync(studentClass);
        }

        public async Task<bool> UpdateAsync(StudentClass studentClass)
        {
            return await _studentClassRepository.UpdateAsync(studentClass);
        }

        public async Task<bool> DeleteAsync(int studentId, int classId, int semesterId)
        {
            return await _studentClassRepository.DeleteAsync(studentId, classId, semesterId);
        }
    }
}