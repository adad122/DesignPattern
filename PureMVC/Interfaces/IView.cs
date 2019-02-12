namespace PureMVC.Interfaces
{
    /// <summary>
    /// View对象接口，用于关联和管理Observer和Mediator
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// 注册观察者对象
        /// </summary>
        /// <param name="notificationName">通知名</param>
        /// <param name="observer">观察者对象</param>
        void RegisterObserver(string notificationName, IObserver observer);

        /// <summary>
        /// 移除观察者对象
        /// </summary>
        /// <param name="notificationName">通知名</param>
        /// <param name="notifyContext"></param>
        void RemoveObserver(string notificationName, object notifyContext);

        /// <summary>
        /// 通知View下面的的所有观察者
        /// </summary>
        /// <param name="notification">发送的通知</param>
        void NotifyObservers(INotification notification);

        /// <summary>
        /// 注册中介者
        /// </summary>
        /// <param name="mediator">中介者对象</param>
        void RegisterMediator(IMediator mediator);

        /// <summary>
        /// 获取中介者实体
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <returns>中介者对象</returns>
        IMediator RetrieveMediator(string mediatorName);

        /// <summary>
        /// 移除中介者对象
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <returns>中介者对象</returns>
        IMediator RemoveMediator(string mediatorName);

        /// <summary>
        /// 是否存在中介者
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <returns>查询结果</returns>
        bool HasMediator(string mediatorName);
    }
}