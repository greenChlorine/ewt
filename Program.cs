using System;
using System.Windows.Forms;

namespace ewt360
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //先展示登录窗口，在登录窗口实现自动登录
            Application.Run(new loginfrm());
            if (UserInfo.token == null) { return; }
            Application.Run(new main());
        }
    }
}
