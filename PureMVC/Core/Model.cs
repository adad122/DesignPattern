using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Singleton;

namespace PureMVC.Core
{
    /// <summary>
    /// Model对象实体，用于关联和管理Proxy
    /// </summary>
    public class Model : SingletonEx<Model>, IModel
    {
        /// <summary>
        /// 代理集合
        /// </summary>
        protected IDictionary<string, IProxy> proxyMap;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected Model()
        {
            proxyMap = new Dictionary<string, IProxy>();
            InitializeModel();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void InitializeModel()
        {
        }

        /// <summary>
        /// 注册代理
        /// </summary>
        /// <param name="proxy">代理实体</param>
        public void RegisterProxy(IProxy proxy)
        {
            if (!proxyMap.ContainsKey(proxy.ProxyName))
            {
                proxyMap.Add(proxy.ProxyName, proxy);
                proxy.OnRegister();
            }
        }

        /// <summary>
        /// 获取代理
        /// </summary>
        /// <param name="proxyName">代理索引</param>
        /// <returns>代理实体</returns>
        public IProxy RetrieveProxy(string proxyName)
        {
            if (proxyMap.ContainsKey(proxyName))
            {
                return proxyMap[proxyName];
            }

            return null;
        }

        /// <summary>
        /// 移除代理
        /// </summary>
        /// <param name="proxyName">代理索引</param>
        /// <returns>代理实体</returns>
        public IProxy RemoveProxy(string proxyName)
        {
            if (proxyMap.ContainsKey(proxyName))
            {
                IProxy proxy = proxyMap[proxyName];
                proxyMap.Remove(proxyName);
                return proxy;
            }

            return null;
        }

        /// <summary>
        /// 是否存在代理
        /// </summary>
        /// <param name="proxyName">代理索引</param>
        /// <returns>代理实体</returns>
        public bool HasProxy(string proxyName)
        {
            return proxyMap.ContainsKey(proxyName);
        }
    }
}