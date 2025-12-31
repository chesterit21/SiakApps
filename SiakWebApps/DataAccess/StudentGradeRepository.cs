using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class StudentGradeRepository : BaseRepository
    {
        public StudentGradeRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<StudentGrade>> GetAllStudentGradesAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<StudentGrade>(
                "SELECT id, siswa_id, ujian_id, nilai, catatan, created_at, updated_at FROM nilai_siswa");
        }

        public async Task<StudentGrade?> GetStudentGradeByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<StudentGrade>(
                "SELECT id, siswa_id, ujian_id, nilai, catatan, created_at, updated_at FROM nilai_siswa WHERE id = @Id",
                new { Id = id });
        }

        public async Task<IEnumerable<StudentGrade>> GetStudentGradesByStudentIdAsync(int studentId)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<StudentGrade>(
                "SELECT id, siswa_id, ujian_id, nilai, catatan, created_at, updated_at FROM nilai_siswa WHERE siswa_id = @SiswaId",
                new { SiswaId = studentId });
        }

        public async Task<IEnumerable<StudentGrade>> GetStudentGradesByExamIdAsync(int examId)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<StudentGrade>(
                "SELECT id, siswa_id, ujian_id, nilai, catatan, created_at, updated_at FROM nilai_siswa WHERE ujian_id = @UjianId",
                new { UjianId = examId });
        }

        public async Task<int> CreateStudentGradeAsync(StudentGrade grade)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO nilai_siswa (siswa_id, ujian_id, nilai, catatan, created_at, updated_at)
                        VALUES (@SiswaId, @UjianId, @Nilai, @Catatan, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            grade.CreatedAt = DateTime.UtcNow;
            grade.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, grade);
        }

        public async Task<bool> UpdateStudentGradeAsync(StudentGrade grade)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE nilai_siswa 
                        SET siswa_id = @SiswaId, ujian_id = @UjianId, nilai = @Nilai, catatan = @Catatan, updated_at = @UpdatedAt
                        WHERE id = @Id";

            grade.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, grade);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteStudentGradeAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM nilai_siswa WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}