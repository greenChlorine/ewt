using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ewt360
{
    public partial class Quick_Complete : Form
    {
        public ewt360.main.course_data unfinished;

        public Quick_Complete()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private async void combtn_Click(object sender, EventArgs e)
        {
            //string[] _course_id = incomplete.getNameValueArr("parentContentId");
            //string[] _lesson_id = incomplete.getNameValueArr("contentId");
            //string[] _length = incomplete.getNameValueArr("duration");
            //int amount = _course_id.Length;
            long begin_time = new DateTimeOffset(DateTime.Today).ToUnixTimeMilliseconds();
            progressBar1.Maximum = unfinished.amout;

            for(int i = 0; i < unfinished.amout; i++)
            {
                new Complete
                {
                    course_id = unfinished.course_id[i],
                    lesson_id = unfinished.content_id[i],
                    length = int.Parse(unfinished.length[i])
                }
                .CompleteCourse(begin_time);

                progressBar1.Value += 1;
                begin_time += 3600000;

                await Task.Delay(500);

            }

            //await Task.Delay(10000);

            MessageBox.Show("Complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void Quick_Complete_Load(object sender, EventArgs e)
        {

        }
    }

    class Complete
     {
        public string course_id;
        public string lesson_id;
        public int length;
        public int log_index=0;

        private string signature(string log)
        {
            const string model = "log={0}&key=eo^nye1j#!wt2%v)";
            return Encrypt.md5_32(string.Format(model, log));
        }
        private string randomStr()
        {
            //06f95b83-8e3c-3241-be3a-aa8fd139c295
            return generate_uuid(8) + "-" + generate_uuid(4) + "-" + generate_uuid(4) + "-" + generate_uuid(4) + "-" + generate_uuid(3);
        }
        private string generate_uuid(int size)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
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
        private async void send(long begin_time, int parts)
        {
            /*
         关键参数
            "oper_type":""
            "log_index":"1",
            "isonline":"0"
            "access":"NO_NETWORK",
            "online":"2",
            "media_time":"60000",
            "stay_time":"30000",
            "uploadStatus":2,
            {begin_time}{dev_id}{course_id}{trace_id}{homeworkid}
            {lesson_id}{log_index}{uuid}{ts}{userid}{log_id}
        */

            if (parts == 0) return;

            const string _part = "{\"oper_type\":\"\",\"version\":\"10.3.6\",\"begin_time\":\"{begin_time}\",\"brand\":\"vivo\",\"carrier\":\"N/A\",\"channel\":\"ewt360\",\"dev_id\":\"{dev_id}\",\"extra\":{\"course_id\":\"{course_id}\",\"trace_id\":\"{trace_id}\",\"content_type\":\"video_record\",\"videoBizCode\":2013,\"homework_id\":\"{homework_id}\",\"lesson_id\":\"{lesson_id}\",\"log_index\":\"{log_index}\",\"uuid\":\"{uuid}\",\"fallback\":\"0\",\"isonline\":\"0\"},\"imei\":\"\",\"imsi\":\"\",\"language\":\"zh\",\"media_time\":\"60000\",\"device_model\":\"V2244A\",\"sdk_version\":\"2.0.95-test-rc21\",\"access\":\"NO_NETWORK\",\"online\":\"2\",\"end_time\":\"\",\"start_time\":\"\",\"phone_number\":\"\",\"os\":\"Android\",\"os_version\":\"13\",\"point_id\":\"60\",\"point_time\":\"60000\",\"resolution\":\"2187*1080\",\"speed\":\"1.0\",\"status\":\"2\",\"stay_time\":\"60000\",\"ts\":{ts},\"type\":\"video\",\"uploadStatus\":2,\"url\":\"com.mistong.ewt360.core.media.video.MediaPlayFragment\",\"userid\":\"{userid}\",\"log_id\":\"{log_id}\"}";
            string log_part = _part
                .Replace("{begin_time}", begin_time.ToString())
                .Replace("{course_id}", course_id)
                .Replace("{homework_id}", UserInfo.homeworkId)
                .Replace("{lesson_id}", lesson_id)
                .Replace("{userid}", UserInfo.userid);

            string data = "";
            string uuidlist = "";

            //生成log
            for (int i = 0; i < parts; i++)
            {
                string uuid = generate_uuid(8);
                uuidlist += uuid + ",";
                log_index++;
                data += log_part.Replace("{dev_id}", randomStr())
                    .Replace("{trace_id}", randomStr())
                    .Replace("{log_index}", (log_index + i).ToString())
                    .Replace("{uuid}", uuid)
                    .Replace("{ts}", (begin_time + 60000 * (log_index + i + 1)).ToString())
                    .Replace("{log_id}", randomStr()) + ",";

            }

            //删除最后的逗号
            uuidlist = uuidlist.TrimEnd(',');
            data = data.TrimEnd(',');

            data = "[" + data + "]";


            //发送请求
            using (HttpClient client = new HttpClient())
            {

                string url = string.Format(ewt.clog, UserInfo.userid, lesson_id, uuidlist);

                //添加请求头
                client.DefaultRequestHeaders.Add("sign", signature(data));
                client.DefaultRequestHeaders.Add("sn", "moses_ewt_video");
                client.DefaultRequestHeaders.Add("ts", (begin_time + 30000 * (log_index + parts)).ToString());

                //添加请求体
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("sn", "moses_ewt_video"),
                    new KeyValuePair<string, string>("log",data),
                });

                //发送请求
                await client.PostAsync(url, formContent);
                //var respone =

                //var status = ((int)respone.StatusCode);
                //var responeStr = await respone.Content.ReadAsStringAsync();
            }
        }

        public void CompleteCourse(long begin_time)
        {
            int minutes = (int)(length / 60);
            int num = (int)(minutes / 10) + 1;

            //如果minutes超过10则分成2个请求发送
            for (int i = 0; i < num; i++)
            {
                send(begin_time, minutes > 10 ? 10 : minutes);
                minutes -= 10;
            }
        }
    }
}
