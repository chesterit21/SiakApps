using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class StudentScholarshipRepository : BaseRepository
    {
        public StudentScholarshipRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<StudentScholarship>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<StudentScholarship>(
                "SELECT siswa_id, beasiswa_id, tahun_ajaran_id, semester_id, tanggal_mulai, tanggal_selesai, jumlah_bantuan, status, created_at, updated_at FROM siswa_beasiswa");
        }

        public async Task<StudentScholarship?> GetByIdAsync(int studentId, int scholarshipId, int academicYearId, int semesterId)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<StudentScholarship>(
                "SELECT siswa_id, beasiswa_id, tahun_ajaran_id, semester_id, tanggal_mulai, tanggal_selesai, jumlah_bantuan, status, created_at, updated_at FROM siswa_beasiswa WHERE siswa_id = @StudentId AND beasiswa_id = @ScholarshipId AND tahun_ajaran_id = @AcademicYearId AND semester_id = @SemesterId",
                new { StudentId = studentId, ScholarshipId = scholarshipId, AcademicYearId = academicYearId, SemesterId = semesterId });
        }

        public async Task<int> CreateAsync(StudentScholarship studentScholarship)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO siswa_beasiswa (siswa_id, beasiswa_id, tahun_ajaran_id, semester_id, tanggal_mulai, tanggal_selesai, jumlah_bantuan, status, created_at, updated_at)
                        VALUES (@SiswaId, @BeasiswaId, @TahunAjaranId, @SemesterId, @TanggalMulai, @TanggalSelesai, @JumlahBantuan, @Status, @CreatedAt, @UpdatedAt)";

            studentScholarship.CreatedAt = DateTime.UtcNow;
            studentScholarship.UpdatedAt = DateTime.UtcNow;

            return await connection.ExecuteAsync(sql, studentScholarship);
        }

        public async Task<bool> UpdateAsync(StudentScholarship studentScholarship)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE siswa_beasiswa 
                        SET tanggal_mulai = @TanggalMulai, tanggal_selesai = @TanggalSelesai, jumlah_bantuan = @JumlahBantuan, status = @Status, updated_at = @UpdatedAt
                        WHERE siswa_id = @SiswaId AND beasiswa_id = @BeasiswaId AND tahun_ajaran_id = @TahunAjaranId AND semester_id = @SemesterId";

            studentScholarship.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, studentScholarship);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int studentId, int scholarshipId, int academicYearId, int semesterId)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM siswa_beasiswa WHERE siswa_id = @StudentId AND beasiswa_id = @ScholarshipId AND tahun_ajaran_id = @AcademicYearId AND semester_id = @SemesterId";

            var affectedRows = await connection.ExecuteAsync(sql, new { StudentId = studentId, ScholarshipId = scholarshipId, AcademicYearId = academicYearId, SemesterId = semesterId });
            return affectedRows > 0;
        }
    }
}