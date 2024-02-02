using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;

namespace ewt360
{
    public partial class Quick_Complete : Form
    {
        private string course;
        private string lesson;
        private int length;


        public Quick_Complete(string _course_id, string _lesson_id, string length)
        {
            InitializeComponent();

            course = _course_id;
            lesson = _lesson_id;
            this.length = int.Parse(length);//单位是s
        }

        private void Quick_Complete_Load(object sender, EventArgs e)
        {
            //log={log}&key=eo^nye1j#!wt2%v)
            //计算需要写几部分，60s
            int parts = (int)(length / 60);
            int others = length % 60;


            //不能确定的参数oper_type,media_time,status
            string oper_type;
            string begin_time = getTimeStamp(DateTime.Today);
            string dev_id = uuid2();
            string course_id = course;
            string trace_id = uuid2();
            string homework_id = UserInfo.homeworkId;
            string lesson_id = lesson;
            string log_index = "72";
            //string uuid;
            string media_time;
            string resolution = "2158*1080";
            string status;
            string stay_time;
            string timestamp;
            string userid = UserInfo.userid;
            string log_id = uuid2();
            string single_part = $"{{\"oper_type\":\"{{0}}\",\"version\":\"10.3.6\",\"begin_time\":\"{begin_time}\",\"brand\":\"vivo\",\"carrier\":\"N/A\",\"channel\":\"ewt360\",\"dev_id\":\"{dev_id}\",\"extra\":{{\"course_id\":\"{course_id}\",\"trace_id\":\"{trace_id}\",\"content_type\":\"video_record\",\"videoBizCode\":2013,\"homework_id\":\"{homework_id}\",\"lesson_id\":\"{lesson_id}\",\"log_index\":\"{log_index}\",\"uuid\":\"{{uuid}}\",\"fallback\":\"1\",\"isonline\":\"0\"}},\"imei\":\"\",\"imsi\":\"\",\"language\":\"zh\",\"media_time\":\"{{3}}\",\"device_model\":\"V2244A\",\"sdk_version\":\"2.0.95-test-rc21\",\"access\":\"NO_NETWORK\",\"online\":\"2\",\"end_time\":\"\",\"start_time\":\"\",\"phone_number\":\"\",\"os\":\"Android\",\"os_version\":\"13\",\"point_id\":\"64\",\"point_time\":\"60000\",\"resolution\":\"{resolution}\",\"speed\":\"2.0\",\"status\":\"{{1}}\",\"stay_time\":\"{{2}}\",\"ts\":{{4}},\"type\":\"video\",\"uploadStatus\":2,\"url\":\"com.mistong.ewt360.questionbank.ui.studyhistory.StudyHistoryDetailActivity\",\"userid\":\"{userid}\",\"log_id\":\"{log_id}\"}}";

            string data = "";
            string uuids = "";

            //先添加整数分钟
            for (int i = 0; i < 3; i++)
            {
                oper_type = "";
                status = "";
                stay_time = "30000";
                media_time = "60000";
                timestamp = begin_time + 60000 * i;
                string uid = uuid1(8);

                data += single_part
                    .Replace("{0}", oper_type)
                    .Replace("{1}", status)
                    .Replace("{2}", stay_time)
                    .Replace("{3}", media_time)
                    .Replace("{4}", timestamp)
                    .Replace("{uuid}", uid);
                uuids += uid;
                if (i != 3 - 1)
                {
                    data += ",";
                    uuids += ",";
                }

            }


            //发送请求
            HttpClient client = new HttpClient();

            //?TrUserId={0}&TrLessonId={1}&TrUuId={2}&TrVideoBizCode=2013&TrFallback=0";
            string url = string.Format(ewt.clog, UserInfo.userid, lesson_id, uuids);

            //添加请求头
            client.DefaultRequestHeaders.Add("sign", sign("[" + data + "]"));
            client.DefaultRequestHeaders.Add("sn", "moses_ewt_video");
            client.DefaultRequestHeaders.Add("ts", getTimeStamp(DateTime.Now));
            //client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");

            //添加请求体,application/x-www-form-urlencoded

            var formContent = new FormUrlEncodedContent(new[]
        {
                new KeyValuePair<string, string>("sn", "moses_ewt_video"),
                new KeyValuePair<string, string>("log", "["+data+"]"),
                // 添加更多的键值对...
            });

            var repone = client.PostAsync(url, formContent);
            Console.WriteLine(repone);

        }

        private string sign(string log)
        {
            const string model = "log={0}&key=eo^nye1j#!wt2%v)";
            return Encrypt.md5_32(string.Format(model, log));
        }

        private string getTimeStamp(DateTime Current_Time)
        {
            return (Current_Time - new DateTime(1970, 1, 1, 8, 0, 0)).TotalMilliseconds.ToString();
        }


        private string uuid2()
        {
            //06f95b83-8e3c-3241-be3a-aa8fd139c295
            return uuid1(8) + "-" + uuid1(4) + "-" + uuid1(4) + "-" + uuid1(4) + "-" + uuid1(3);
        }

        private string uuid1(int size)
        {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            byte[] data = new byte[size];
            using (System.Security.Cryptography.RNGCryptoServiceProvider crypto = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
            }
            System.Text.StringBuilder result = new System.Text.StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
