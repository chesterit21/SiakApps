using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class ClassRepository : BaseRepository
    {
        public ClassRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Class>(
                "SELECT id, nama, tingkat, tahun_ajaran_id, wali_kelas_id, kapasitas, created_at, updated_at FROM kelas");
        }

        public async Task<Class?> GetClassByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Class>(
                "SELECT id, nama, tingkat, tahun_ajaran_id, wali_kelas_id, kapasitas, created_at, updated_at FROM kelas WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateClassAsync(Class cls)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO kelas (nama, tingkat, tahun_ajaran_id, wali_kelas_id, kapasitas, created_at, updated_at)
                        VALUES (@Nama, @Tingkat, @TahunAjaranId, @WaliKelasId, @Kapasitas, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            cls.CreatedAt = DateTime.UtcNow;
            cls.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, cls);
        }

        public async Task<bool> UpdateClassAsync(Class cls)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE kelas 
                        SET nama = @Nama, tingkat = @Tingkat, tahun_ajaran_id = @TahunAjaranId, wali_kelas_id = @WaliKelasId, kapasitas = @Kapasitas, updated_at = @UpdatedAt
                        WHERE id = @Id";

            cls.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, cls);
            return affectedRows > 0;
        }
    }
}