using System;
using System.Windows.Forms;

namespace ewt360
{
    public partial class main : Form
    {
        json complete;
        json incomplete;

        public static bool jump = false;


        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            //设置控件属性
            notlist.AutoScroll = true;//自动滚动
            finishlist.AutoScroll = true;



            UserInfo.getbasicinfo();

            //下面开始处理数据，数据都储存在UserInfo类中

            //设置datetimepicker的日期范围
            DateTime start = TimeStampToDateTime(UserInfo.starttime);
            DateTime end = TimeStampToDateTime(UserInfo.endtime);
            dateTimePicker1.MaxDate = end;
            dateTimePicker1.MinDate = start;
            //如果今天的日期超出最大日期或者先于开始日期，则自动选择到第一天。适用于任务过期后重新查看
            //int Compare (DateTime t1, DateTime t2);
            //小于零	t1 早于 t2。
            //等于零 t1 与 t2 相同。
            //大于零 t1 晚于 t2。
            if (DateTime.Compare(end, DateTime.Now) < 0 && DateTime.Compare(start, DateTime.Now) > 0)
                dateTimePicker1.Value = start;

            //显示用户信息
            json info = new json(request.get(ewt.getUserInfo, true));
            json classinfo = new json(request.get(ewt.getMyClassList, true));
            this.Text = $"欢迎来自{info.getNameValue("schoolName")}{classinfo.getNameValue("className")}的{info.getNameValue("realName")}同学，班主任是{classinfo.getNameValue("headTeacherName")}老师";

            //显示今天的任务列表
            display();

        }

        private DateTime TimeStampToDateTime(long timestamp)
        {
            return new DateTime(1970, 1, 1, 8, 0, 0).AddMilliseconds(timestamp);
        }

        private void button1_Click(object sender, EventArgs e) => display();

        private void display()
        {
            //由于控件选择的value值默认有23：59：59，所以要去掉
            DateTime selectday = dateTimePicker1.Value.Date;

            //获取到day和dayId
            //出现发生超出索引范围的错误
            //原因：并不是每一天都有数值（休息），这样导致实际ids的长度较小
            //解决：先把日期转化为时间戳，查询是否存在这一天
            long Current_st;
            Current_st = (long)(selectday - new DateTime(1970, 1, 1, 8, 0, 0)).TotalMilliseconds;
            //查询是否存在这一天

            //查询选择的这一天在dayTS的位置，没有就返回-1，相当于数组的索引
            int index = Array.IndexOf(UserInfo.dayTS, Current_st.ToString());


            if (index == -1)
            {
                MessageBox.Show("当天无任务！", "找不到这一天", 0, MessageBoxIcon.Information);
                return;
            }


            //存在这一天，继续执行
            //注意日期的排列不连续
            //由于day和dayId是一一对应的，所以数组索引都为index
            string day = UserInfo.dayTS[index];
            string dayId = UserInfo.dayId[index];

            //获取当日的任务进度
            string taskproreqdata = "{\"dayId\": [\"" + dayId + "\"],\"day\": " + day + ",\"homeworkIds\": [" + UserInfo.homeworkId + "],\"isSelfTask\": false,\"userOptionTaskId\": null,\"schoolId\": " + UserInfo.schoolId + ",\"sceneId\": \"111\"}";
            json taskreq = new json(request.post(true, ewt.getTaskProcess, taskproreqdata));
            double rate = double.Parse(taskreq.getNameValue("progressRate")) * 100;
            progressBar1.Value = (int)rate;
            string count = taskreq.getNameValue("count");
            string finishCount = taskreq.getNameValue("finishCount");
            string no = taskreq.getNameValue("noFinishCount");
            label1.Text = string.Format("今日共{0}个任务，已完成{1}个，未完成{2}个", count, finishCount, no);
            label2.Text = rate + "%";

            //为listbox清空项
            //cleanControls(notlist);
            //cleanControls(finishlist);

            cleanControls(notlist);
            cleanControls(finishlist);


            //string status = "1";
            //1为已完成，0为未完成


            //先添加未完成的
            addItems(0, dayId, day);

            //再添加已完成的
            addItems(1, dayId, day);

        }

