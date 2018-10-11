using System;
using System.Collections.Generic;
using System.Reflection;
using MVCApply.BaseDLL.Event;
using MVCApply.Plugin.Common;
using SingletonPatternApply;

namespace MVCApply.BaseDLL.Scene
{
    /// <summary>
    /// 抽象控制器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Controller<T> : SingletonEx<T>, IDisposable where T : class,new()
    {
        protected Controller()
        {
            Init();
        }

        public void Init()
        {
            InitController();
        }

        /// <summary>
        /// 初始化Controller模板方法
        /// </summary>
        public virtual void InitController()
        {
        }

        /// <summary>
        /// 对象被回收时，取消消息处理函数 IDispose接口函数，由GC调用
        /// </summary>
        public void Dispose()
        {
        }
    }
}
