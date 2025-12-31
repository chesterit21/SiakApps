using Microsoft.AspNetCore.Mvc;

namespace SiakWebApps.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 显示成功消息
        /// </summary>
        /// <param name="message">消息内容</param>
        protected void ShowSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        /// <summary>
        /// 显示错误消息
        /// </summary>
        /// <param name="message">消息内容</param>
        protected void ShowErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }

        /// <summary>
        /// 显示警告消息
        /// </summary>
        /// <param name="message">消息内容</param>
        protected void ShowWarningMessage(string message)
        {
            TempData["WarningMessage"] = message;
        }

        /// <summary>
        /// 显示信息消息
        /// </summary>
        /// <param name="message">消息内容</param>
        protected void ShowInfoMessage(string message)
        {
            TempData["InfoMessage"] = message;
        }
    }
}