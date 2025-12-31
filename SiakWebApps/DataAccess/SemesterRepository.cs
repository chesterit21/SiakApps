using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class SemesterRepository : BaseRepository
    {
        public SemesterRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Semester>> GetAllSemestersAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Semester>(
                "SELECT id, tahun_ajaran_id, nama, semester_ke, tanggal_mulai, tanggal_selesai, is_active, created_at, updated_at FROM semester");
        }

        public async Task<IEnumerable<Semester>> GetSemestersByAcademicYearAsync(int academicYearId)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Semester>(
                "SELECT id, tahun_ajaran_id, nama, semester_ke, tanggal_mulai, tanggal_selesai, is_active, created_at, updated_at FROM semester WHERE tahun_ajaran_id = @TahunAjaranId",
                new { TahunAjaranId = academicYearId });
        }

        public async Task<Semester?> GetActiveSemesterAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Semester>(
                "SELECT id, tahun_ajaran_id, nama, semester_ke, tanggal_mulai, tanggal_selesai, is_active, created_at, updated_at FROM semester WHERE is_active = true");
        }

        public async Task<Semester?> GetSemesterByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Semester>(
                "SELECT id, tahun_ajaran_id, nama, semester_ke, tanggal_mulai, tanggal_selesai, is_active, created_at, updated_at FROM semester WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateSemesterAsync(Semester semester)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO semester (tahun_ajaran_id, nama, semester_ke, tanggal_mulai, tanggal_selesai, is_active, created_at, updated_at)
                        VALUES (@TahunAjaranId, @Nama, @SemesterKe, @TanggalMulai, @TanggalSelesai, @IsActive, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            semester.CreatedAt = DateTime.UtcNow;
            semester.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, semester);
        }

        public async Task<bool> UpdateSemesterAsync(Semester semester)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE semester 
                        SET tahun_ajaran_id = @TahunAjaranId, nama = @Nama, semester_ke = @SemesterKe, 
                            tanggal_mulai = @TanggalMulai, tanggal_selesai = @TanggalSelesai, is_active = @IsActive, updated_at = @UpdatedAt
                        WHERE id = @Id";

            semester.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, semester);
            return affectedRows > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = "DELETE FROM semester WHERE id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}