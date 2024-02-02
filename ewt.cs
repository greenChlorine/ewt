namespace ewt360
{
    //该类是用来存储相关的接口
    class ewt
    {
        public const string login = "https://gateway.ewt360.com/api/authcenter/v2/oauth/login/account";
        //public const string logout = "https://gateway.ewt360.com/api/authcenter/oauth/logout/guide/isshow";
        public const string getSchoolUserInfo = "https://gateway.ewt360.com/api/eteacherproduct/school/getSchoolUserInfo";
        public const string getHomeworkDistribution = "https://gateway.ewt360.com/api/homeworkprod/homework/student/holiday/getHomeworkDistribution?sceneId=136";
        public const string getUserInfo = "https://web.ewt360.com/api/usercenter/user/info";
        public const string getTaskProcess = "https://gateway.ewt360.com/api/homeworkprod/homework/student/holiday/getTaskProcess?sceneId=136";
        public const string getpageHomeworkTasks = "https://gateway.ewt360.com/api/homeworkprod/homework/student/holiday/pageHomeworkTasks";
        public const string getPlayerGlobalConf = "https://web.ewt360.com/api/videoplayerprod/videoplayer/getPlayerGlobalConf?videoBizCode=1013";
        public const string bach = "https://bfe.ewt360.com/monitor/web/collect/batch";
        public const string getExternalVideoInfo = "https://web.ewt360.com/api/videoplayerprod/videoplayer/getExternalVideoInfo?videoBizCode=1013&lessonId={0}&videoToken={1}&sdkVersion=3.0.9";
        public const string getPlayerToken = "https://gateway.ewt360.com/api/homeworkprod/player/getPlayerToken";
        public const string getHomeworkSummaryInfo = "https://gateway.ewt360.com/api/homeworkprod/homework/student/holiday/getHomeworkSummaryInfo?schoolId={0}&sceneId=136";
        public const string getMyClassList = "https://teacher.ewt360.com/api/eteacherproduct/studentManage/getMyClassList";
        public const string getPlayerLessonConfig = "https://gateway.ewt360.com/api/homeworkprod/player/getPlayerLessonConfig";
        public const string getreportId = "https://web.ewt360.com/customerApi/api/studyprod/web/answer/report?paperId={0}&platform=1&bizCode=204&reportId=0&isRepeat=1";
        public const string getPaperAnswer = "https://web.ewt360.com/customerApi/api/studyprod/web/answer/webreport?platform=1&reportId={0}&userId={1}&bizCode=204";
        public const string clog = "https://https://clog.ewt360.com/?TrUserId={0}&TrLessonId={1}&TrUuId={2}&TrVideoBizCode=2013&TrFallback=0&reqId=138248612-1508d0ea-b9f0-4c8c-aac3-52b8e75a9a22";
    }
}
