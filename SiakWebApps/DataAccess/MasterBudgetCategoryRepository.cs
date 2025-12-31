using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class MasterBudgetCategoryRepository : BaseRepository
    {
        public MasterBudgetCategoryRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<MasterBudgetCategory>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<MasterBudgetCategory>(
                "SELECT id, nama, created_at, updated_at FROM master_anggaran_kategori");
        }

        public async Task<MasterBudgetCategory?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MasterBudgetCategory>(
                "SELECT id, nama, created_at, updated_at FROM master_anggaran_kategori WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(MasterBudgetCategory category)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO master_anggaran_kategori (nama, created_at, updated_at)
                        VALUES (@Nama, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            category.CreatedAt = DateTime.UtcNow;
            category.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, category);
        }

        public async Task<bool> UpdateAsync(MasterBudgetCategory category)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE master_anggaran_kategori 
                        SET nama = @Nama, updated_at = @UpdatedAt
                        WHERE id = @Id";

            category.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, category);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM master_anggaran_kategori WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}