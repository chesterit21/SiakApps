using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class AcademicYearRepository : BaseRepository
    {
        public AcademicYearRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<AcademicYear>> GetAllAcademicYearsAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<AcademicYear>(
                "SELECT id, nama, tahun_mulai, tahun_selesai, is_active, created_at, updated_at FROM tahun_ajaran");
        }

        public async Task<AcademicYear?> GetActiveAcademicYearAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<AcademicYear>(
                "SELECT id, nama, tahun_mulai, tahun_selesai, is_active, created_at, updated_at FROM tahun_ajaran WHERE is_active = true");
        }

        public async Task<AcademicYear?> GetAcademicYearByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<AcademicYear>(
                "SELECT id, nama, tahun_mulai, tahun_selesai, is_active, created_at, updated_at FROM tahun_ajaran WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAcademicYearAsync(AcademicYear academicYear)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO tahun_ajaran (nama, tahun_mulai, tahun_selesai, is_active, created_at, updated_at)
                        VALUES (@Nama, @TahunMulai, @TahunSelesai, @IsActive, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            academicYear.CreatedAt = DateTime.UtcNow;
            academicYear.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, academicYear);
        }

        public async Task<bool> UpdateAcademicYearAsync(AcademicYear academicYear)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE tahun_ajaran 
                        SET nama = @Nama, tahun_mulai = @TahunMulai, tahun_selesai = @TahunSelesai, is_active = @IsActive, updated_at = @UpdatedAt
                        WHERE id = @Id";

            academicYear.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, academicYear);
            return affectedRows > 0;
        }
    }
}