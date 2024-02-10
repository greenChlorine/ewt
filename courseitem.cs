using ewt360.Properties;
using System;
using System.Windows.Forms;

namespace ewt360
{
    public partial class courseitem : UserControl
    {
        public string title;
        public string lessonId;
        //public string imgurl;
        public string ratio;
        public string type;
        public int index;
        public courseitem()
        {
            InitializeComponent();
        }

        private void courseitem_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(0, this.Height * index);
            label1.Text = string.Format("第{0}节：", index);//设置第几节

            if (title.Length >= 12)
                label2.Text = title.Substring(0, 12) + "...";
            else
                label2.Text = title;//课程标题


            //显示进度条与图片
            typeimg.SizeMode = PictureBoxSizeMode.Zoom;
            if (type == "试卷")
            {
                ratiobar.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                paperbtn.Visible = true;
                typeimg.Image = Resources.paper;
            }
            else
            {
                typeimg.Image = Resources.video;
                ratiobar.Maximum = 100;
                ratiobar.Value = (int)(double.Parse(ratio) * 100);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new VideoPlayer(lessonId).ShowDialog();
            //MessageBox.Show("This function is building!", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string payload = $"{{\"lessonIdList\":[{lessonId}],\"homeworkId\":{UserInfo.homeworkId},\"schoolId\":{UserInfo.schoolId}}}";

            //该api包含paperId等等
            json req = new json(request.post(true, ewt.getPlayerLessonConfig, payload));
            if (!req.jsonData.Contains("studyGuidePdfWithWaterMark"))
            {
                MessageBox.Show("该课程没有学案！", "找不到", 0, MessageBoxIcon.Warning);
                return;
            }

            string pdfSource = req.getNameValue("studyGuideAnswerPdfWithWaterMark");
            //new pdfViewer(guideurl.Split('?')[0]).ShowDialog();
            if (!pdfSource.Equals("null"))
            { System.Diagnostics.Process.Start(pdfSource); }
            else
            { MessageBox.Show("Cannot find the pdf source!"); }
        }

        private void paperbtn_Click(object sender, EventArgs e)
        {
            //这里是完成试卷的实验代码
            /*
             * 基本思路：
             * 1.首先获取试卷的reportId和paperId
             * paperId就是contentId
             * reportId需要发送请求获取
             * 
             * 2.获取questId
             * 
             * 3.先submintpaper提交试卷，但是这时试卷还没有批改。
             * 因为不提交试卷，是获取不到正确答案的，又因为没有批改，所以Submitanswer后完成批改是有分数的
             * 
             * 4.循环提交答案（可能需要delay）
             * 
             * 5.完成批改
             */
            const string bizCode = "205";

            string paperId = lessonId;
            string reportId = new json(request.get(string.Format(ewt.getReportId, paperId, bizCode, UserInfo.homeworkId), true)).getNameValue("reportId");

            string getQuestionId_json = $"{{\"client\":1,\"paperId\":\"{paperId}\",\"platform\":\"1\",\"reportId\":\"{reportId}\",\"bizCode\":\"{bizCode}\",\"userId\":\"{UserInfo.userid}\"}}";
            string[] questionIdArr = new json(request.post(true, ewt.getQuestionId, getQuestionId_json)).getNameValueArr("questionId");

        }
    }
}
