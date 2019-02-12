using System;
using PureMVC.Interfaces;

namespace PureMVC.Patterns.Facade
{
    /// <summary>
    /// Facade对象实体，用于统一关联和管理View,Controller，Model的方法
    /// </summary>
    public class Facade : IFacade
    {
        protected IController controller;

        protected IModel model;

        protected IView view;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Facade()
        {
            InitializeFacade();
        }

        protected virtual void InitializeFacade()
        {
            //InitializeModel();
            //InitializeController();
            //InitializeView();
        }

        public void SendNotification(string notificationName, object body = null, string type = null)
        {
        }

        public void RegisterProxy(IProxy proxy)
        {
        }

        public IProxy RetrieveProxy(string proxyName)
        {
        }

        public IProxy RemoveProxy(string proxyName)
        {
        }

        public bool HasProxy(string proxyName)
        {
        }

        public void RegisterCommand(string notificationName, Func<ICommand> commandClassRef)
        {
        }

        public void RemoveCommand(string notificationName)
        {
        }

        public bool HasCommand(string notificationName)
        {
        }

        public void RegisterMediator(IMediator mediator)
        {
        }

        public IMediator RetrieveMediator(string mediatorName)
        {
        }

        public IMediator RemoveMediator(string mediatorName)
        {
        }

        public bool HasMediator(string mediatorName)
        {
        }

        public void NotifyObservers(INotification notification)
        {
        }
    }
}