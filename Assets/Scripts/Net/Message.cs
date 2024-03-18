using System;
using System.Linq;
using System.Text;
using Common;

namespace Net
{
    /// <summary>
    /// 服务器消息类
    /// </summary>
    public class Message
    {

        //Buffer
        private byte[] data = new byte[1024];
        private int startIndex = 0;//存取了多少个字节的数据在数组里面

        //public void AddCount(int count)
        //{
        //    startIndex += count;
        //}
        //数据
        public byte[] Data
        {
            get { return data; }
        }
        //开始索引
        public int StartIndex
        {
            get { return startIndex; }
        }
        //数据大小
        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }
        /// <summary>
        /// 解析数据
        /// </summary>
        public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallback)
        {
            //索引位数+Data位数
            startIndex += newDataAmount;
            while (true)
            {
                //判断是否完整
                if (startIndex <= 4) return;
                //获取消息大小
                int count = BitConverter.ToInt32(data, 0);
                //判断是否完整
                if ((startIndex - 4) >= count)
                {
                    //Console.WriteLine(startIndex);
                    //Console.WriteLine(count);
                    //string s = Encoding.UTF8.GetString(data, 4, count);
                    //Console.WriteLine("解析出来一条数据：" + s);
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                    string s = Encoding.UTF8.GetString(data, 8, count - 4);
                    processDataCallback(actionCode, s);
                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                    startIndex -= (count + 4);
                }
                else
                {
                    //继续接收
                    break;
                }
            }
        }
        //public static byte[] PackData(ActionCode actionCode, string data)
        //{
        //    byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
        //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        //    int dataAmount = requestCodeBytes.Length + dataBytes.Length;
        //    byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        //    byte[] newBytes = dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>();//Concat(dataBytes);
        //    return newBytes.Concat(dataBytes).ToArray<byte>();
        //}
        public static byte[] PackData(RequestCode requestData, ActionCode actionCode, string data)
        {
            byte[] requestCodeBytes = BitConverter.GetBytes((int)requestData);
            byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = requestCodeBytes.Length + dataBytes.Length + actionCodeBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            //byte[] newBytes = dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>();//Concat(dataBytes);
            //return newBytes.Concat(dataBytes).ToArray<byte>();
            return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>()
                .Concat(actionCodeBytes).ToArray<byte>()
                .Concat(dataBytes).ToArray<byte>();
        }
    }
}
