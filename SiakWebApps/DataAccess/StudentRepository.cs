using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class StudentRepository : BaseRepository
    {
        public StudentRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Student>(
                "SELECT id, user_id, nis, nisn, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, agama, alamat_id, phone, email, foto_profile, created_at, updated_at, deleted_at FROM siswa WHERE deleted_at IS NULL");
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Student>(
                "SELECT id, user_id, nis, nisn, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, agama, alamat_id, phone, email, foto_profile, created_at, updated_at, deleted_at FROM siswa WHERE id = @Id AND deleted_at IS NULL",
                new { Id = id });
        }

        public async Task<int> CreateStudentAsync(Student student)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO siswa (user_id, nis, nisn, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, agama, alamat_id, phone, email, foto_profile, created_at, updated_at)
                        VALUES (@UserId, @Nis, @Nisn, @NamaLengkap, @JenisKelaminId, @TempatLahir, @TanggalLahir, @Agama, @AlamatId, @Phone, @Email, @FotoProfile, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            student.CreatedAt = DateTime.UtcNow;
            student.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, student);
        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE siswa 
                        SET user_id = @UserId, nis = @Nis, nisn = @Nisn, nama_lengkap = @NamaLengkap, jenis_kelamin_id = @JenisKelaminId, 
                            tempat_lahir = @TempatLahir, tanggal_lahir = @TanggalLahir, agama = @Agama, alamat_id = @AlamatId, 
                            phone = @Phone, email = @Email, foto_profile = @FotoProfile, updated_at = @UpdatedAt
                        WHERE id = @Id";

            student.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, student);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE siswa 
                        SET deleted_at = @DeletedAt
                        WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id, DeletedAt = DateTime.UtcNow });
            return affectedRows > 0;
        }
    }
}