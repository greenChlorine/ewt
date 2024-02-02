using ewt360.Properties;
using System;
using System.Windows.Forms;

namespace ewt360
{
    public partial class courseitem : UserControl, IDisposable
    {
        public string title;
        public string lessonId;
        public string imgurl;
        public string ratio;
        public string type;
        public int index;
        public courseitem()
        {
            InitializeComponent();
            //label3.Text = double.Parse(_ratio)*100+"%";
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

        private void courseitem_ControlRemoved(object sender, ControlEventArgs e)
        {
            Console.WriteLine("");
        }

        private void courseitem_Leave(object sender, EventArgs e)
        {
            Console.WriteLine();
        }


    }
}
