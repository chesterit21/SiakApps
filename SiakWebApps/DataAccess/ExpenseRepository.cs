using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class ExpenseRepository : BaseRepository
    {
        public ExpenseRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Expense>(
                "SELECT id, anggaran_id, tahun_ajaran_id, semester_id, nama_pengeluaran, jumlah, deskripsi, tanggal_pengeluaran, bukti_pengeluaran, created_by, created_at, updated_at FROM pengeluaran");
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Expense>(
                "SELECT id, anggaran_id, tahun_ajaran_id, semester_id, nama_pengeluaran, jumlah, deskripsi, tanggal_pengeluaran, bukti_pengeluaran, created_by, created_at, updated_at FROM pengeluaran WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(Expense expense)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO pengeluaran (anggaran_id, tahun_ajaran_id, semester_id, nama_pengeluaran, jumlah, deskripsi, tanggal_pengeluaran, bukti_pengeluaran, created_by, created_at, updated_at)
                        VALUES (@AnggaranId, @TahunAjaranId, @SemesterId, @NamaPengeluaran, @Jumlah, @Deskripsi, @TanggalPengeluaran, @BuktiPengeluaran, @CreatedBy, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            expense.CreatedAt = DateTime.UtcNow;
            expense.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, expense);
        }

        public async Task<bool> UpdateAsync(Expense expense)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE pengeluaran 
                        SET anggaran_id = @AnggaranId, tahun_ajaran_id = @TahunAjaranId, semester_id = @SemesterId, nama_pengeluaran = @NamaPengeluaran, 
                            jumlah = @Jumlah, deskripsi = @Deskripsi, tanggal_pengeluaran = @TanggalPengeluaran, bukti_pengeluaran = @BuktiPengeluaran, 
                            created_by = @CreatedBy, updated_at = @UpdatedAt
                        WHERE id = @Id";

            expense.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, expense);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM pengeluaran WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}