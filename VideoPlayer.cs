using System;
using System.Windows.Forms;

namespace ewt360
{

    public partial class VideoPlayer : Form
    {
        string lessonId;
        public VideoPlayer(string _lessonId)
        {
            InitializeComponent();
            lessonId = _lessonId;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private string getPlayerToken()
        {
            string data = $"{{\"lessonId\":{lessonId},\"type\":1,\"schoolId\":{UserInfo.schoolId}}}";

            json resp = new json(request.post(true, ewt.getPlayerToken, data));
            return resp.getNameValue("data");

        }

        private void Play()
        {
            string requestUrl = string.Format(ewt.getExternalVideoInfo, lessonId, getPlayerToken());
            json resp = new json(request.get(requestUrl, true));

            //先获取psign
            string psign = resp.getNameValue("psign");

            //获取uuid和videoId为下一步drmToken的网址部分做准备
            string uuid = resp.getNameValueArr("platFormUuId")[0];//因为第一个返回为空
            string videoId = resp.getNameValueArr("videoId")[0];
            //初始key和iv为32位0和16位0，公钥加密后得到
            const string cipheredOverlayKey = "0fbfdba4a1ba025a1d64b557a1919c4a9eb9d3d13321dfeafb0abc1d126e01df720ed6852ffc3205b18c025d78a272804a4ff8ef9b7f293ff681c0180fc42f9297fef70eed116246db139a6c7e7981c6bb5a50e08af1274dedf6a96d86113264b5d1f083bca432e958ae17f2668c7043af1ee40fcc88945d0badf2b0b12098d1";
            const string cipheredOverlayIv = "5425f8bd243df601379bab09b3a18390d156b6ca1740a52c8b1f7ce02d48fd9f3a0bad1ddc864fc0cb9983e05b9d11ded287240718dbac38d3d21cb405f457df08da0a565fe0ff8a66f884bb44df49e387787fe80a4cffe1f74ed26bd22939f07f14a3be0b41316a2f563506423b0a1627efb7a9b1c3b2251a793b0d737598b0";

            //发送请求
            json drmresp = new json(request.get($"https://playvideo.qcloud.com/getplayinfo/v4/{uuid}/{videoId}?psign={psign}&cipheredOverlayKey={cipheredOverlayKey}&cipheredOverlayIv={cipheredOverlayIv}&keyId=1", false));

            /*
             * 惊天Bug：2023年8月26日
            overlaykey和iv分别置32位0，和16位0，然后用公钥RSA加密，用这个请求，会直接返回mp4

            附件：公钥：
            -----BEGIN PUBLIC KEY-----
            MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC3pDA7GTxOvNbXRGMi9QSIzQEI
            +EMD1HcUPJSQSFuRkZkWo4VQECuPRg/xVjqwX1yUrHUvGQJsBwTS/6LIcQiSwYsO
            qf+8TWxGQOJyW46gPPQVzTjNTiUoq435QB0v11lNxvKWBQIZLmacUZ2r1APta7i/
            MY4Lx9XlZVMZNUdUywIDAQAB
            -----END PUBLIC KEY-----
            */
            //string drmToken = drmresp.getNameValue("drmToken");
            //直接写死了urls[3]
            //axWindowsMediaPlayer1.URL = drmresp.getNameValueArr("url")[3];


            System.Diagnostics.Process.Start(drmresp.getNameValueArr("url")[3]);
            Console.WriteLine(drmresp.getNameValueArr("url")[3]);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string payload = $"{{\"lessonIdList\":[{lessonId}],\"homeworkId\":{UserInfo.homeworkId},\"schoolId\":{UserInfo.schoolId}}}";

            //该api包含paperId等等
            json req = new json(request.post(true, ewt.getPlayerLessonConfig, payload));
            string guideurl = req.getNameValue("studyGuideUrl");

            if (guideurl.Equals(string.Empty))
            {
                MessageBox.Show("该课程没有学习检测和学案！", "找不到", 0, MessageBoxIcon.Warning);
                return;
            }
            string paperId = req.getNameValue("paperId");


            //string reportId = new json(request.get(string.Format(ewt.getreportId, paperId), true)).getNameValue("reportId");
            string questionCount = req.getNameValue("questionCount");

            //json answers = new json(request.get(string.Format(ewt.getPaperAnswer, reportId, UserInfo.userid), true));
            //new paperfrm(answers, int.Parse(questionCount)).Show();
        }

        private void VideoPlayer_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void VideoPlayer_Load(object sender, EventArgs e)
        {
            //axWindowsMediaPlayer1.Width=this.Width; 
            //axWindowsMediaPlayer1.Height = this.Height;
            Play();

        }
    }
}