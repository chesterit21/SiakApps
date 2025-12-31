using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class ParentRepository : BaseRepository
    {
        public ParentRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Parent>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Parent>(
                "SELECT id, user_id, nik, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, agama, phone, email, pekerjaan, penghasilan_per_bulan, alamat_id, created_at, updated_at, deleted_at FROM orang_tua");
        }

        public async Task<Parent?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Parent>(
                "SELECT id, user_id, nik, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, agama, phone, email, pekerjaan, penghasilan_per_bulan, alamat_id, created_at, updated_at, deleted_at FROM orang_tua WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(Parent parent)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO orang_tua (user_id, nik, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, agama, phone, email, pekerjaan, penghasilan_per_bulan, alamat_id, created_at, updated_at, deleted_at)
                        VALUES (@UserId, @Nik, @NamaLengkap, @JenisKelaminId, @TempatLahir, @TanggalLahir, @Agama, @Phone, @Email, @Pekerjaan, @PenghasilanPerBulan, @AlamatId, @CreatedAt, @UpdatedAt, @DeletedAt)
                        RETURNING id";

            parent.CreatedAt = DateTime.UtcNow;
            parent.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, parent);
        }

        public async Task<bool> UpdateAsync(Parent parent)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE orang_tua 
                        SET user_id = @UserId, nik = @Nik, nama_lengkap = @NamaLengkap, jenis_kelamin_id = @JenisKelaminId, tempat_lahir = @TempatLahir, 
                            tanggal_lahir = @TanggalLahir, agama = @Agama, phone = @Phone, email = @Email, pekerjaan = @Pekerjaan, 
                            penghasilan_per_bulan = @PenghasilanPerBulan, alamat_id = @AlamatId, updated_at = @UpdatedAt, deleted_at = @DeletedAt
                        WHERE id = @Id";

            parent.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, parent);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM orang_tua WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}