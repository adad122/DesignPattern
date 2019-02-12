using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Command
{
    /// <summary>
    /// 简单命令结构，用于处理无序的单条命令
    /// </summary>
    public class SimpleCommand : Notifier, ICommand
    {
        /// <summary>
        /// 执行对应的命令
        /// </summary>
        /// <param name="notification">通知实体</param>
        public virtual void Execute(INotification notification)
        {
        }
    }
}