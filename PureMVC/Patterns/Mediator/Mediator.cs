using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Mediator
{
    /// <summary>
    /// 中介者实体类，定义了中介者的基本属性
    /// </summary>
    public class Mediator : Notifier, IMediator
    {
        /// <summary>
        /// 中介者静态索引
        /// </summary>
        public static string NAME = "Mediator";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <param name="viewComponent">中介者管理的UI对象引用</param>
        public Mediator(string mediatorName, object viewComponent = null)
        {
            MediatorName = mediatorName ?? Mediator.NAME;
            ViewComponent = viewComponent;
        }

        /// <summary>
        /// 中介者监听的通知标记
        /// </summary>
        /// <returns>监听的通知字符串数组</returns>
        public virtual string[] ListNotificationInterests()
        {
            return new string[0];
        }

        /// <summary>
        /// 接收到通知的回调
        /// </summary>
        /// <param name="notification">通知本体</param>
        public virtual void HandleNotification(INotification notification)
        {
        }

        /// <summary>
        /// 当中介者被View注册
        /// </summary>
        public virtual void OnRegister()
        {
        }

        /// <summary>
        /// 当中介者被View移除
        /// </summary>
        public virtual void OnRemove()
        {
        }

        /// <summary>
        /// 中介者索引
        /// </summary>
        public string MediatorName { get; protected set; }

        /// <summary>
        /// 中介者管理的UI对象引用
        /// </summary>
        public object ViewComponent { get; set; }
    }
}