using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendNetease_DGJModule
{
    /// <summary>
    /// https://stackoverflow.com/questions/41690273/how-to-gather-all-cookies-using-cefsharp
    /// 获取CefSharp中的Cookie
    /// </summary>
    class CookieCollector : CefSharp.ICookieVisitor
    {
        private readonly TaskCompletionSource<List<CefSharp.Cookie>> _source = new TaskCompletionSource<List<CefSharp.Cookie>>();

        //需要的字段
        public readonly HashSet<string> REQUIRED_FIELDS = new HashSet<string> { "__csrf", "NMTID", "__remember_me", "MUSIC_U" };

        public bool Visit(CefSharp.Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            if (!REQUIRED_FIELDS.Contains(cookie.Name)) return true;

            _cookies.Add(cookie);

            if (_cookies.Count == REQUIRED_FIELDS.Count) {
                var cookieString = "os=pc; " + GetCookieHeader(_cookies);
                try
                {
                    MainConfig.Instance.Cookie = cookieString;
                    System.Windows.MessageBox.Show($"登陆成功: CookieString = {cookieString}");
                    PluginMain.MainWindow.OnLoginStatusChanged(MainConfig.Instance.LoginSession.LoginStatus);
                }
                catch (Exception exception)
                {
                    System.Windows.MessageBox.Show($"登陆失败: CookieString = {cookieString}\n 错误 = {exception.Message}");
                }
            }

            if (count == (total - 1))
            {
                _source.SetResult(_cookies);
            }
            return true;
        }

        // https://github.com/amaitland/CefSharp.MinimalExample/blob/ce6e579ad77dc92be94c0129b4a101f85e2fd75b/CefSharp.MinimalExample.WinForms/ListCookieVisitor.cs
        // CefSharp.MinimalExample.WinForms ListCookieVisitor 

        public Task<List<CefSharp.Cookie>> Task => _source.Task;

        public static string GetCookieHeader(List<CefSharp.Cookie> cookies)
        {

            StringBuilder cookieString = new StringBuilder();
            string delimiter = string.Empty;

            foreach (var cookie in cookies)
            {
                cookieString.Append(delimiter);
                cookieString.Append(cookie.Name);
                cookieString.Append('=');
                cookieString.Append(cookie.Value);
                delimiter = "; ";
            }

            return cookieString.ToString();
        }

        public List<CefSharp.Cookie> _cookies = new List<CefSharp.Cookie>();
        public void Dispose()
        {
        }
    }
}
