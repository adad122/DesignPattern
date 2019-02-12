using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Command
{
    /// <summary>
    /// 批量顺序命令，需要执行有序的批量命令时使用
    /// </summary>
    public class MacroCommand : Notifier, ICommand
    {
        /// <summary>
        /// 子命令队列
        /// </summary>
        public Queue<Func<ICommand>> subcommands;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MacroCommand()
        {
            subcommands = new Queue<Func<ICommand>>();
            InitializeMacroCommand();
        }

        /// <summary>
        /// 初始化函数，一般用于AddSubCommand的调用
        /// </summary>
        protected virtual void InitializeMacroCommand()
        {
        }

        /// <summary>
        /// 添加子命令到子命令队列
        /// </summary>
        /// <param name="commandClassRef">命令的构造函数引用</param>
        protected void AddSubCommand(Func<ICommand> commandClassRef)
        {
            subcommands.Enqueue(commandClassRef);
        }

        /// <summary>
        /// 执行对应的子命令
        /// </summary>
        /// <param name="notification">通知实体</param>
        public virtual void Execute(INotification notification)
        {
            while (subcommands.Count > 0)
            {
                Func<ICommand> commandClassRef = subcommands.Dequeue();
                ICommand commandInstance = commandClassRef();
                commandInstance.Execute(notification);
            }
        }
    }
}