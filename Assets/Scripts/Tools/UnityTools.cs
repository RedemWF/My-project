
using UnityEngine;

namespace Tools
{
    public static class UnityTools
    {
        /// <summary>
        /// 位置消息解析
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector3 ParseVector3(string str)
        {
            string[] strs = str.Split(',');
            var x = float.Parse(strs[0]);
            var y = float.Parse(strs[1]);
            var z = float.Parse(strs[2]);
            return new Vector3(x, y, z);
        }
    }
}