using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class MasterCityRepository : BaseRepository
    {
        public MasterCityRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<MasterCity>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<MasterCity>(
                "SELECT id, provinsi_id, nama, created_at, updated_at FROM master_kota");
        }

        public async Task<MasterCity?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterCity>(
                "SELECT id, provinsi_id, nama, created_at, updated_at FROM master_kota WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(MasterCity city)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO master_kota (provinsi_id, nama, created_at, updated_at)
                        VALUES (@ProvinsiId, @Nama, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            city.CreatedAt = DateTime.UtcNow;
            city.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, city);
        }

        public async Task<bool> UpdateAsync(MasterCity city)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE master_kota 
                        SET provinsi_id = @ProvinsiId, nama = @Nama, updated_at = @UpdatedAt
                        WHERE id = @Id";

            city.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, city);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM master_kota WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}