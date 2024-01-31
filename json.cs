using System.Text.RegularExpressions;

namespace ewt360
{
    public class json
    {
        public string jsonData;
        public json(string data) { jsonData = data.Replace("\n", "").Replace("\r", "").Replace(" ", "").Replace("[", "").Replace("]", ""); }

        public string getNameValue(string key)
        {
            string regex = $"\"{key}\":\"?(.*?)[\",}}]";
            return Regex.Match(jsonData, regex).Groups[1].Value;
        }

        public string[] getNameValueArr(string key)
        {
            string regex = $"\"{key}\":\"?(.*?)[\",}}]";
            MatchCollection matches = Regex.Matches(jsonData, regex);

            //转化为string数组
            int matchNum = matches.Count;
            string[] matchesArr = new string[matchNum];
            for (int i = 0; i < matchNum; i++)
            {
                matchesArr[i] = matches[i].Groups[1].Value;
            }

            return matchesArr;
        }
    }
}
