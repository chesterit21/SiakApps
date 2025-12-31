using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class RoomRepository : BaseRepository
    {
        public RoomRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Room>(
                "SELECT id, nama, kapasitas, lokasi, tipe_ruangan, created_at, updated_at FROM ruangan");
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Room>(
                "SELECT id, nama, kapasitas, lokasi, tipe_ruangan, created_at, updated_at FROM ruangan WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(Room room)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO ruangan (nama, kapasitas, lokasi, tipe_ruangan, created_at, updated_at)
                        VALUES (@Nama, @Kapasitas, @Lokasi, @TipeRuangan, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            room.CreatedAt = DateTime.UtcNow;
            room.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, room);
        }

        public async Task<bool> UpdateAsync(Room room)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE ruangan 
                        SET nama = @Nama, kapasitas = @Kapasitas, lokasi = @Lokasi, tipe_ruangan = @TipeRuangan, updated_at = @UpdatedAt
                        WHERE id = @Id";

            room.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, room);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM ruangan WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}