        private void cleanControls(FlowLayoutPanel list)
        {
            foreach (Control c1 in list.Controls)
            {
                //pannel
                foreach (Control c2 in c1.Controls)
                {
                    foreach (Control c3 in c2.Controls)
                    {
                        c3.Dispose();
                    }
                }
            }
            list.Controls.Clear();
        }

        //为listbox添加项
        private void addItems(int status, string dayId, string day)
        {


            //先请求数据在添加

            string payload = "{\"dayId\":[\"" + dayId + "\"],\"day\":" + day + ",\"status\":" + status + ",\"homeworkIds\":[" + UserInfo.homeworkId + "],\"isSelfTask\":false,\"userOptionTaskId\":null,\"pageIndex\":1,\"pageSize\":30,\"missionType\":0,\"schoolId\":" + UserInfo.schoolId + ",\"sceneId\":\"136\"}";
            json data = new json(request.post(true, ewt.getpageHomeworkTasks, payload));
            if (status == 0) incomplete = data;

            //请求得到的数据，标题，进度，lessonId,图片
            //试卷的lessonid，contentid,imgs
            string[] titles = data.getNameValueArr("title");//标题
            string[] ratio = data.getNameValueArr("ratio");//该课程的进度
            string[] lessonId = data.getNameValueArr("contentId");//lessonId
            string[] imgs = data.getNameValueArr("imgUrl");//课程封面
            string[] type = data.getNameValueArr("contentTypeName");


            int item_amount = titles.Length;
            FlowLayoutPanel panel = status == 0 ? notlist : finishlist;

            //为panel添加自定义控件
            for (int i = 0; i < item_amount; i++)
            {

                var item = new courseitem
                {
                    //传参
                    index = i + 1,//课程结束从1开始，要加1
                    title = titles[i],
                    ratio = ratio[i],
                    lessonId = lessonId[i],
                    imgurl = imgs[i],
                    type = type[i],

                };

                //添加控件
                panel.Controls.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime changed = dateTimePicker1.Value.AddDays(-1);
            if (changed < dateTimePicker1.MinDate)
            {
                MessageBox.Show("当前已是最小的日期了!", "Wrong", 0, MessageBoxIcon.Error);
                return;
            }
            dateTimePicker1.Value = changed;
            display();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime changed = dateTimePicker1.Value.AddDays(1);
            if (changed > dateTimePicker1.MaxDate)
            {
                MessageBox.Show("当前已是最大的日期了!", "Wrong", 0, MessageBoxIcon.Error);
                return;
            }
            dateTimePicker1.Value = changed;
            display();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] contentId = incomplete.getNameValueArr("contentId");
            string[] parentContentId = incomplete.getNameValueArr("parentContentId");
            string[] length = incomplete.getNameValueArr("duration");
            string[] type = incomplete.getNameValueArr("contentTypeName");

            //判断是否需要看课还是试卷
            if (contentId.Length == 0)
            {
                //Console.WriteLine("当日没有课程！");
                MessageBox.Show("该天没有课程需要看！");
                dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
                display();
                return;
            }


            for (int i = 0; i < contentId.Length; i++)
            {
                if (jump)
                {
                    jump = false;
                    return;
                }

                if (type[i] == "试卷")
                {
                    //MessageBox.Show("试卷不需要看！");
                    Console.WriteLine("已经为你跳过试卷了！");
                    continue;
                }
                //new Form1(contentId[i], parentContentId[i], int.Parse(length[i])).ShowDialog();
                var frm = new finishCourse(parentContentId[i], contentId[i], int.Parse(length[i]));

                frm.ShowDialog();

                display();//刷新菜单
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Quick_Complete(incomplete.getNameValueArr("parentContentId")[0], incomplete.getNameValueArr("contentId")[0], incomplete.getNameValueArr("duration")[0]).ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            notlist.Dispose();
            finishlist.Dispose();

        }
    }
}
