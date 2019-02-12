using System;

namespace PureMVC.Interfaces
{
    /// <summary>
    /// Facade对象接口，用于统一关联和管理View,Controller，Model的方法
    /// </summary>
    public interface IFacade : INotifier
    {
        /// <summary>
        /// 注册代理对象
        /// </summary>
        /// <param name="proxy">代理实体</param>
        void RegisterProxy(IProxy proxy);

        /// <summary>
        /// 获得代理对象
        /// </summary>
        /// <param name="proxyName">代理识别标记</param>
        /// <returns>代理对象</returns>
        IProxy RetrieveProxy(string proxyName);

        /// <summary>
        /// 移除代理对象
        /// </summary>
        /// <param name="proxyName">代理识别标记</param>
        /// <returns>代理对象</returns>
        IProxy RemoveProxy(string proxyName);

        /// <summary>
        /// 是否存在代理对象
        /// </summary>
        /// <param name="proxyName">代理识别标记</param>
        /// <returns>查询结果</returns>
        bool HasProxy(string proxyName);

        /// <summary>
        /// 注册Notification对象和ICommand构造引用
        /// </summary>
        /// <param name="notificationName">通知名</param>
        /// <param name="commandClassRef">关联Command的构造引用</param>
        void RegisterCommand(string notificationName, Func<ICommand> commandClassRef);

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

        /// <summary>
        /// 注册中介者
        /// </summary>
        /// <param name="mediator">中介者对象</param>
        void RegisterMediator(IMediator mediator);

        /// <summary>
        /// 获取中介者实体
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <returns></returns>
        IMediator RetrieveMediator(string mediatorName);

        /// <summary>
        /// 移除中介者对象
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <returns></returns>
        IMediator RemoveMediator(string mediatorName);

        /// <summary>
        /// 是否存在中介者
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <returns>查询结果</returns>
        bool HasMediator(string mediatorName);

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notification">发送的通知</param>
        void NotifyObservers(INotification notification);
    }
}