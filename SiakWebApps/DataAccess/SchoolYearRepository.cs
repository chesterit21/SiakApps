using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class SchoolYearRepository : BaseRepository
    {
        public SchoolYearRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<SchoolYear>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<SchoolYear>(
                "SELECT id, nama, tahun_mulai, tahun_selesai, is_active, created_at, updated_at FROM tahun_ajaran");
        }

        public async Task<SchoolYear?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<SchoolYear>(
                "SELECT id, nama, tahun_mulai, tahun_selesai, is_active, created_at, updated_at FROM tahun_ajaran WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(SchoolYear schoolYear)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO tahun_ajaran (nama, tahun_mulai, tahun_selesai, is_active, created_at, updated_at)
                        VALUES (@Nama, @TahunMulai, @TahunSelesai, @IsActive, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            schoolYear.CreatedAt = DateTime.UtcNow;
            schoolYear.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, schoolYear);
        }

        public async Task<bool> UpdateAsync(SchoolYear schoolYear)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE tahun_ajaran 
                        SET nama = @Nama, tahun_mulai = @TahunMulai, tahun_selesai = @TahunSelesai, is_active = @IsActive, updated_at = @UpdatedAt
                        WHERE id = @Id";

            schoolYear.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, schoolYear);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM tahun_ajaran WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}