using Dapper;
using SiakWebApps.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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
            var sql = @"
                SELECT 
                    siswa_id AS SiswaId, 
                    orang_tua_id AS OrangTuaId, 
                    hubungan AS Hubungan, 
                    is_active AS IsActive,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM student_parent";
            return await connection.QueryAsync<StudentParent>(sql);
        }

        public async Task<StudentParent> GetByIdAsync(int siswaId, int orangTuaId)
        {
            using var connection = CreateConnection();
            var sql = @"
                SELECT 
                    siswa_id AS SiswaId, 
                    orang_tua_id AS OrangTuaId, 
                    hubungan AS Hubungan, 
                    is_active AS IsActive,
                    created_at AS CreatedAt,
                    updated_at AS UpdatedAt
                FROM student_parent 
                WHERE siswa_id = @SiswaId AND orang_tua_id = @OrangTuaId";
            return await connection.QueryFirstOrDefaultAsync<StudentParent>(sql, new { SiswaId = siswaId, OrangTuaId = orangTuaId });
        }

        public async Task<bool> CreateAsync(StudentParent studentParent)
        {
            using var connection = CreateConnection();
            studentParent.CreatedAt = studentParent.UpdatedAt = DateTime.UtcNow;
            var sql = @"
                INSERT INTO student_parent (siswa_id, orang_tua_id, hubungan, is_active, created_at, updated_at)
                VALUES (@SiswaId, @OrangTuaId, @Hubungan, @IsActive, @CreatedAt, @UpdatedAt)";
            var affectedRows = await connection.ExecuteAsync(sql, studentParent);
            return affectedRows > 0;
        }

        public async Task<bool> UpdateAsync(StudentParent studentParent)
        {
            using var connection = CreateConnection();
            studentParent.UpdatedAt = DateTime.UtcNow;
            var sql = @"
                UPDATE student_parent
                SET 
                    hubungan = @Hubungan,
                    is_active = @IsActive,
                    updated_at = @UpdatedAt
                WHERE siswa_id = @SiswaId AND orang_tua_id = @OrangTuaId";
            var affectedRows = await connection.ExecuteAsync(sql, studentParent);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int siswaId, int orangTuaId)
        {
            using var connection = CreateConnection();
            var sql = @"
                DELETE FROM student_parent 
                WHERE siswa_id = @SiswaId AND orang_tua_id = @OrangTuaId";
            var affectedRows = await connection.ExecuteAsync(sql, new { SiswaId = siswaId, OrangTuaId = orangTuaId });
            return affectedRows > 0;
        }
    }
}
