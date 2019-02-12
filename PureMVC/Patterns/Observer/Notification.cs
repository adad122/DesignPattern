using PureMVC.Interfaces;

namespace PureMVC.Patterns.Observer
{
    /// <summary>
    /// Notification实体类
    /// </summary>
    public class Notification : INotification
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">通知标识字符</param>
        /// <param name="body">通知实体</param>
        /// <param name="type">通知类型</param>
        public Notification(string name, object body = null, string type = null)
        {
            Name = name;
            Body = body;
            Type = type;
        }

        /// <summary>
        /// 通知字符结构
        /// </summary>
        /// <returns>通知字符结构</returns>
        public override string ToString()
        {
            string msg = "Notification Name: " + Name;
            msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
            msg += "\nType:" + ((Type == null) ? "null" : Type);
            return msg;
        }

        /// <summary>
        /// 通知索引
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 通知实体
        /// </summary>
        public object Body { get; protected set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public string Type { get; protected set; }
    }
}