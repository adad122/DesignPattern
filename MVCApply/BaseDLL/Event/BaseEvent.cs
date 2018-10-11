using System;
using MVCApply.BaseDLL.Templates;

namespace MVCApply.BaseDLL.Event
{
    public class BaseEvent : IFactoryObj
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public Int32 EventId { get; set; }

        /// <summary>
        /// 事件参数
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 默认构造
        /// </summary>
        public BaseEvent()
        {
            EventId = -1;
            Data = null;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        public BaseEvent(Int32 id, object data = null)
        {
            EventId = id;
            Data = data;
        }

        /// <summary>
        /// 实现回收接口
        /// </summary>
        public void Recycle()
        {
            EventId = -1;
            Data = null;
        }
    }
}
