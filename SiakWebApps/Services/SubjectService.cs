using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class SubjectService
    {
        private readonly SubjectRepository _subjectRepository;

        public SubjectService(SubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _subjectRepository.GetAllAsync();
        }

        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            return await _subjectRepository.GetByIdAsync(id);
        }

        public async Task<int> CreateSubjectAsync(Subject subject)
        {
            return await _subjectRepository.CreateAsync(subject);
        }

        public async Task<bool> UpdateSubjectAsync(Subject subject)
        {
            return await _subjectRepository.UpdateAsync(subject);
        }

        public async Task<bool> DeleteSubjectAsync(int id)
        {
            return await _subjectRepository.DeleteAsync(id);
        }
    }
}