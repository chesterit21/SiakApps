using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class StudentRegistrationRepository : BaseRepository
    {
        public StudentRegistrationRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<StudentRegistration>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<StudentRegistration>(
                "SELECT id, siswa_id, tahun_ajaran_id, semester_id, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, alamat, phone, email, nama_ayah, phone_ayah, nama_ibu, phone_ibu, asal_sekolah, nilai_raport, status, tanggal_daftar, created_at, updated_at FROM pendaftaran_siswa");
        }

        public async Task<StudentRegistration?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<StudentRegistration>(
                "SELECT id, siswa_id, tahun_ajaran_id, semester_id, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, alamat, phone, email, nama_ayah, phone_ayah, nama_ibu, phone_ibu, asal_sekolah, nilai_raport, status, tanggal_daftar, created_at, updated_at FROM pendaftaran_siswa WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(StudentRegistration registration)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO pendaftaran_siswa (siswa_id, tahun_ajaran_id, semester_id, nama_lengkap, jenis_kelamin_id, tempat_lahir, tanggal_lahir, alamat, phone, email, nama_ayah, phone_ayah, nama_ibu, phone_ibu, asal_sekolah, nilai_raport, status, tanggal_daftar, created_at, updated_at)
                        VALUES (@SiswaId, @TahunAjaranId, @SemesterId, @NamaLengkap, @JenisKelaminId, @TempatLahir, @TanggalLahir, @Alamat, @Phone, @Email, @NamaAyah, @PhoneAyah, @NamaIbu, @PhoneIbu, @AsalSekolah, @NilaiRaport, @Status, @TanggalDaftar, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            registration.CreatedAt = DateTime.UtcNow;
            registration.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, registration);
        }

        public async Task<bool> UpdateAsync(StudentRegistration registration)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE pendaftaran_siswa 
                        SET siswa_id = @SiswaId, tahun_ajaran_id = @TahunAjaranId, semester_id = @SemesterId, nama_lengkap = @NamaLengkap, 
                            jenis_kelamin_id = @JenisKelaminId, tempat_lahir = @TempatLahir, tanggal_lahir = @TanggalLahir, alamat = @Alamat, 
                            phone = @Phone, email = @Email, nama_ayah = @NamaAyah, phone_ayah = @PhoneAyah, nama_ibu = @NamaIbu, 
                            phone_ibu = @PhoneIbu, asal_sekolah = @AsalSekolah, nilai_raport = @NilaiRaport, status = @Status, 
                            tanggal_daftar = @TanggalDaftar, updated_at = @UpdatedAt
                        WHERE id = @Id";

            registration.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, registration);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM pendaftaran_siswa WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}