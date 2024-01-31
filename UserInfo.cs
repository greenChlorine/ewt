namespace ewt360
{
    class UserInfo
    {
        public static string token;
        public static string userid;
        public static string schoolId;
        public static string homeworkId;
        public static long starttime;
        public static long endtime;
        public static string[] dayTS;
        public static string[] dayId;

        public static void getbasicinfo()
        {
            getSchoolId();
            getSummaryInfo();//前提条件是获取schoolid，顺便获取homeworkid和start,end使时间
            getDayDistribution();//前提条件是获取homeworkid
        }

        public static void getSchoolId()
        {
            schoolId = new json(request.get(ewt.getSchoolUserInfo, true)).getNameValue("schoolId");
        }
        public static void getSummaryInfo()
        {
            json rt = new json(request.get(string.Format(ewt.getHomeworkSummaryInfo,schoolId), true));
            homeworkId = rt.getNameValue("homeworkIds").Replace("[", "").Replace("]", "");
            starttime = long.Parse(rt.getNameValue("startDate"));
            endtime = long.Parse(rt.getNameValue("endDate"));
        }

        public static void getDayDistribution()
        {
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ewt.getHomeworkDistribution);
            //req.Method = "POST";
            //req.ContentType="application/json";
            //req.Headers.Add("token", token);

            string data = $"{{\"homeworkIds\":[{homeworkId}],\"isSelfTask\":false,\"userOptionTaskId\":null,\"schoolId\":{schoolId},\"sceneId\":\"111\"}}";
            //使用正则表达式比如去掉[]
            string resp = request.post(true, ewt.getHomeworkDistribution, data);
            json rep = new json(resp);

            //返回的数组中，index为奇数的是day，偶数的为dayId
            dayTS = rep.getNameValueArr("day");
            dayId = rep.getNameValueArr("dayId");
        }

    }
}
