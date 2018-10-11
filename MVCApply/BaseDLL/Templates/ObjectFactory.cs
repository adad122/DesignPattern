using System.Collections.Generic;

namespace MVCApply.BaseDLL.Templates
{
    /// <summary>
    /// 工厂对象必须实现的接口
    /// </summary>
    public interface IFactoryObj
    {
        void Recycle();
    }

    /// <summary>
    /// 对象工厂模版（用于减少频繁的new操作）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectFactory<T> where T : IFactoryObj, new()
    {

        protected static Stack<T> MFreeList = new Stack<T>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="size"></param>
        public static void Initialize(int size)
        {
            for (int a = 0; a < size; ++a)
            {
                MFreeList.Push(new T());
            }
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <returns></returns>
        public static T Create()
        {
            if (MFreeList.Count <= 0)
            {
                for (int a = 0; a < DefaultSize; ++a)
                {
                    MFreeList.Push(new T());
                }
            }

            if (MFreeList.Count <= 0)
            {
                return default(T);
            }

            return MFreeList.Pop();
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj"></param>
        public static void Recycle(T obj)
        {
            obj.Recycle();
            MFreeList.Push(obj);
        }

        /// <summary>
        /// 默认大小
        /// </summary>
        private const int DefaultSize = 10;
    }
}
