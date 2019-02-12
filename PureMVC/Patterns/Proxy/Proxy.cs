using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;

namespace PureMVC.Patterns.Proxy
{
    /// <summary>
    /// 代理实体类，用于维护Model层的数据
    /// </summary>
    public class Proxy : Notifier, IProxy
    {
        /// <summary>
        /// 代理静态名字
        /// </summary>
        public static string NAME = "Proxy";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="proxyName">代理名字</param>
        /// <param name="data">代理数据</param>
        public Proxy(string proxyName, object data = null)
        {
            ProxyName = proxyName ?? Proxy.NAME;
            if (data != null) Data = data;
        }

        /// <summary>
        /// 代理被Model注册
        /// </summary>
        public virtual void OnRegister()
        {
        }

        /// <summary>
        /// 代理被Model移除
        /// </summary>
        public virtual void OnRemove()
        {
        }

        /// <summary>
        /// 代理名字
        /// </summary>
        public string ProxyName { get; protected set; }

        /// <summary>
        /// 代理数据
        /// </summary>
        public object Data { get; set; }
    }
}