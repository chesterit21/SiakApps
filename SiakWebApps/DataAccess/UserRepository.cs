using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<User>(
                "SELECT id, username, email, phone, is_active, created_at, updated_at FROM users WHERE deleted_at IS NULL");
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT id, username, email, phone, is_active, created_at, updated_at FROM users WHERE id = @Id AND deleted_at IS NULL",
                new { Id = id });
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT id, username, password, email, phone, is_active, created_at, updated_at FROM users WHERE email = @Username AND deleted_at IS NULL",
                new { Username = username });
        }

        public async Task<int> CreateUserAsync(User user)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO users (username, password, email, phone, is_active, created_at, updated_at)
                        VALUES (@Username, @Password, @Email, @Phone, @IsActive, @CreatedAt, @UpdatedAt)
                        RETURNING id";

            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, user);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE users 
                        SET username = @Username, email = @Email, phone = @Phone, is_active = @IsActive, updated_at = @UpdatedAt
                        WHERE id = @Id";

            user.UpdatedAt = DateTime.UtcNow;

            var affectedRows = await connection.ExecuteAsync(sql, user);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE users 
                        SET deleted_at = @DeletedAt
                        WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id, DeletedAt = DateTime.UtcNow });
            return affectedRows > 0;
        }
    }
}