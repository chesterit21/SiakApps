using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class ClassService
    {
        private readonly ClassRepository _classRepository;

        public ClassService(ClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _classRepository.GetAllClassesAsync();
        }

        public async Task<Class?> GetClassByIdAsync(int id)
        {
            return await _classRepository.GetClassByIdAsync(id);
        }

        public async Task<int> CreateClassAsync(Class cls)
        {
            return await _classRepository.CreateClassAsync(cls);
        }

        public async Task<bool> UpdateClassAsync(Class cls)
        {
            return await _classRepository.UpdateClassAsync(cls);
        }
    }
}