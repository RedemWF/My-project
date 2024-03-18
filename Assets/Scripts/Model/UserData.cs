using System;

namespace Model
{
    /// <summary>
    /// 用户数据模型
    /// </summary>
    public class UserData
    {
        public UserData(string username, int totalCount, int winCount)
        {
            UserName = username;
            TotalCount = totalCount;
            WinCount = winCount;
        }

        public UserData(int id, string username, int totalCount, int winCount)
        {
            UserId = id;
            UserName = username;
            TotalCount = totalCount;
            WinCount = winCount;
        }

        public UserData(string userdata)
        {
            string[] strs = userdata.Split(',');
            UserId = int.Parse(strs[0]);
            UserName = strs[1];
            TotalCount = int.Parse(strs[2]);
            WinCount = int.Parse(strs[3]);
        }
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public int TotalCount { get; private set; }
        public int WinCount { get; private set; }
    }
}