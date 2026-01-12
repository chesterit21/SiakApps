using Dapper;
using System.Data;
using SiakWebApps.Models;

namespace SiakWebApps.DataAccess
{
    public class UserRoleRepository : BaseRepository
    {
        public UserRoleRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<UserRole>(
                "SELECT user_id, role_id FROM user_roles");
        }

        public async Task<UserRole?> GetByUserIdAndRoleIdAsync(int userId, int roleId)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<UserRole>(
                "SELECT user_id, role_id FROM user_roles WHERE user_id = @UserId AND role_id = @RoleId",
                new { UserId = userId, RoleId = roleId });
        }

        public async Task<int> CreateAsync(UserRole userRole)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO user_roles (user_id, role_id)
                        VALUES (@UserId, @RoleId)";

            return await connection.ExecuteAsync(sql, userRole);
        }

        public async Task<bool> DeleteAsync(int userId, int roleId)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM user_roles WHERE user_id = @UserId AND role_id = @RoleId";

            var affectedRows = await connection.ExecuteAsync(sql, new { UserId = userId, RoleId = roleId });
            return affectedRows > 0;
        }

        public async Task<bool> DeleteByUserIdAsync(int userId)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM user_roles WHERE user_id = @UserId";

            var affectedRows = await connection.ExecuteAsync(sql, new { UserId = userId });
            return affectedRows > 0;
        }

        public async Task<bool> DeleteByRoleIdAsync(int roleId)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM user_roles WHERE role_id = @RoleId";

            var affectedRows = await connection.ExecuteAsync(sql, new { RoleId = roleId });
            return affectedRows > 0;
        }

        public async Task<UserRole?> GetWithRoleByUserIdAsync(long userId)
        {
            using var connection = CreateConnection();
            var sql = @"
                SELECT ur.user_id, ur.role_id, r.id, r.name 
                FROM user_roles ur
                JOIN roles r ON ur.role_id = r.id
                WHERE ur.user_id = @UserId";
            
            var userRole = await connection.QueryAsync<UserRole, Role, UserRole>(
                sql, 
                (ur, r) => {
                    ur.Role = r;
                    return ur;
                },
                new { UserId = userId },
                splitOn: "id");

            return userRole.FirstOrDefault();
        }
    }
}