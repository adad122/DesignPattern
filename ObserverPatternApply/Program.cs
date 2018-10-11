using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPatternApply
{
    public delegate void Event(string msg);

    public abstract class Ref
    {
        
    }

    public class Node : Ref
    {
        protected EventDispatcher EventDispatcher;

        public Node()
        {
            EventDispatcher = Director.GetInstance().GetEventDispatcher();
        }

        public EventDispatcher GetEventDispatcher()
        {
            return EventDispatcher;
        }

        public void SetEventDispatcher(EventDispatcher eventDispatcher)
        {
            EventDispatcher = eventDispatcher;
        }
    }

    public class EventListener : Ref
    {
        public string Name;

        public Event Callback;

        protected Node AssociatedNode;

        public EventListener(Event callback)
        {
            Name = "";
            Callback = callback;
        }

        public Node GetAssociatedNode()
        {
            return AssociatedNode;
        }

        public void SetAssociatedNode(Node node)
        {
            AssociatedNode = node;
        }
    }

    public class EventListenerCustom : EventListener
    {
        public EventListenerCustom(string name, Event callback)
            : base(callback)
        {
            Name = name;
        }
    }

    public class EventDispatcher : Ref
    {
        private readonly List<EventListener> _toAddedListeners = new List<EventListener>();

        private readonly Dictionary<Node, List<EventListener>> _nodeListenersMap = new Dictionary<Node, List<EventListener>>(); 

        public void RemoveCustomEventListeners(string name)
        {
            List<EventListener> dirtyListeners = new List<EventListener>();
            foreach (EventListener addedListener in _toAddedListeners)
            {
                if (addedListener.Name.Equals(name))
                {
                    dirtyListeners.Add(addedListener);
                }
            }

            foreach (EventListener dirtyListener in dirtyListeners)
            {
                _toAddedListeners.Remove(dirtyListener);
            }
        }

        public EventListenerCustom AddCustomEventListener(string name, Event callback)
        {
            EventListenerCustom eventListener = new EventListenerCustom(name, callback);
            AddEventListener(eventListener);
            return eventListener;
        }

        public void AddEventListener(EventListener eventListener)
        {
            _toAddedListeners.Add(eventListener);
        }

        public void DispatchCustomEvent(string name, string data)
        {
            foreach (EventListener eventListener in _toAddedListeners)
            {
                if (eventListener.GetType() == typeof (EventListenerCustom) && 
                    eventListener.Name.Equals(name) && 
                    eventListener.Callback != null)
                {
                    eventListener.Callback(data);
                }
            }
        }

        public void RemoveEventListenersForTarget(Node node, bool recursive = false)
        {
            if (_nodeListenersMap.ContainsKey(node))
            {
                _nodeListenersMap.Clear();
                _nodeListenersMap.Remove(node);
            }
        }

        public void RemoveEventListener(EventListener listener)
        {
            foreach (EventListener addedListener in _toAddedListeners)
            {
                if (addedListener == listener)
                {
                    _toAddedListeners.Remove(listener);
                    return;
                }
            }
        }
    }

    public class Director : Ref
    {
        protected EventDispatcher EventDispatcher;
        private static Director _mInstance;

        public static Director GetInstance()
        {
            return _mInstance ?? (_mInstance = new Director());
        }

        private Director()
        {
            EventDispatcher = new EventDispatcher();
        }

        public EventDispatcher GetEventDispatcher()
        {
            return EventDispatcher;
        }

        public void SetEventDispatcher(EventDispatcher eventDispatcher)
        {
            EventDispatcher = eventDispatcher;
        }
    }

    class Program
    {
        static EventListener RegistCustomEvent(string name, Event callback)
        {
            EventDispatcher eventDispatcher = Director.GetInstance().GetEventDispatcher();
            return eventDispatcher.AddCustomEventListener(name, callback);
        }

        static void RemoveCustomEvent(string name)
        {
            EventDispatcher eventDispatcher = Director.GetInstance().GetEventDispatcher();
            eventDispatcher.RemoveCustomEventListeners(name);
        }

        static void RemoveCustomEvent(EventListener eventListener)
        {
            EventDispatcher eventDispatcher = Director.GetInstance().GetEventDispatcher();
            eventDispatcher.RemoveEventListener(eventListener);
        }

        static void DispatchCustomEvent(string name, string msg)
        {
            EventDispatcher eventDispatcher = Director.GetInstance().GetEventDispatcher();
            eventDispatcher.DispatchCustomEvent(name, msg);
        }

        public class View : Node
        {
            protected string Name;

            public View(string name)
            {
                Name = name;
            }

            public virtual void Init()
            {
                
            }

            public void Close()
            {
                OnExit();
            }

            protected virtual void OnExit()
            {
                EventDispatcher.RemoveEventListenersForTarget(this);
            }
        }

        public class UserDataView : View
        {
            public EventListener UpdateEvent;
            public UserDataView() 
                : base("UserDataView")
            {
            }

            public override void Init()
            {
                base.Init();

                UpdateEvent = RegistCustomEvent("Update", delegate(string msg)
                {
                    Console.WriteLine(Name + " UpdateEvent " + msg);
                });
            }

            protected override void OnExit()
            {
                RemoveCustomEvent(UpdateEvent);
                base.OnExit();
            }
        }

        public class CityView : View
        {
            public EventListener UpdateEvent;

            public EventListener BuildEvent;
            public CityView()
                : base("CityView")
            {
            }

            public override void Init()
            {
                base.Init();
                UpdateEvent = RegistCustomEvent("Update", delegate(string msg)
                {
                    Console.WriteLine(Name + " UpdateEvent " + msg);
                });

                BuildEvent = RegistCustomEvent("Build", delegate(string msg)
                {
                    Console.WriteLine(Name + " BuildEvent " + msg);
                });
            }

            protected override void OnExit()
            {
                RemoveCustomEvent(UpdateEvent);
                RemoveCustomEvent(BuildEvent);
                base.OnExit();
            }
        }

        public class ResourceView : View
        {
            public EventListener UpdateEvent;

            public ResourceView()
                : base("ResourceView")
            {
            }

            public override void Init()
            {
                base.Init();
                UpdateEvent = RegistCustomEvent("Update", delegate(string msg)
                {
                    Console.WriteLine(Name + " UpdateEvent " + msg);
                });
            }

            protected override void OnExit()
            {
                RemoveCustomEvent(UpdateEvent);
                base.OnExit();
            }
        }

        static void Main(string[] args)
        {
            Director director = Director.GetInstance();

            View cityView = new CityView();
            cityView.Init();

            Console.WriteLine("------------------------------------------------------------");
            DispatchCustomEvent("Login", "XXX登陆了");
            DispatchCustomEvent("Update", "XXX升级了");

            View userDataView = new UserDataView();
            userDataView.Init();

            Console.WriteLine("------------------------------------------------------------");
            DispatchCustomEvent("Update", "XXX升级了");
            DispatchCustomEvent("Build", "XXX建筑开始建造了");

            View resourceView = new ResourceView();
            resourceView.Init();

            Console.WriteLine("------------------------------------------------------------");
            DispatchCustomEvent("Update", "XXX升级了");
            DispatchCustomEvent("Build", "XXX建筑开始建造了");

            Console.WriteLine("------------------------------------------------------------");
            resourceView.Close();
            DispatchCustomEvent("Update", "XXX升级了");

            Console.WriteLine("------------------------------------------------------------");
            RemoveCustomEvent("Update");
            DispatchCustomEvent("Update", "XXX升级了");
        }
    }
}
