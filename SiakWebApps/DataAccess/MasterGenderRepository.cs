using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class MasterGenderRepository : BaseRepository
    {
        public MasterGenderRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<MasterGender>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<MasterGender>(
                "SELECT id, nama, created_at, updated_at FROM master_jenis_kelamin");
        }

        public async Task<MasterGender?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterGender>(
                "SELECT id, nama, created_at, updated_at FROM master_jenis_kelamin WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(MasterGender gender)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO master_jenis_kelamin (nama, created_at, updated_at)
                        VALUES (@Nama, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            gender.CreatedAt = DateTime.UtcNow;
            gender.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, gender);
        }

        public async Task<bool> UpdateAsync(MasterGender gender)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE master_jenis_kelamin 
                        SET nama = @Nama, updated_at = @UpdatedAt
                        WHERE id = @Id";

            gender.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, gender);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM master_jenis_kelamin WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}