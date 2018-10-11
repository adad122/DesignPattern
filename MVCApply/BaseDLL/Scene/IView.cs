namespace MVCApply.BaseDLL.Scene
{
    public interface IView
    {
        void OnLoadStart();
        void OnLoadCompleted();
        void OnEnable();
        void OnDisable();
        void OnDestroy();
        void OnEnterStackTop();
        void OnExitStackTop();

    }
}
