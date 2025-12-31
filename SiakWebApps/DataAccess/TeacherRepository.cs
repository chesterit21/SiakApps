using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class TeacherRepository : BaseRepository
    {
        public TeacherRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachersAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Teacher>(
                "SELECT id, user_id, nip, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, alamat_id, phone, email, foto_profile, created_at, updated_at, deleted_at FROM guru WHERE deleted_at IS NULL");
        }

        public async Task<Teacher?> GetTeacherByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Teacher>(
                "SELECT id, user_id, nip, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, alamat_id, phone, email, foto_profile, created_at, updated_at, deleted_at FROM guru WHERE id = @Id AND deleted_at IS NULL",
                new { Id = id });
        }

        public async Task<int> CreateTeacherAsync(Teacher teacher)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO guru (user_id, nip, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, alamat_id, phone, email, foto_profile, created_at, updated_at)
                        VALUES (@UserId, @Nip, @NamaLengkap, @JenisKelaminId, @TempatLahir, @TanggalLahir, @AlamatId, @Phone, @Email, @FotoProfile, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            teacher.CreatedAt = DateTime.UtcNow;
            teacher.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, teacher);
        }

        public async Task<bool> UpdateTeacherAsync(Teacher teacher)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE guru 
                        SET user_id = @UserId, nip = @Nip, nama_lengkap = @NamaLengkap, jenis_kelamin_id = @JenisKelaminId, 
                            tempat_lahir = @TempatLahir, tanggal_lahir = @TanggalLahir, alamat_id = @AlamatId, 
                            phone = @Phone, email = @Email, foto_profile = @FotoProfile, updated_at = @UpdatedAt
                        WHERE id = @Id";

            teacher.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, teacher);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE guru 
                        SET deleted_at = @DeletedAt
                        WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id, DeletedAt = DateTime.UtcNow });
            return affectedRows > 0;
        }
    }
}