using System;
using System.Drawing;
using System.Windows.Forms;

namespace ewt360
{
    public partial class courseitem : UserControl
    {
        string text;
        string lessonId;
        string imgurl;
        int index;
        public courseitem(int _index, string _className,string _ratio,string _lessonId,string _imgurl)
        {
            InitializeComponent();
            text = _className;
            index = _index;
            lessonId = _lessonId;
            imgurl = _imgurl;
            //label3.Text = double.Parse(_ratio)*100+"%";
        }

        private void courseitem_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, this.Height * index);
            label1.Text = string.Format("第{0}节：", index + 1);
            label2.Text = text;

            pictureBox1.LoadAsync(imgurl);
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
    }
}
