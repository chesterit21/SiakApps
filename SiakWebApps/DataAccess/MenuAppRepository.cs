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

        public async Task<IEnumerable<MenuPermissionDto>> GetByRoleIdAsync(int roleId)
        {
            using var connection = CreateConnection();
            var sql = @"
                SELECT m.id, m.menuname AS MenuName, m.unique_code AS UniqueCode, m.menu_url AS MenuUrl, 
                       m.parent_menu_id AS ParentMenuId, m.is_parent AS IsParent, m.level_parent AS LevelParent,
                       rm.isview AS IsView, rm.isadd AS IsAdd, rm.isedit AS IsEdit, rm.isdelete AS IsDelete,rm.isupload As IsUpload
                       ,rm.isapprove AS IsApprove, rm.isdownload AS IsDownload, rm.isprint AS IsPrint
                FROM menuapp m
                JOIN role_menu rm ON m.id = rm.menu_id
                WHERE rm.role_id = @RoleId
                ORDER BY m.level_parent, m.id";
            return await connection.QueryAsync<MenuPermissionDto>(sql, new { RoleId = roleId });
        }
    }
}