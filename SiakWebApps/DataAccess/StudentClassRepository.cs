using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class StudentClassRepository : BaseRepository
    {
        public StudentClassRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<StudentClass>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<StudentClass>(
                "SELECT siswa_id, kelas_id, semester_id, tanggal_masuk, tanggal_keluar, is_active, created_at, updated_at FROM siswa_kelas");
        }

        public async Task<StudentClass?> GetByIdAsync(int studentId, int classId, int semesterId)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<StudentClass>(
                "SELECT siswa_id, kelas_id, semester_id, tanggal_masuk, tanggal_keluar, is_active, created_at, updated_at FROM siswa_kelas WHERE siswa_id = @StudentId AND kelas_id = @ClassId AND semester_id = @SemesterId",
                new { StudentId = studentId, ClassId = classId, SemesterId = semesterId });
        }

        public async Task<int> CreateAsync(StudentClass studentClass)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO siswa_kelas (siswa_id, kelas_id, semester_id, tanggal_masuk, tanggal_keluar, is_active, created_at, updated_at)
                        VALUES (@SiswaId, @KelasId, @SemesterId, @TanggalMasuk, @TanggalKeluar, @IsActive, @CreatedAt, @UpdatedAt)";

            studentClass.CreatedAt = DateTime.UtcNow;
            studentClass.UpdatedAt = DateTime.UtcNow;

            return await connection.ExecuteAsync(sql, studentClass);
        }

        public async Task<bool> UpdateAsync(StudentClass studentClass)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE siswa_kelas 
                        SET tanggal_masuk = @TanggalMasuk, tanggal_keluar = @TanggalKeluar, is_active = @IsActive, updated_at = @UpdatedAt
                        WHERE siswa_id = @SiswaId AND kelas_id = @KelasId AND semester_id = @SemesterId";

            studentClass.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, studentClass);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int studentId, int classId, int semesterId)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM siswa_kelas WHERE siswa_id = @StudentId AND kelas_id = @ClassId AND semester_id = @SemesterId";

            var affectedRows = await connection.ExecuteAsync(sql, new { StudentId = studentId, ClassId = classId, SemesterId = semesterId });
            return affectedRows > 0;
        }
    }
}