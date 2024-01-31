using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ewt360
{
    public partial class loginfrm : Form
    {
        public loginfrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("用户名或密码不能为空！", "错误", 0, MessageBoxIcon.Error);
                return;
            }
            if (checkBox1.Checked)
            {
                File.WriteAllText(@".\login", textBox1.Text + "\n" + Encrypt.AES_Encrypt(textBox2.Text));

            }
            login(textBox1.Text, Encrypt.AES_Encrypt(textBox2.Text));
            Close();
        }

        private void login(string username, string decrypt_pwd)
        {
            string data = "{\"platform\": 1,\"userName\": \"" + username + "\",\"password\": \"" + decrypt_pwd + "\",\"autoLogin\": false}";

            //发送请求获取token
            json loginResult = new json(request.post(false, ewt.login, data));
            string msg = loginResult.getNameValue("msg");
            if (msg != "操作成功") { MessageBox.Show("错误：" + msg, "登录失败", 0, MessageBoxIcon.Error); return; }

            //设置token和userid
            UserInfo.token = loginResult.getNameValue("token");
            UserInfo.userid = loginResult.getNameValue("userId");
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) { textBox2.PasswordChar = '\0'; return; }
            textBox2.PasswordChar = '\u26ab';
        }

        private void loginfrm_Load(object sender, EventArgs e)
        {
            if (File.Exists(@".\login"))
            {
                string[] info = File.ReadAllText(@".\login").Split('\n');
                login(info[0], info[1]);
                Close();
            }
        }
    }
}
