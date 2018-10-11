using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePatternApply
{
    public class Scene
    {
        public string Name;

        public bool IsRunning { get; protected set; }

        public Scene(string name)
        {
            Name = name;
        }

        public void Run()
        {
            Console.WriteLine("Scene " + Name + "Running.");
            IsRunning = true;
        }

        public void Stop()
        {
            Console.WriteLine("Scene " + Name + "Stoped.");
            IsRunning = false;
        }
    }
    public class Scheduler
    {
        public void Schedule()
        {
            Console.WriteLine("Scheduler:Schedule");
        }

        public void Unschedule()
        {
            Console.WriteLine("Scheduler:Unschedule");
        }
    }

    public class ActionManager
    {
        public void AddAction()
        {
            Console.WriteLine("ActionManager:AddAction");
        }

        public void RemoveAction()
        {
            Console.WriteLine("ActionManager:RemoveAction");
        }
    }

    public class EventDispatcher
    {
        public void AddEventListener()
        {
            Console.WriteLine("EventDispatcher:AddEventListener");
        }

        public void RemoveEventListener()
        {
            Console.WriteLine("EventDispatcher:RemoveEventListener");
        }
    }

    public class Director
    {
        protected Stack<Scene> Scenes = new Stack<Scene>();

        protected Scheduler Scheduler;
        protected ActionManager ActionManager;
        protected EventDispatcher EventDispatcher;

        public Director()
        {
            Scheduler = new Scheduler();
            ActionManager = new ActionManager();
            EventDispatcher = new EventDispatcher();
        }

        public Scene GetRunningScene()
        {
            if (Scenes.Count > 0)
            {
                return Scenes.Peek();
            }

            return null;
        }

        public void PushScene(Scene scene)
        {
            Scenes.Push(scene);
        }

        public void RunWithScene(Scene scene)
        {
            if (Scenes.Count > 0)
            {
                Scenes.Peek().Stop();
            }
            Scenes.Push(scene);
            Scenes.Peek().Run();
        }

        public void PopScene()
        {
            if (Scenes.Count > 0)
            {
                Scene scene = Scenes.Pop();
                if (scene.IsRunning)
                {
                    scene.Stop();
                }
            }
        }

        public Scheduler GetScheduler()
        {
            return Scheduler;
        }

        public void SetScheduler(Scheduler scheduler)
        {
            Scheduler = scheduler;
        }

        public ActionManager GetActionManager()
        {
            return ActionManager;
        }

        public void SetActionManager(ActionManager actionManager)
        {
            ActionManager = actionManager;
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

    public class DisplayLinkDirector : Director
    {
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            Director director = new DisplayLinkDirector();
            director.RunWithScene(new Scene("Login"));
            director.RunWithScene(new Scene("Loading"));
            director.RunWithScene(new Scene("MainCity"));
            director.GetEventDispatcher().AddEventListener();
            director.GetActionManager().AddAction();
            director.GetScheduler().Schedule();
        }
    }
}
