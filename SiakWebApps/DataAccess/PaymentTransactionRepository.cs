using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class PaymentTransactionRepository : BaseRepository
    {
        public PaymentTransactionRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<PaymentTransaction>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<PaymentTransaction>(
                "SELECT id, siswa_id, tahun_ajaran_id, semester_id, jenis_pembayaran, jumlah, tanggal_pembayaran, bukti_pembayaran, status, created_by, created_at, updated_at FROM transaksi_pembayaran");
        }

        public async Task<PaymentTransaction?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<PaymentTransaction>(
                "SELECT id, siswa_id, tahun_ajaran_id, semester_id, jenis_pembayaran, jumlah, tanggal_pembayaran, bukti_pembayaran, status, created_by, created_at, updated_at FROM transaksi_pembayaran WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(PaymentTransaction transaction)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO transaksi_pembayaran (siswa_id, tahun_ajaran_id, semester_id, jenis_pembayaran, jumlah, tanggal_pembayaran, bukti_pembayaran, status, created_by, created_at, updated_at)
                        VALUES (@SiswaId, @TahunAjaranId, @SemesterId, @JenisPembayaran, @Jumlah, @TanggalPembayaran, @BuktiPembayaran, @Status, @CreatedBy, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            transaction.CreatedAt = DateTime.UtcNow;
            transaction.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, transaction);
        }

        public async Task<bool> UpdateAsync(PaymentTransaction transaction)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE transaksi_pembayaran 
                        SET siswa_id = @SiswaId, tahun_ajaran_id = @TahunAjaranId, semester_id = @SemesterId, jenis_pembayaran = @JenisPembayaran, 
                            jumlah = @Jumlah, tanggal_pembayaran = @TanggalPembayaran, bukti_pembayaran = @BuktiPembayaran, status = @Status, 
                            created_by = @CreatedBy, updated_at = @UpdatedAt
                        WHERE id = @Id";

            transaction.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, transaction);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM transaksi_pembayaran WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}