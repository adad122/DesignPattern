using CompositePatternApply2;
using SingletonPatternApply;

namespace MVCApply.BaseDLL.Scene
{
    public abstract class AbstractView<T> : SingletonEx<T>, IView, IRootObject where T : class, new()
    {
        //显式实现接口，保证接口函数只能被接口调用
        void IView.OnLoadStart()
        {
            OnLoadStart();
        }

        void IView.OnLoadCompleted()
        {
            OnLoadCompleted();
        }

        void IView.OnDestroy()
        {
            OnDestroy();
        }

        void IView.OnEnable()
        {
            OnEnable();
        }

        void IView.OnDisable()
        {
            OnDisable();
        }

        void IView.OnEnterStackTop()
        {
            OnEnterStackTop();
        }

        void IView.OnExitStackTop()
        {
            OnExitStackTop();
        }

        //在视图关联资源开始加载时调用
        protected virtual void OnLoadStart() { }
        //在视图关联资源加载完成时调用
        protected virtual void OnLoadCompleted() { }
        //视图资源及游戏对象(窗口、场景)被摧毁后调用，派生类可重写该函数释放不受场景管理的资源(手动调用Generator创建的资源)
        protected virtual void OnDestroy() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        public virtual void OnEnterStackTop() { }
        protected virtual void OnExitStackTop() { }

        public GameObject RootObject { get; set; }

        //显示视图
        public void Show()
        {
            if (RootObject != null)
            {
                if (!RootObject.activeSelf)
                    RootObject.SetActive(true);

                RootObject.transform.localScale = Vector3.one;
            }
        }

        //隐藏视图
        public void Hidden()
        {
            if (RootObject != null)
            {
                RootObject.transform.localScale = Vector3.zero;
            }
        }
    }
}
