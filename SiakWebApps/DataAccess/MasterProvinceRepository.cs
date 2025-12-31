using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class MasterProvinceRepository : BaseRepository
    {
        public MasterProvinceRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<MasterProvince>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<MasterProvince>(
                "SELECT id, nama, created_at, updated_at FROM master_provinsi");
        }

        public async Task<MasterProvince?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterProvince>(
                "SELECT id, nama, created_at, updated_at FROM master_provinsi WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(MasterProvince province)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO master_provinsi (nama, created_at, updated_at)
                        VALUES (@Nama, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            province.CreatedAt = DateTime.UtcNow;
            province.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, province);
        }

        public async Task<bool> UpdateAsync(MasterProvince province)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE master_provinsi 
                        SET nama = @Nama, updated_at = @UpdatedAt
                        WHERE id = @Id";

            province.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, province);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM master_provinsi WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}