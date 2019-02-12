namespace PureMVC.Interfaces
{
    /// <summary>
    /// 命令接口，用于处理对应的通知
    /// </summary>
    public interface ICommand : INotifier
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="notification">通知结构类型</param>
        void Execute(INotification notification);
    }
}