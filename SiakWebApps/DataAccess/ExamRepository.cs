using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class ExamRepository : BaseRepository
    {
        public ExamRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Exam>> GetAllExamsAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Exam>(
                "SELECT id, jenis_ujian_id, mata_pelajaran_id, kelas_id, semester_id, tanggal_ujian, jam_mulai, jam_selesai, ruangan_id, guru_id, deskripsi, created_at, updated_at FROM ujian");
        }

        public async Task<Exam?> GetExamByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Exam>(
                "SELECT id, jenis_ujian_id, mata_pelajaran_id, kelas_id, semester_id, tanggal_ujian, jam_mulai, jam_selesai, ruangan_id, guru_id, deskripsi, created_at, updated_at FROM ujian WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateExamAsync(Exam exam)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO ujian (jenis_ujian_id, mata_pelajaran_id, kelas_id, semester_id, tanggal_ujian, jam_mulai, jam_selesai, ruangan_id, guru_id, deskripsi, created_at, updated_at)
                        VALUES (@JenisUjianId, @MataPelajaranId, @KelasId, @SemesterId, @TanggalUjian, @JamMulai, @JamSelesai, @RuanganId, @GuruId, @Deskripsi, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            exam.CreatedAt = DateTime.UtcNow;
            exam.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, exam);
        }

        public async Task<bool> UpdateExamAsync(Exam exam)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE ujian 
                        SET jenis_ujian_id = @JenisUjianId, mata_pelajaran_id = @MataPelajaranId, kelas_id = @KelasId, semester_id = @SemesterId,
                            tanggal_ujian = @TanggalUjian, jam_mulai = @JamMulai, jam_selesai = @JamSelesai, ruangan_id = @RuanganId, 
                            guru_id = @GuruId, deskripsi = @Deskripsi, updated_at = @UpdatedAt
                        WHERE id = @Id";

            exam.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, exam);
            return affectedRows > 0;
        }
    }
}