using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Singleton;

namespace PureMVC.Core
{
    /// <summary>
    /// Controller对象实体，用于关联和管理Command和Notification
    /// </summary>
    public class Controller : SingletonEx<IController>, IController
    {
        protected IDictionary<string, Func<ICommand>> commandMap;

        protected IView view;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected Controller()
        {
            InitializeController();
        }

        protected virtual void InitializeController()
        {
        }

        public void RegisterCommand(string notificationName, Func<ICommand> commandClassRef)
        {
        }

        public void ExecuteCommand(INotification notification)
        {
        }

        public void RemoveCommand(string notificationName)
        {
        }

        public bool HasCommand(string notificationName)
        {
        }
    }
}