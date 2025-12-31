using SiakWebApps.DataAccess;
using SiakWebApps.Models;

namespace SiakWebApps.Services
{
    public class StudentGradeService
    {
        private readonly StudentGradeRepository _studentGradeRepository;

        public StudentGradeService(StudentGradeRepository studentGradeRepository)
        {
            _studentGradeRepository = studentGradeRepository;
        }

        public async Task<IEnumerable<StudentGrade>> GetAllStudentGradesAsync()
        {
            return await _studentGradeRepository.GetAllStudentGradesAsync();
        }

        public async Task<StudentGrade?> GetStudentGradeByIdAsync(int id)
        {
            return await _studentGradeRepository.GetStudentGradeByIdAsync(id);
        }

        public async Task<IEnumerable<StudentGrade>> GetStudentGradesByStudentIdAsync(int studentId)
        {
            return await _studentGradeRepository.GetStudentGradesByStudentIdAsync(studentId);
        }

        public async Task<IEnumerable<StudentGrade>> GetStudentGradesByExamIdAsync(int examId)
        {
            return await _studentGradeRepository.GetStudentGradesByExamIdAsync(examId);
        }

        public async Task<int> CreateStudentGradeAsync(StudentGrade grade)
        {
            return await _studentGradeRepository.CreateStudentGradeAsync(grade);
        }

        public async Task<bool> UpdateStudentGradeAsync(StudentGrade grade)
        {
            return await _studentGradeRepository.UpdateStudentGradeAsync(grade);
        }

        public async Task<bool> DeleteStudentGradeAsync(int id)
        {
            return await _studentGradeRepository.DeleteStudentGradeAsync(id);
        }
    }
}