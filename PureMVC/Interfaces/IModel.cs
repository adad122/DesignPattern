namespace PureMVC.Interfaces
{
    /// <summary>
    /// Controller对象接口，用于关联和管理Command和Notification
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// 注册代理对象
        /// </summary>
        /// <param name="proxy">代理实体</param>
        void RegisterProxy(IProxy proxy);

        /// <summary>
        /// 获得代理对象
        /// </summary>
        /// <param name="proxyName">代理识别标记</param>
        /// <returns>代理对象</returns>
        IProxy RetrieveProxy(string proxyName);

        /// <summary>
        /// 移除代理对象
        /// </summary>
        /// <param name="proxyName">代理识别标记</param>
        /// <returns>代理对象</returns>
        IProxy RemoveProxy(string proxyName);

        /// <summary>
        /// 是否存在代理对象
        /// </summary>
        /// <param name="proxyName">代理识别标记</param>
        /// <returns>查询结果</returns>
        bool HasProxy(string proxyName);
    }
}