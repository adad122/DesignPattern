using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternApply
{
    public enum ComponentType
    {
        None,
        GameObject,
        Rigidbody3D,
        BoxCollider,
        Sprite,
    }
    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;
    }
    public abstract class Component
    {
        public ComponentType ComponentType { get; protected set; }

        protected Component()
        {
            ComponentType = ComponentType.None;
        }
        protected Dictionary<ComponentType, Component> ComDictionary = new Dictionary<ComponentType, Component>();

        public Component GetComponent(ComponentType componentType)
        {
            if (ComDictionary.ContainsKey(componentType))
            {
                return ComDictionary[componentType];
            }

            return null;
        }

        public bool RemoveComponent(ComponentType type)
        {
            if (ComDictionary.ContainsKey(type))
            {
                ComDictionary.Remove(type);
                return true;
            }

            return false;
        }

        public Component AddComponent(Component mono)
        {
            if (!ComDictionary.ContainsKey(mono.ComponentType))
            {
                ComDictionary.Add(mono.ComponentType, mono);
            }

            return ComDictionary[mono.ComponentType];
        }
    }

    public abstract class MonoBehavier : Component
    {
        public virtual void Awake()
        {
            foreach (KeyValuePair<ComponentType, Component> monoBehavier in ComDictionary)
            {
                ((MonoBehavier)monoBehavier.Value).Awake();
            }
            OnEnable();
        }

        public virtual void OnEnable()
        {
        }

        public virtual void Start()
        {
            foreach (KeyValuePair<ComponentType, Component> monoBehavier in ComDictionary)
            {
                ((MonoBehavier)monoBehavier.Value).Start();
            }
        }

        public virtual void FixedUpdate()
        {
            OnCollisionEnter();
            OnCollisionStay();
            OnCollisionExit();
            foreach (KeyValuePair<ComponentType, Component> monoBehavier in ComDictionary)
            {
                ((MonoBehavier)monoBehavier.Value).FixedUpdate();
            }
        }

        public virtual void Update()
        {
            foreach (KeyValuePair<ComponentType, Component> monoBehavier in ComDictionary)
            {
                ((MonoBehavier)monoBehavier.Value).Update();
            }
        }

        public virtual void Rendering()
        {
            OnRenderImage();
            foreach (KeyValuePair<ComponentType, Component> monoBehavier in ComDictionary)
            {
                ((MonoBehavier)monoBehavier.Value).Rendering();
            }
        }

        public virtual void OnRenderImage()
        {
        }

        public virtual void LateUpdate()
        {
            foreach (KeyValuePair<ComponentType, Component> monoBehavier in ComDictionary)
            {
                ((MonoBehavier)monoBehavier.Value).LateUpdate();
            }
        }

        public virtual void OnDisable()
        {
        }

        public virtual void OnDestroy()
        {
        }


        public virtual void OnCollisionEnter()
        {
        }

        public virtual void OnCollisionExit()
        {
        }

        public virtual void OnCollisionStay()
        {
        }

        public virtual void Coroutine()
        {
            foreach (KeyValuePair<ComponentType, Component> monoBehavier in ComDictionary)
            {
                ((MonoBehavier)monoBehavier.Value).Coroutine();
            }
        }

        public virtual void Quitting()
        {
            foreach (KeyValuePair<ComponentType, Component> monoBehavier in ComDictionary)
            {
                ((MonoBehavier)monoBehavier.Value).Quitting();
            }
            OnDisable();
            OnDestroy();
        }
    }

    public class GameObject : MonoBehavier
    {
        public Vector3 Position;
        public GameObject()
        {
            ComponentType = ComponentType.GameObject;
        }

        public override void Awake()
        {
            base.Awake();
            Console.WriteLine("GameObject:Awake");
        }

        public override void Start()
        {
            base.Start();
            Console.WriteLine("GameObject:Start");
        }

        public override void OnEnable()
        {
            base.OnEnable();
            Console.WriteLine("GameObject:OnEnable");
        }

        public override void Update()
        {
            base.Update();
            Console.WriteLine("GameObject:Update");
        }

        public override void Quitting()
        {
            base.Quitting();
            Console.WriteLine("GameObject:Quitting");
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Console.WriteLine("GameObject:OnDisable");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Console.WriteLine("GameObject:OnDestroy");
        }
    }

    public class Rigidbody3D : MonoBehavier
    {
        public Rigidbody3D()
        {
            ComponentType = ComponentType.Rigidbody3D;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Console.WriteLine("Rigidbody:FixedUpdate");
        }

        public override void Awake()
        {
            base.Awake();
            Console.WriteLine("Rigidbody:Awake");
        }

        public override void Start()
        {
            base.Start();
            Console.WriteLine("Rigidbody:Start");
        }

        public override void OnEnable()
        {
            base.OnEnable();
            Console.WriteLine("Rigidbody:OnEnable");
        }

        public override void Update()
        {
            base.Update();
            Console.WriteLine("Rigidbody:Update");
        }

        public override void Quitting()
        {
            base.Quitting();
            Console.WriteLine("Rigidbody:Quitting");
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Console.WriteLine("Rigidbody:OnDisable");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Console.WriteLine("Rigidbody:OnDestroy");
        }
    }

    public class BoxCollider : MonoBehavier
    {
        public BoxCollider()
        {
            ComponentType = ComponentType.BoxCollider;
        }
        public override void OnCollisionEnter()
        {
            base.OnCollisionEnter();
            Console.WriteLine("BoxCollider:OnCollisionEnter");
        }

        public override void OnCollisionStay()
        {
            base.OnCollisionEnter();
            Console.WriteLine("BoxCollider:OnCollisionStay");
        }

        public override void OnCollisionExit()
        {
            base.OnCollisionEnter();
            Console.WriteLine("BoxCollider:OnCollisionExit");
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Console.WriteLine("BoxCollider:FixedUpdate");
        }

        public override void Awake()
        {
            base.Awake();
            Console.WriteLine("BoxCollider:Awake");
        }

        public override void Start()
        {
            base.Start();
            Console.WriteLine("BoxCollider:Start");
        }

        public override void OnEnable()
        {
            base.OnEnable();
            Console.WriteLine("BoxCollider:OnEnable");
        }

        public override void Update()
        {
            base.Update();
            Console.WriteLine("BoxCollider:Update");
        }

        public override void Quitting()
        {
            base.Quitting();
            Console.WriteLine("BoxCollider:Quitting");
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Console.WriteLine("BoxCollider:OnDisable");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Console.WriteLine("BoxCollider:OnDestroy");
        }
    }

    public class Sprite : MonoBehavier
    {
        protected string Name;

        public Sprite()
        {
            ComponentType = ComponentType.Sprite;
        }

        public override void OnRenderImage()
        {
            base.OnRenderImage();
            Console.WriteLine("Sprite:OnRenderImage");
        }

        public void SetSprite(string name)
        {
            Name = name;
        }

        public string GetSprite()
        {
            return Name;
        }

        public override void Awake()
        {
            base.Awake();
            Console.WriteLine("Sprite:Awake");
        }

        public override void Start()
        {
            base.Start();
            Console.WriteLine("Sprite:Start");
        }

        public override void OnEnable()
        {
            base.OnEnable();
            Console.WriteLine("Sprite:OnEnable");
        }

        public override void Update()
        {
            base.Update();
            Console.WriteLine("Sprite:Update");
        }

        public override void Quitting()
        {
            base.Quitting();
            Console.WriteLine("Sprite:Quitting");
        }

        public override void OnDisable()
        {
            base.OnDisable();
            Console.WriteLine("Sprite:OnDisable");
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Console.WriteLine("Sprite:OnDestroy");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MonoBehavier mono = new GameObject();
            mono.AddComponent(new BoxCollider());
            mono.AddComponent(new Rigidbody3D());
            mono.AddComponent(new Sprite());

            ((Sprite)mono.GetComponent(ComponentType.Sprite)).SetSprite("Empire.png");
            Console.WriteLine(((Sprite)mono.GetComponent(ComponentType.Sprite)).GetSprite());

            mono.Awake();
            mono.Start();
            mono.FixedUpdate();
            mono.Update();
            mono.LateUpdate();
            mono.Coroutine();
            mono.Rendering();
            mono.Quitting();

            Console.WriteLine("-----------------------------------------------------");
            mono.RemoveComponent(ComponentType.BoxCollider);
            mono.RemoveComponent(ComponentType.Rigidbody3D);

            mono.Awake();
            mono.Start();
            mono.FixedUpdate();
            mono.Update();
            mono.LateUpdate();
            mono.Coroutine();
            mono.Rendering();
            mono.Quitting();
        }
    }
}
