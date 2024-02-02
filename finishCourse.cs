using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ewt360
{
    public partial class finishCourse : Form
    {
        //courseId和lessonId用于发送bfe请求定位课程
        public string courseId;//parentContentId--->courseId
        public string lessonId;//contentId--->lessonId
        public int length;//视频长度，单位s

        private int postTimes;//用于确定看多长时间
        private string sessionId;//请求头X-Bfe-Session-Id用
        private string secret;//signature的密钥
        private string clientIp;
        private int index = 1;//uuid要添加请求index
        private string beginTS;//开始请求的时间戳
        private string uuid;

        public finishCourse(string a, string b, int c)
        {
            InitializeComponent();

            courseId = a;
            lessonId = b;
            length = c;

            postTimes = (int)((length * 0.8) / 60 / 2) + 1;//相当于有多少分
            //超过80%就算完成
            //显示视频时间
            label1.Text = (int)(length / 60) + ":" + length % 60;
        }
        private string getSignature(string action, string duration, string ts)
        {
            string data = $"action={action}&duration={duration}&mstid={UserInfo.token}&signatureMethod=HMAC-SHA1&signatureVersion=1.0&timestamp={ts}&version=2022-08-02";
            return Encrypt.HMACSHA1_Encrypt(data, secret);
        }
        private string getTimeStamp()
        {
            return ((long)(DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0)).TotalMilliseconds).ToString();
        }

        private string getUUID()
        {
            //8为uuid，随机字符串加一个_
            const string chars = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZABCDEFGHIGKLMNOPQRSTUVWXYZABCDEFGHIGKLMNOPQRSTUVWXYZABCDEFGHIGKLMNOPQRSTUVWXYZABCDEFGHIGKLMNOPQRSTUVWXYZ";
            char[] uuid = new char[8];

            for (int i = 0; i < 8; i++)
            {
                uuid[i] = chars[new Random().Next(390)];
            }

            return uuid + "_";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //因为需要发送开始信号，这个index为1，所以需要减1

            postBFE("2", "60000", "1", "60000");
            if ((index - 2) == postTimes)
            {
                timer1.Enabled = false;
                Console.WriteLine("The Course have finished!");
                this.Close();
            }
            //这个if要放在postBFE后面，因为BFE对index进行了操作，等到发送完成
            //index++完了之后再判断
        }

        private void postBFE(string action, string duration, string status, string stay_time)
        {
            string currentTS = getTimeStamp();
            //uuid是8位随机数字大小写
            //string data = $"{{\"CommonPackage\":{{\"userid\":138249150,\"ip\":\"{clientIp}\",\"os\":\"Windows\",\"resolution\":\"1536*864\",\"mstid\":\"{UserInfo.token}\",\"browser\":\"Edge\",\"browser_ver\":\"5.0(WindowsNT10.0;Win64;x64)AppleWebKit/537.36(KHTML,likeGecko)Chrome/115.0.0.0Safari/537.36Edg/115.0.1901.200\",\"playerType\":1,\"sdkVersion\":\"3.0.8\",\"videoBizCode\":\"1013\"}},\"EventPackage\":[{{\"lesson_id\":\"{lessonId}\",\"course_id\":\"{courseId}\",\"stay_time\":{stay_time},\"status\":{status},\"begin_time\":\"{beginTS}\",\"report_time\":{currentTS},\"point_time_id\":4,\"point_time\":60000,\"point_num\":16,\"video_type\":1,\"speed\":2,\"quality\":\"高清\",\"action\":{action},\"fallback\":0,\"uuid\":\"{uuid + index}\"}}],\"signature\":\"{getSignature(action,duration,currentTS)}\",\"sn\":\"ewt_web_video_detail\",\"_\":{currentTS}}}";
            string data = $"{{\"CommonPackage\":{{\"userid\":{UserInfo.userid},\"ip\":\"{clientIp}\",\"os\":\"Windows\",\"resolution\":\"1536*864\",\"mstid\":\"{UserInfo.token}\",\"browser\":\"Edge\",\"browser_ver\":\"5.0(WindowsNT10.0;Win64;x64)AppleWebKit/537.36(KHTML,likeGecko)Chrome/115.0.0.0Safari/537.36Edg/115.0.1901.200\",\"playerType\":1,\"sdkVersion\":\"3.0.14\",\"videoBizCode\":\"1013\"}},\"EventPackage\":[{{\"lesson_id\":\"{lessonId}\",\"course_id\":\"{courseId}\",\"stay_time\":{stay_time},\"status\":{status},\"begin_time\":\"{beginTS}\",\"report_time\":{currentTS},\"point_time_id\":4,\"point_time\":60000,\"point_num\":16,\"video_type\":1,\"speed\":2,\"quality\":\"高清\",\"action\":{action},\"fallback\":0,\"uuid\":\"{uuid + index}\"}}],\"signature\":\"{getSignature(action, duration, currentTS)}\",\"sn\":\"ewt_web_video_detail\",\"_\":{currentTS}}}";
            //删除了_:
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ewt.bach + $"?TrVideoBizCode=1013&TrFallback=0&TrUserId={UserInfo.userid}&TrLessonId={lessonId}&TrUuId={uuid + index}&sdkVersion=3.0.8");
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers.Add("X-Bfe-Session-Id", sessionId);
            req.Headers.Add("token", UserInfo.token);

            byte[] bdata = Encoding.UTF8.GetBytes(data);
            req.ContentLength = bdata.Length;

            using (Stream reqStream = req.GetRequestStream())//写入数据
            {
                reqStream.Write(bdata, 0, bdata.Length);
                reqStream.Close();
            }

            index++;
            listBox1.Items.Add($"{DateTime.Now}   {index - 2} of {postTimes}   {request.getRespone(req)}");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //获取secret,sesion,clientIP
            json rep = new json(request.get(ewt.getPlayerGlobalConf, true));
            sessionId = rep.getNameValue("sessionId");
            secret = rep.getNameValue("secret");
            clientIp = rep.getNameValue("clientIp");


            uuid = getUUID();
            beginTS = getTimeStamp();

            postBFE("1", "0", "1", "0");
            timer1.Enabled = true;
        }

        private void finishCourse_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.jump = true;
        }
    }
}
