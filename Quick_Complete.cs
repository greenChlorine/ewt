using System;
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
            int parts =(int)(length/60);
            int others = length % 60;


            //不能确定的参数oper_type,media_time,status
            string oper_type;
            string begin_time= "1706576971505";
            string dev_id=uuid2();
            string course_id=course;
            string trace_id=uuid2();
            string homework_id = UserInfo.homeworkId;
            string lesson_id = lesson;
            string log_index = "42";
            string uuid=uuid1(8);
            string media_time;
            string resolution= "2158*1080";
            string status;
            string stay_time;
            string timestamp;
            string userid=UserInfo.userid;
            string log_id=uuid2();
            string single_part = $"{{{{\"oper_type\":\"{{0}}\",\"version\":\"10.3.6\",\"begin_time\":\"{begin_time}\",\"brand\":\"vivo\",\"carrier\":\"N/A\",\"channel\":\"ewt360\",\"dev_id\":\"{dev_id}\",\"extra\":{{\"course_id\":\"{course_id}\",\"trace_id\":\"{trace_id}\",\"content_type\":\"video_record\",\"videoBizCode\":2013,\"homework_id\":\"{homework_id}\",\"lesson_id\":\"{lesson_id}\",\"log_index\":\"{log_index}\",\"uuid\":\"{uuid}\",\"fallback\":\"1\",\"isonline\":\"2\"}},\"imei\":\"\",\"imsi\":\"\",\"language\":\"zh\",\"media_time\":\"{{3}}\",\"device_model\":\"V2244A\",\"sdk_version\":\"2.0.95-test-rc21\",\"access\":\"NO_NETWORK\",\"online\":\"2\",\"end_time\":\"\",\"start_time\":\"\",\"phone_number\":\"\",\"os\":\"Android\",\"os_version\":\"13\",\"point_id\":\"64\",\"point_time\":\"60000\",\"resolution\":\"{resolution}\",\"speed\":\"2.0\",\"status\":\"{{1}}\",\"stay_time\":\"{{2}}\",\"ts\":{{4}},\"type\":\"video\",\"uploadStatus\":2,\"url\":\"com.mistong.ewt360.questionbank.ui.studyhistory.StudyHistoryDetailActivity\",\"userid\":\"{userid}\",\"log_id\":\"{log_id}\"}}}}";

            string data = "";

            //先添加整数分钟
            for(int i = 0; i < parts; i++)
            {
                oper_type = "";
                status = "2";
                stay_time = "60000";
                media_time = "60000";
                timestamp = begin_time + 60000 * i;

                data += single_part
                    .Replace("{0}", oper_type)
                    .Replace("{1}", status)
                    .Replace("{2}", stay_time)
                    .Replace("{3}", media_time)
                    .Replace("{4}", timestamp)+",";
            }
            //是否为整数

            oper_type = "";
            status = "2";
            stay_time = others.ToString();
            media_time = others.ToString();
            timestamp = begin_time + 60000 * parts + others;
            data += single_part
                    .Replace("{0}", oper_type)
                    .Replace("{1}", status)
                    .Replace("{2}", stay_time)
                    .Replace("{3}", media_time)
                    .Replace("{4}", timestamp);



            Console.WriteLine("[{0}]",data);
            //搞清楚单位
        }

        private string getCurrentTimeStamp()
        {
            return (DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0)).TotalMilliseconds.ToString();
        }


        private string uuid2()
        {
            //06f95b83-8e3c-3241-be3a-aa8fd139c295
            return uuid1(8) + "-" + uuid1(4) + "-" + uuid1(4) + "-" + uuid1(4) + "-" + uuid1(3);
        }

        private string uuid1(int num)
        {
            const string chars = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890abcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzabcdefghigklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZABCDEFGHIGKLMNOPQRSTUVWXYZABCDEFGHIGKLMNOPQRSTUVWXYZABCDEFGHIGKLMNOPQRSTUVWXYZABCDEFGHIGKLMNOPQRSTUVWXYZ";
            char[] uuid = new char[num];
            Random rnd=new Random();

            for (int i = 0; i < num; i++)
            {
                uuid[i] = chars[rnd.Next(390)];
            }
            return new string(uuid);
        }
    }
}
