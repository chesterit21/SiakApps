using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class MasterDistrictRepository : BaseRepository
    {
        public MasterDistrictRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<MasterDistrict>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<MasterDistrict>(
                @"SELECT d.id, d.kota_id as ""KotaId"", d.nama, c.nama as ""CityName"", p.nama as ""ProvinceName"", d.created_at as ""CreatedAt"", d.updated_at as ""UpdatedAt""
                  FROM master_kecamatan d
                  JOIN master_kota c ON d.kota_id = c.id
                  JOIN master_provinsi p ON c.provinsi_id = p.id");
        }

        public async Task<MasterDistrict?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterDistrict>(
                "SELECT id, kota_id, nama, created_at, updated_at FROM master_kecamatan WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(MasterDistrict district)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO master_kecamatan (kota_id, nama, created_at, updated_at)
                        VALUES (@KotaId, @Nama, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            district.CreatedAt = DateTime.UtcNow;
            district.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, district);
        }

        public async Task<bool> UpdateAsync(MasterDistrict district)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE master_kecamatan 
                        SET kota_id = @KotaId, nama = @Nama, updated_at = @UpdatedAt
                        WHERE id = @Id";

            district.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, district);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM master_kecamatan WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<MasterDistrict>> GetByCityIdAsync(int cityId)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<MasterDistrict>(
                "SELECT id, kota_id, nama FROM master_kecamatan WHERE kota_id = @CityId",
                new { CityId = cityId });
        }
    }
}