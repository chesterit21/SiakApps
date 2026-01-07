using Dapper;
using SiakWebApps.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace SiakWebApps.DataAccess
{
    public class RoleMenuRepository : BaseRepository
    {
        public RoleMenuRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            using var connection = CreateConnection();
            var sql = "SELECT id, name, description, created_at FROM roles";
            return await connection.QueryAsync<Role>(sql);
        }

        public async Task<IEnumerable<RoleMenu>> GetRoleMenusByRoleIdAsync(int roleId)
        {
            using var connection = CreateConnection();
            var sql = @"
                SELECT 
                    id AS Id, 
                    role_id AS RoleId, 
                    menu_id AS MenuId, 
                    isview AS IsView, 
                    isadd AS IsAdd, 
                    isedit AS IsEdit, 
                    isdelete AS IsDelete, 
                    isapprove AS IsApprove, 
                    isdownload AS IsDownload, 
                    isprint AS IsPrint,
                    isupload AS IsUpload
                FROM role_menu
                WHERE role_id = @RoleId
            ";
            return await connection.QueryAsync<RoleMenu>(sql, new { RoleId = roleId });
        }

        public async Task DeleteRoleMenusByRoleIdAsync(int roleId)
        {
            using var connection = CreateConnection();
            var sql = @"
                DELETE FROM role_menu
                WHERE role_id = @RoleId
            ";
            await connection.ExecuteAsync(sql, new { RoleId = roleId });
        }

        public async Task CreateRoleMenuAsync(RoleMenu roleMenu)
        {
            using var connection = CreateConnection();
            var sql = @"
                INSERT INTO role_menu (role_id, menu_id, isview, isadd, isedit, isdelete, isapprove, isdownload, isprint, isupload)
                VALUES (@RoleId, @MenuId, @IsView, @IsAdd, @IsEdit, @IsDelete, @IsApprove, @IsDownload, @IsPrint, @IsUpload)
            ";
            await connection.ExecuteAsync(sql, roleMenu);
        }


    }
}
