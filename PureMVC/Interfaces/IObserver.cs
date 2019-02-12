using System;

namespace PureMVC.Interfaces
{
    /// <summary>
    /// 观察者接口，由View对象进行维护，用于关联Mediator注册的Notification监听
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 通知用消息函数
        /// </summary>
        Action<INotification> NotifyMethod { set; }

        /// <summary>
        /// 监听者对象
        /// </summary>
        object NotifyContext { set; }

        /// <summary>
        /// 通知观察对象
        /// </summary>
        /// <param name="notification">通知实体</param>
        void NotifyObserver(INotification notification);

        /// <summary>
        /// 验证对象是否和监听主体相同
        /// </summary>
        /// <param name="obj">要验证的对象</param>
        /// <returns>验证结果</returns>
        bool CompareNotifyContext(object obj);
    }
}