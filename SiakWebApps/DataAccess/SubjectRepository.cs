using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class SubjectRepository : BaseRepository
    {
        public SubjectRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Subject>(
                "SELECT id, nama, kode, tingkat_min, tingkat_max, deskripsi, created_at, updated_at FROM mata_pelajaran");
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Subject>(
                "SELECT id, nama, kode, tingkat_min, tingkat_max, deskripsi, created_at, updated_at FROM mata_pelajaran WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(Subject subject)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO mata_pelajaran (nama, kode, tingkat_min, tingkat_max, deskripsi, created_at, updated_at)
                        VALUES (@Nama, @Kode, @TingkatMin, @TingkatMax, @Deskripsi, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            subject.CreatedAt = DateTime.UtcNow;
            subject.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, subject);
        }

        public async Task<bool> UpdateAsync(Subject subject)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE mata_pelajaran 
                        SET nama = @Nama, kode = @Kode, tingkat_min = @TingkatMin, tingkat_max = @TingkatMax, deskripsi = @Deskripsi, updated_at = @UpdatedAt
                        WHERE id = @Id";

            subject.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, subject);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM mata_pelajaran WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}