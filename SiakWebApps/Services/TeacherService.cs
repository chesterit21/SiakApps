using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class TeacherService
    {
        private readonly TeacherRepository _teacherRepository;

        public TeacherService(TeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            return await _teacherRepository.GetAllTeachersAsync();
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            return await _teacherRepository.GetTeacherByIdAsync(id);
        }

        public async Task<int> CreateTeacherAsync(Teacher teacher)
        {
            return await _teacherRepository.CreateTeacherAsync(teacher);
        }

        public async Task<bool> UpdateTeacherAsync(Teacher teacher)
        {
            return await _teacherRepository.UpdateTeacherAsync(teacher);
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            return await _teacherRepository.DeleteTeacherAsync(id);
        }
    }
}