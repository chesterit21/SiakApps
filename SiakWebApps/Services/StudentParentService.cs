using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class StudentParentService
    {
        private readonly StudentParentRepository _studentParentRepository;

        public StudentParentService(StudentParentRepository studentParentRepository)
        {
            _studentParentRepository = studentParentRepository;
        }

        public async Task<IEnumerable<StudentParent>> GetAllAsync()
        {
            return await _studentParentRepository.GetAllAsync();
        }

        public async Task<StudentParent?> GetByIdAsync(int studentId, int parentId)
        {
            return await _studentParentRepository.GetByIdAsync(studentId, parentId);
        }

        public async Task<int> CreateAsync(StudentParent studentParent)
        {
            return await _studentParentRepository.CreateAsync(studentParent);
        }

        public async Task<bool> UpdateAsync(StudentParent studentParent)
        {
            return await _studentParentRepository.UpdateAsync(studentParent);
        }

        public async Task<bool> DeleteAsync(int studentId, int parentId)
        {
            return await _studentParentRepository.DeleteAsync(studentId, parentId);
        }
    }
}