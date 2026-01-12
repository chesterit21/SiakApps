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
                @"SELECT 
                    s.id, 
                    s.kecamatan_id as ""KecamatanId"", 
                    s.nama, 
                    d.nama as ""DistrictName"", 
                    c.nama as ""CityName"",
                    p.nama as ""ProvinceName"",
                    s.created_at as ""CreatedAt"", 
                    s.updated_at as ""UpdatedAt""
                  FROM master_kelurahan s
                  JOIN master_kecamatan d ON s.kecamatan_id = d.id
                  JOIN master_kota c ON d.kota_id = c.id
                  JOIN master_provinsi p ON c.provinsi_id = p.id");
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