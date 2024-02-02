using System;
using System.Windows.Forms;

namespace ewt360
{
    public partial class paperfrm : Form
    {
        json answer;
        int count;
        public paperfrm(json _answer, int _count)
        {
            InitializeComponent();

            answer = _answer;
            count = _count;

            answer.jsonData = answer.jsonData.Replace("\\\"", "\'");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.ImageLocation = "http://file.ewt360.com/file/1431971044566720587";
            string[] rightAnswers = answer.getNameValueArr("rightAnswer");
            foreach (string rightAnswer in rightAnswers)
            {
                MessageBox.Show(rightAnswers[0]);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // paperfrm
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "paperfrm";
            this.ResumeLayout(false);

        }
    }
}
