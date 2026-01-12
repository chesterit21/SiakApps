using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using SiakWebApps.Models;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace SiakWebApps.Filters
{
    public class MenuActionAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _permission;

        public MenuActionAuthorizeAttribute(string permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Get controller's MenuAuthorizeAttribute to determine menu UniqueCode
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null)
            {
                context.Result = new RedirectToActionResult("Index", "Dashboard", null);
                return;
            }
            
            var controllerType = controllerActionDescriptor.ControllerTypeInfo.AsType();
            var controllerAttributes = controllerType.GetCustomAttributes(true);
            var menuAuthorizeAttr = controllerAttributes
                .OfType<MenuAuthorizeAttribute>()
                .FirstOrDefault();

            if (menuAuthorizeAttr == null)
            {
                context.Result = new RedirectToActionResult("Index", "Dashboard", null);
                return;
            }

            // Access the private _uniqueCode field using reflection
            var uniqueCodeField = typeof(MenuAuthorizeAttribute).GetField("_uniqueCode", BindingFlags.NonPublic | BindingFlags.Instance);
            string uniqueCode = uniqueCodeField?.GetValue(menuAuthorizeAttr)?.ToString();

            if (string.IsNullOrEmpty(uniqueCode))
            {
                context.Result = new RedirectToActionResult("Index", "Dashboard", null);
                return;
            }

            // Retrieve UserMenu from session
            var session = context.HttpContext.Session;
            var userMenuJson = session.GetString("UserMenu");

            if (string.IsNullOrEmpty(userMenuJson))
            {
                context.Result = new RedirectToActionResult("Index", "Dashboard", null);
                return;
            }

            try
            {
                var userMenus = JsonSerializer.Deserialize<List<MenuPermissionDto>>(userMenuJson);
                var menu = userMenus?.FirstOrDefault(m => m.UniqueCode == uniqueCode);

                if (menu == null)
                {
                    context.Result = new RedirectToActionResult("Index", "Dashboard", null);
                    return;
                }

                // Check specific permission based on _permission
                bool hasPermission = false;
                switch (_permission.ToUpper())
                {
                    case "VIEW":
                        hasPermission = menu.IsView;
                        break;
                    case "ADD":
                        hasPermission = menu.IsAdd;
                        break;
                    case "EDIT":
                        hasPermission = menu.IsEdit;
                        break;
                    case "DELETE":
                        hasPermission = menu.IsDelete;
                        break;
                    default:
                        hasPermission = false;
                        break;
                }

                if (!hasPermission)
                {
                    context.Result = new RedirectToActionResult("Index", "Dashboard", null);
                }
            }
            catch
            {
                context.Result = new RedirectToActionResult("Index", "Dashboard", null);
            }
        }
    }
}