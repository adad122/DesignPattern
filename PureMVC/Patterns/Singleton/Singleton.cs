using System;
using System.Collections;
using System.Reflection;

namespace PureMVC.Patterns.Singleton
{
    /// <summary>
    /// 单例模式泛型
    /// </summary>
    public static class Singleton
    {
        private static Hashtable _hTable = new Hashtable();
        private static object SyncRoot = new object();

        /// <summary>
        /// 获取实例 获取强类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>实例</returns>
        public static T GetInstance<T>() where T : class
        {
            return GetInstance(typeof(T)) as T;
        }

        /// <summary>
        /// 获取强类型的实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetInstance(Type type)
        {
            if (_hTable.ContainsKey(type.FullName))
            {
                //返回已有实例
                return _hTable[type.FullName];
            }
            else
            {
                //构造唯一实例
                ConstructorInfo[] infos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                ConstructorInfo constructorInfo = null;
                foreach (ConstructorInfo info in infos)
                {
                    if (info.GetParameters().Length == 0)
                    {
                        constructorInfo = info;
                        break;
                    }
                }
                if (constructorInfo == null)
                {
                    throw new NotSupportedException("没有无参构造函数");
                }

                object instance = constructorInfo.Invoke(null);

                lock (SyncRoot)
                {
                    _hTable.Add(type.FullName, instance);
                }

                return instance;
            }
        }
    }

    public class SingletonEx<T> where T : class
    {
        public static T instance
        {
            get
            {
                return Singleton.GetInstance<T>();
            }
        }
    }
}