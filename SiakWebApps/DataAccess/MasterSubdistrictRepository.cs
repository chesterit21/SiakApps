using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class MasterSubdistrictRepository : BaseRepository
    {
        public MasterSubdistrictRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<MasterSubdistrict>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<MasterSubdistrict>(
                "SELECT id, kecamatan_id, nama, created_at, updated_at FROM master_kelurahan");
        }

        public async Task<MasterSubdistrict?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterSubdistrict>(
                "SELECT id, kecamatan_id, nama, created_at, updated_at FROM master_kelurahan WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(MasterSubdistrict subdistrict)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO master_kelurahan (kecamatan_id, nama, created_at, updated_at)
                        VALUES (@KecamatanId, @Nama, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            subdistrict.CreatedAt = DateTime.UtcNow;
            subdistrict.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, subdistrict);
        }

        public async Task<bool> UpdateAsync(MasterSubdistrict subdistrict)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE master_kelurahan 
                        SET kecamatan_id = @KecamatanId, nama = @Nama, updated_at = @UpdatedAt
                        WHERE id = @Id";

            subdistrict.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, subdistrict);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM master_kelurahan WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}