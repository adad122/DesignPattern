namespace PureMVC.Interfaces
{
    /// <summary>
    /// 消息通知接口，可用于发送通知
    /// </summary>
    public interface INotifier
    {
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notificationName">通知索引</param>
        /// <param name="body">通知数据体</param>
        /// <param name="type">通知类型</param>
        void SendNotification(string notificationName, object body = null, string type = null);
    }
}