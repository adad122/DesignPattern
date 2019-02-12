using System;
using PureMVC.Interfaces;

namespace PureMVC.Patterns.Observer
{
    /// <summary>
    /// 观察者实体类，由View对象进行维护，用于关联Mediator注册的Notification监听
    /// </summary>
    public class Observer : IObserver
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="notifyMethod">通知回调</param>
        /// <param name="notifyContext">对通知进行监听的对象</param>
        public Observer(Action<INotification> notifyMethod, object notifyContext)
        {
            NotifyMethod = notifyMethod;
            NotifyContext = notifyContext;
        }

        /// <summary>
        /// 对观察对象发送通知
        /// </summary>
        /// <param name="notification">通知实体</param>
        public virtual void NotifyObserver(INotification notification)
        {
            NotifyMethod(notification);
        }

        /// <summary>
        /// 验证对象是否和监听主体相同
        /// </summary>
        /// <param name="obj">要验证的对象</param>
        /// <returns>验证结果</returns>
        public virtual bool CompareNotifyContext(object obj)
        {
            return NotifyContext.Equals(obj);
        }

        /// <summary>
        /// 通知用消息函数
        /// </summary>
        public Action<INotification> NotifyMethod { get; set; }

        /// <summary>
        /// 监听者对象
        /// </summary>
        public object NotifyContext { get; set; }
    }
}