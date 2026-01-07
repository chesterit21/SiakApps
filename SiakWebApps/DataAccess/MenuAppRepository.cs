using Dapper;
using SiakWebApps.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiakWebApps.DataAccess
{
    public class MenuAppRepository : BaseRepository
    {
        public MenuAppRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<MenuApp>> GetAllAsync()
        {
            using var connection = CreateConnection();
            // Menggunakan nama kolom snake_case sesuai konvensi PostgreSQL di query
            return await connection.QueryAsync<MenuApp>(
                "SELECT id, menuname, unique_code AS UniqueCode, menu_url AS MenuUrl, parent_menu_id AS ParentMenuId, is_parent AS IsParent, level_parent AS LevelParent FROM menuapp");
        }

        public async Task<MenuApp?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MenuApp>(
                "SELECT id, menuname, unique_code AS UniqueCode, menu_url AS MenuUrl, parent_menu_id AS ParentMenuId, is_parent AS IsParent, level_parent AS LevelParent FROM menuapp WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> CreateAsync(MenuApp menuApp)
        {
            using var connection = CreateConnection();
            var sql = @"INSERT INTO menuapp (menuname, unique_code, menu_url, parent_menu_id, is_parent, level_parent)
                        VALUES (@MenuName, @UniqueCode, @MenuUrl, @ParentMenuId, @IsParent, @LevelParent)
                        RETURNING id";
            return await connection.QuerySingleAsync<int>(sql, menuApp);
        }

        public async Task<bool> UpdateAsync(MenuApp menuApp)
        {
            using var connection = CreateConnection();
            var sql = @"UPDATE menuapp
                        SET menuname = @MenuName,
                            unique_code = @UniqueCode,
                            menu_url = @MenuUrl,
                            parent_menu_id = @ParentMenuId,
                            is_parent = @IsParent,
                            level_parent = @LevelParent
                        WHERE id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, menuApp);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM menuapp WHERE id = @Id";
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
        
        public async Task<IEnumerable<MenuApp>> GetParentMenusAsync()
        {
            using var connection = CreateConnection();
            // Mengambil semua menu yang bisa menjadi parent (termasuk yang tidak memiliki parent)
            return await connection.QueryAsync<MenuApp>(
                "SELECT id, menuname AS MenuName, unique_code AS UniqueCode, menu_url AS MenuUrl, parent_menu_id AS ParentMenuId, is_parent AS IsParent, level_parent AS LevelParent FROM menuapp ORDER BY level_parent, menuname");
        }
    }
}