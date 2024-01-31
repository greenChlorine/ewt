using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ewt360
{
    internal class request
    {
        //2024年1月28日对此更改，turn HttpWebRequest to HttpWebclient

        public static string get(string url, bool iftoken)
        {
            //MessageBox.Show("Hello EWT!\nI will send a GET request.");
            Console.WriteLine("Get Request:\n\t(url:)" + url + "\n");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 15000;
            if (iftoken)
            {
                request.Headers.Add("token", UserInfo.token);
            }

            return getRespone(request);
        }

        //public async static Task<string> get(string url,bool iftoken)
        //{
        //    Console.WriteLine("Get Request:\n\t(url:)" + url + "\n");

        //    using (var client=new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/115.0.0.0 Safari/537.36 Edg/115.0.1901.200");
        //        if(iftoken)
        //            client.DefaultRequestHeaders.Add("token",UserInfo.token);

        //        var respone = await client.GetStringAsync(url);
        //        return respone;
        //    }
        //}

        public static string post(bool iftoken,string url,string data)
        {
            Console.WriteLine("Post Request:\n\t(url:){0}\n\t(data:){1}\n",url,data);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Timeout = 15000;
            if (iftoken)
            {
                req.Headers.Add("token", UserInfo.token);
            }

            byte[] bdata = Encoding.UTF8.GetBytes(data);
            req.ContentLength = bdata.Length;


            //操作超时错误
            /*
             * System.Net.WebException
 HResult=0x80131509
 Message=操作超时
 Source=System
 StackTrace:
  at System.Net.HttpWebRequest.GetRequestStream(TransportContext& context)
  at System.Net.HttpWebRequest.GetRequestStream()
  at ewt360.request.post(Boolean iftoken, String url, String data) in C:\Users\greenChlorine\source\repos\ewt360\request.cs:line 41
  at ewt360.loginfrm.login(String username, String decrypt_pwd) in C:\Users\greenChlorine\source\repos\ewt360\loginfrm.cs:line 36
  at ewt360.loginfrm.loginfrm_Load(Object sender, EventArgs e) in C:\Users\greenChlorine\source\repos\ewt360\loginfrm.cs:line 56
  at System.Windows.Forms.Form.OnLoad(EventArgs e)
  at System.Windows.Forms.Form.OnCreateControl()
  at System.Windows.Forms.Control.CreateControl(Boolean fIgnoreVisible)
  at System.Windows.Forms.Control.CreateControl()
  at System.Windows.Forms.Control.WmShowWindow(Message& m)
  at System.Windows.Forms.Control.WndProc(Message& m)
  at System.Windows.Forms.ScrollableControl.WndProc(Message& m)
  at System.Windows.Forms.ContainerControl.WndProc(Message& m)
  at System.Windows.Forms.Form.WmShowWindow(Message& m)
  at System.Windows.Forms.Form.WndProc(Message& m)
  at System.Windows.Forms.Control.ControlNativeWindow.OnMessage(Message& m)
  at System.Windows.Forms.Control.ControlNativeWindow.WndProc(Message& m)
  at System.Windows.Forms.NativeWindow.DebuggableCallback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)

            */
            using (Stream reqStream = req.GetRequestStream())//写入数据
            {
                reqStream.Write(bdata,0,bdata.Length);
                reqStream.Close();
            }

            return getRespone(req);
        }
        public static string getRespone(HttpWebRequest request)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string resp = reader.ReadToEnd();
                        Console.WriteLine("Recieved the respone:\n\t(Respone:){0}\n",resp);
                        return resp;
                    }
                }
            }
        }
    }
}
