using System;

namespace PureMVC.Interfaces
{
    /// <summary>
    /// Controller对象接口，用于关联和管理Command和Notification
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// 注册Notification对象和ICommand构造引用
        /// </summary>
        /// <param name="notificationName">通知名</param>
        /// <param name="commandClassRef">关联Command的构造引用</param>
        void RegisterCommand(string notificationName, Func<ICommand> commandClassRef);

        /// <summary>
        /// 执行对应的Notification
        /// </summary>
        /// <param name="notification">Notification实体</param>
        void ExecuteCommand(INotification notification);

        /// <summary>
        /// 移除对应的Notification
        /// </summary>
        /// <param name="notificationName">Notification名字</param>
        void RemoveCommand(string notificationName);

        /// <summary>
        /// 是否存在Notification
        /// </summary>
        /// <param name="notificationName">Notification名字</param>
        /// <returns>查询结果</returns>
        bool HasCommand(string notificationName);
    }
}