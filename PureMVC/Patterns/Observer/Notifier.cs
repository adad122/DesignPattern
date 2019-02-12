using PureMVC.Interfaces;

namespace PureMVC.Patterns.Observer
{
    /// <summary>
    /// 消息发送者实体对象
    /// </summary>
    public class Notifier : INotifier
    {
        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            Facade.Facade.instance.SendNotification(notificationName, body, type);
        }
    }
}