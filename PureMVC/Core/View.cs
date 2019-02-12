using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using PureMVC.Patterns.Singleton;

namespace PureMVC.Core
{
    public class View : SingletonEx<IController>, IView
    {
        /// <summary>
        /// 中介者集合
        /// </summary>
        protected IDictionary<string, IMediator> mediatorMap;

        /// <summary>
        /// 观察者集合
        /// </summary>
        protected IDictionary<string, IList<IObserver>> observerMap;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected View()
        {
            mediatorMap = new Dictionary<string, IMediator>();
            observerMap = new Dictionary<string, IList<IObserver>>();
            InitializeView();
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        protected virtual void InitializeView()
        {
        }

        /// <summary>
        /// 注册观察者对象
        /// </summary>
        /// <param name="notificationName">通知名</param>
        /// <param name="observer">观察者对象</param>
        public void RegisterObserver(string notificationName, IObserver observer)
        {
            if (!observerMap.ContainsKey(notificationName))
            {
                observerMap.Add(notificationName, new List<IObserver> { observer });
            }
            else
            {
                var observers = observerMap[notificationName];
                observers.Add(observer);
            }
        }

        /// <summary>
        /// 移除观察者对象
        /// </summary>
        /// <param name="notificationName">通知名</param>
        /// <param name="notifyContext">对象实体引用</param>
        public void RemoveObserver(string notificationName, object notifyContext)
        {
            if (observerMap.ContainsKey(notificationName))
            {
                var observers = observerMap[notificationName];

                for (int i = 0; i < observers.Count; i++)
                {
                    if (observers[i].CompareNotifyContext(notifyContext))
                    {
                        observers.RemoveAt(i);
                        break;
                    }
                }

                if (observers.Count == 0)
                    observerMap.Remove(notificationName);
            }
        }

        /// <summary>
        /// 通知View下面的的所有观察者
        /// </summary>
        /// <param name="notification">发送的通知</param>
        public void NotifyObservers(INotification notification)
        {
            if (observerMap.ContainsKey(notification.Name))
            {
                var observers = observerMap[notification.Name];

                foreach (IObserver observer in observers)
                {
                    observer.NotifyObserver(notification);
                }
            }
        }

        /// <summary>
        /// 注册中介者
        /// </summary>
        /// <param name="mediator">中介者对象</param>
        public void RegisterMediator(IMediator mediator)
        {
            if (!mediatorMap.ContainsKey(mediator.MediatorName))
            {
                mediatorMap.Add(mediator.MediatorName, mediator);

                string[] interests = mediator.ListNotificationInterests();

                if (interests.Length > 0)
                {
                    IObserver observer = new Observer(mediator.HandleNotification, mediator);
                    for (int i = 0; i < interests.Length; i++)
                    {
                        RegisterObserver(interests[i], observer);
                    }
                }
                mediator.OnRegister();
            }
        }

        /// <summary>
        /// 获取中介者实体
        /// </summary>
        /// <param name="mediatorName">中介者特征名</param>
        /// <returns>中介者对象</returns>
        public IMediator RetrieveMediator(string mediatorName)
        {
            if (mediatorMap.ContainsKey(mediatorName))
            {
                return mediatorMap[mediatorName];
            }

            return null;
        }

        /// <summary>
        /// 移除中介者对象
        /// </summary>
        /// <param name="mediatorName">中介者特征名</param>
        /// <returns>中介者对象</returns>
        public IMediator RemoveMediator(string mediatorName)
        {
            if (mediatorMap.ContainsKey(mediatorName))
            {
                var mediator = mediatorMap[mediatorName];
                string[] interests = mediator.ListNotificationInterests();
                for (int i = 0; i < interests.Length; i++)
                {
                    RemoveObserver(interests[i], mediator);
                }
                mediator.OnRemove();

                return mediator;
            }

            return null;
        }

        /// <summary>
        /// 是否存在中介者
        /// </summary>
        /// <param name="mediatorName">中介者特征名</param>
        /// <returns>查询结果</returns>
        public bool HasMediator(string mediatorName)
        {
            return mediatorMap.ContainsKey(mediatorName);
        }
    }
}