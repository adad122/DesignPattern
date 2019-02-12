namespace PureMVC.Interfaces
{
    /// <summary>
    /// Notification接口，定义了Notification的基本属性
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// 通知索引
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 通知数据体
        /// </summary>
        object Body { get;}

        /// <summary>
        /// 通知类型
        /// </summary>
        string Type { get;}

        /// <summary>
        /// 通知ToString对象
        /// </summary>
        /// <returns>ToString对象</returns>
        string ToString();
    }
}