using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class StudentParentRepository : BaseRepository
    {
        public StudentParentRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<StudentParent>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<StudentParent>(
                "SELECT siswa_id, orang_tua_id, hubungan, is_active, created_at, updated_at FROM siswa_orang_tua");
        }

        public async Task<StudentParent?> GetByIdAsync(int studentId, int parentId)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<StudentParent>(
                "SELECT siswa_id, orang_tua_id, hubungan, is_active, created_at, updated_at FROM siswa_orang_tua WHERE siswa_id = @StudentId AND orang_tua_id = @ParentId",
                new { StudentId = studentId, ParentId = parentId });
        }

        public async Task<int> CreateAsync(StudentParent studentParent)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO siswa_orang_tua (siswa_id, orang_tua_id, hubungan, is_active, created_at, updated_at)
                        VALUES (@SiswaId, @OrangTuaId, @Hubungan, @IsActive, @CreatedAt, @UpdatedAt)";

            studentParent.CreatedAt = DateTime.UtcNow;
            studentParent.UpdatedAt = DateTime.UtcNow;

            return await connection.ExecuteAsync(sql, studentParent);
        }

        public async Task<bool> UpdateAsync(StudentParent studentParent)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE siswa_orang_tua 
                        SET hubungan = @Hubungan, is_active = @IsActive, updated_at = @UpdatedAt
                        WHERE siswa_id = @SiswaId AND orang_tua_id = @OrangTuaId";

            studentParent.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, studentParent);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int studentId, int parentId)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM siswa_orang_tua WHERE siswa_id = @StudentId AND orang_tua_id = @ParentId";

            var affectedRows = await connection.ExecuteAsync(sql, new { StudentId = studentId, ParentId = parentId });
            return affectedRows > 0;
        }
    }
}