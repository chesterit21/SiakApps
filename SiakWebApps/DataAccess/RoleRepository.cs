using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class RoleRepository : BaseRepository
    {
        public RoleRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Role>(
                "SELECT id, name, description, created_at FROM roles");
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Role>(
                "SELECT id, name, description, created_at FROM roles WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(Role role)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO roles (name, description, created_at)
                        VALUES (@Name, @Description, @CreatedAt)
                        RETURNING id";

            role.CreatedAt = DateTime.UtcNow;

            return await connection.QuerySingleAsync<int>(sql, role);
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE roles 
                        SET name = @Name, description = @Description
                        WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, role);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM roles WHERE id = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}