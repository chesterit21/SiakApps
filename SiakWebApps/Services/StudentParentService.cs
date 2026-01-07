using SiakWebApps.DataAccess;
using SiakWebApps.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<StudentParent> GetByIdAsync(int siswaId, int orangTuaId)
        {
            return await _studentParentRepository.GetByIdAsync(siswaId, orangTuaId);
        }

        public async Task<bool> CreateAsync(StudentParent studentParent)
        {
            return await _studentParentRepository.CreateAsync(studentParent);
        }

        public async Task<bool> UpdateAsync(StudentParent studentParent)
        {
            return await _studentParentRepository.UpdateAsync(studentParent);
        }

        public async Task<bool> DeleteAsync(int siswaId, int orangTuaId)
        {
            return await _studentParentRepository.DeleteAsync(siswaId, orangTuaId);
        }
    }
}
