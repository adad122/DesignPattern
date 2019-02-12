namespace PureMVC.Interfaces
{
    /// <summary>
    /// 中介者接口，定义了中介者的基本属性
    /// </summary>
    public interface IMediator : INotifier
    {
        /// <summary>
        /// 中介者索引
        /// </summary>
        string MediatorName { get; }

        /// <summary>
        /// 中介者管理的UI对象引用
        /// </summary>
        object ViewComponent { get; set; }

        /// <summary>
        /// 中介者监听的通知标记
        /// </summary>
        /// <returns>监听的通知字符串数组</returns>
        string[] ListNotificationInterests();

        /// <summary>
        /// 接收到通知的回调
        /// </summary>
        /// <param name="notification">通知本体</param>
        void HandleNotification(INotification notification);

        /// <summary>
        /// 当中介者被View注册
        /// </summary>
        void OnRegister();

        /// <summary>
        /// 当中介者被View移除
        /// </summary>
        void OnRemove();
    }
}