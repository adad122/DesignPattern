using System;
using PureMVC.Core;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using PureMVC.Patterns.Singleton;

namespace PureMVC.Patterns.Facade
{
    /// <summary>
    /// Facade对象实体，用于统一关联和管理View,Controller，Model的方法
    /// </summary>
    public class Facade : SingletonEx<Facade>, IFacade
    {
        protected IController controller;

        protected IModel model;

        protected IView view;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected Facade()
        {
            InitializeFacade();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void InitializeFacade()
        {
            InitializeModel();
            InitializeController();
            InitializeView();
        }

        /// <summary>
        /// 初始化Model
        /// </summary>
        protected virtual void InitializeModel()
        {
            model = Model.instance;
        }

        /// <summary>
        /// 初始化Controller
        /// </summary>
        protected virtual void InitializeController()
        {
            controller = Controller.instance;
        }

        /// <summary>
        /// 初始化View
        /// </summary>
        protected virtual void InitializeView()
        {
            view = View.instance;
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="notificationName">通知索引</param>
        /// <param name="body">通知实体</param>
        /// <param name="type">通知类型</param>
        public void SendNotification(string notificationName, object body = null, string type = null)
        {
            NotifyObservers(new Notification(notificationName, body, type));
        }

        /// <summary>
        /// 注册代理
        /// </summary>
        /// <param name="proxy">代理实体</param>
        public void RegisterProxy(IProxy proxy)
        {
            model.RegisterProxy(proxy);
        }

        /// <summary>
        /// 获取代理
        /// </summary>
        /// <param name="proxyName">代理索引</param>
        /// <returns>代理实体</returns>
        public IProxy RetrieveProxy(string proxyName)
        {
            return model.RetrieveProxy(proxyName);
        }

        /// <summary>
        /// 移除代理
        /// </summary>
        /// <param name="proxyName">代理索引</param>
        /// <returns>代理实体</returns>
        public IProxy RemoveProxy(string proxyName)
        {
            return model.RemoveProxy(proxyName);
        }

        /// <summary>
        /// 是否存在代理
        /// </summary>
        /// <param name="proxyName">代理索引</param>
        /// <returns>代理实体</returns>
        public bool HasProxy(string proxyName)
        {
            return model.HasProxy(proxyName);
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        /// <param name="notificationName">通知索引</param>
        /// <param name="commandClassRef">通知构造引用</param>
        public void RegisterCommand(string notificationName, Func<ICommand> commandClassRef)
        {
            controller.RegisterCommand(notificationName, commandClassRef);
        }

        /// <summary>
        /// 移除命令
        /// </summary>
        /// <param name="notificationName">命令索引</param>
        public void RemoveCommand(string notificationName)
        {
            controller.RemoveCommand(notificationName);
        }

        /// <summary>
        /// 是否存在命令
        /// </summary>
        /// <param name="notificationName">命令索引</param>
        /// <returns>查询结果</returns>
        public bool HasCommand(string notificationName)
        {
            return controller.HasCommand(notificationName);
        }

        /// <summary>
        /// 注册中介者
        /// </summary>
        /// <param name="mediator">中介者对象</param>
        public void RegisterMediator(IMediator mediator)
        {
            view.RegisterMediator(mediator);
        }

        /// <summary>
        /// 获取中介者实体
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <returns>中介者对象</returns>
        public IMediator RetrieveMediator(string mediatorName)
        {
            return view.RetrieveMediator(mediatorName);
        }

        /// <summary>
        /// 移除中介者对象
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <returns>中介者对象</returns>
        public IMediator RemoveMediator(string mediatorName)
        {
            return view.RemoveMediator(mediatorName);
        }

        /// <summary>
        /// 是否存在中介者
        /// </summary>
        /// <param name="mediatorName">中介者索引</param>
        /// <returns>查询结果</returns>
        public bool HasMediator(string mediatorName)
        {
            return view.HasMediator(mediatorName);
        }

        /// <summary>
        /// 通知观察者
        /// </summary>
        /// <param name="notification">发送的通知</param>
        public void NotifyObservers(INotification notification)
        {
            view.NotifyObservers(notification);
        }
    }
}