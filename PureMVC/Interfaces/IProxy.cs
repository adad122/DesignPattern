namespace PureMVC.Interfaces
{
    /// <summary>
    /// 代理对象接口，用于维护Model层的数据
    /// </summary>
    public interface IProxy : INotifier
    {
        /// <summary>
        /// 代理索引
        /// </summary>
        string ProxyName { get; }

        /// <summary>
        /// 代理数据
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// 代理被Model注册
        /// </summary>
        void OnRegister();

        /// <summary>
        /// 代理被Model移除
        /// </summary>
        void OnRemove();
    }
}