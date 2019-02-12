using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using PureMVC.Patterns.Singleton;

namespace PureMVC.Core
{
    /// <summary>
    /// Controller对象实体，用于关联和管理Command和Notification
    /// </summary>
    public class Controller : SingletonEx<Controller>, IController
    {
        /// <summary>
        /// 命令集合
        /// </summary>
        protected IDictionary<string, Func<ICommand>> commandMap;

        /// <summary>
        /// View单例引用
        /// </summary>
        protected IView view;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected Controller()
        {
            commandMap = new Dictionary<string, Func<ICommand>>();
            InitializeController();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void InitializeController()
        {
            view = View.instance;
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        /// <param name="notificationName">通知索引</param>
        /// <param name="commandClassRef">通知构造引用</param>
        public void RegisterCommand(string notificationName, Func<ICommand> commandClassRef)
        {
            if (!commandMap.ContainsKey(notificationName))
            {
                view.RegisterObserver(notificationName, new Observer(ExecuteCommand, this));
                commandMap.Add(notificationName, commandClassRef);
            }
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="notification">通知实体</param>
        public void ExecuteCommand(INotification notification)
        {
            if (commandMap.ContainsKey(notification.Name))
            {
                ICommand commandInstance = commandMap[notification.Name]();
                commandInstance.Execute(notification);
            }
        }

        /// <summary>
        /// 移除命令
        /// </summary>
        /// <param name="notificationName">命令索引</param>
        public void RemoveCommand(string notificationName)
        {
            if (commandMap.ContainsKey(notificationName))
            {
                commandMap.Remove(notificationName);
            }
        }

        /// <summary>
        /// 是否存在命令
        /// </summary>
        /// <param name="notificationName">命令索引</param>
        /// <returns>查询结果</returns>
        public bool HasCommand(string notificationName)
        {
            return commandMap.ContainsKey(notificationName);
        }
    }
}