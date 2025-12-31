using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class ExamService
    {
        private readonly ExamRepository _examRepository;

        public ExamService(ExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<IEnumerable<Exam>> GetAllExamsAsync()
        {
            return await _examRepository.GetAllExamsAsync();
        }

        public async Task<Exam?> GetExamByIdAsync(int id)
        {
            return await _examRepository.GetExamByIdAsync(id);
        }

        public async Task<int> CreateExamAsync(Exam exam)
        {
            return await _examRepository.CreateExamAsync(exam);
        }

        public async Task<bool> UpdateExamAsync(Exam exam)
        {
            return await _examRepository.UpdateExamAsync(exam);
        }
    }
}