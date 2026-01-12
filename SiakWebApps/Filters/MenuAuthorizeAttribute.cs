using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using SiakWebApps.Models;

namespace SiakWebApps.Filters
{
    public class MenuAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _uniqueCode;

        public MenuAuthorizeAttribute(string uniqueCode)
        {
            _uniqueCode = uniqueCode;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;
            var userMenuJson = session.GetString("UserMenu");

            // Jika tidak ada UserMenu di session, redirect ke Dashboard
            if (string.IsNullOrEmpty(userMenuJson))
            {
                context.Result = new RedirectToActionResult("Logout", "Auth", null);
                return;
            }

            try
            {
                var userMenus = JsonSerializer.Deserialize<List<MenuPermissionDto>>(userMenuJson);
                
                // Periksa apakah user memiliki akses ke menu dengan UniqueCode yang sesuai
                if (userMenus == null || !userMenus.Any(m => m.UniqueCode == _uniqueCode))
                {
                    context.Result = new RedirectToActionResult("Index", "Dashboard", null);
                }
            }
            catch
            {
                // Jika terjadi error saat deserialize, tolak akses
                context.Result = new RedirectToActionResult("Index", "Dashboard", null);
            }
        }
    }
}