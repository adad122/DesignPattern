using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternApply2
{
    public abstract class UObject
    {
        public string name;

        public bool active;

        public static void Destroy(UObject obj)
        {

        }
    }
    public abstract class Component : UObject
    {

        public GameObject GameObject;

        public Rigidbody rigidbody
        {
            get { return GetComponent<Rigidbody>() == null ? null : GetComponent<Rigidbody>() as Rigidbody; }
        }

        public Collider boxCollider
        {
            get { return GetComponent<Collider>() == null ? null : GetComponent<Collider>() as Collider; }
        }

        public Transform transform
        {
            get { return GetComponent<Transform>() == null ? null : GetComponent<Transform>() as Transform; }
        }


        public Component GetComponent<T>()
        {
            if (GameObject != null)
                return GameObject.GetComponent<T>();

            return null;
        }
    }

    public class GameObject : UObject
    {
        protected Dictionary<Type, UObject> _comDictionary = new Dictionary<Type, UObject>();

        private bool _isActive;

        public bool activeSelf
        {
            get { return _isActive; }
        }

        public GameObject(string name)
        {
            this.name = name;
        }

        protected Dictionary<Type, UObject> ComDictionary
        {
            get { return _comDictionary; }
        }

        public Component AddComponent<T>()
        {
            Type t = typeof(T);

            if (ComDictionary.ContainsKey(t))
                return ComDictionary[t] as Component;

            Assembly asm = Assembly.GetExecutingAssembly();
            Component obj = asm.CreateInstance(t.ToString()) as Component;

            if (obj != null)
            {
                obj.GameObject = this;
                ComDictionary.Add(t, obj);
                return obj;
            }

            return null;
        }
        public Component GetComponent<T>()
        {
            Type t = typeof(T);

            if (ComDictionary.ContainsKey(t))
            {
                return ComDictionary[t] as Component;
            }

            return null;
        }

        public new static void Destroy(UObject obj)
        {
            Component component = obj as Component;
            if (component != null && component.GameObject != null && component.GameObject.ComDictionary.ContainsKey(obj.GetType()))
            {
                component.GameObject.ComDictionary.Remove(component.GetType());
            }
        }

        public Transform transform
        {
            get { return GetComponent<Transform>() == null ? null : GetComponent<Transform>() as Transform; }
        }

        public void SetActive(bool active)
        {
            _isActive = active;
        }
    }

    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 one = new Vector3(1.0f, 1.0f, 1.0f);
        public static Vector3 zero = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public class Transform : Component
    {
        public Vector3 position = Vector3.zero;
        public Vector3 rotation = Vector3.zero;
        public Vector3 localScale = Vector3.one;
    }

    public class Collision
    {

    }

    public abstract class Behaviour : Component
    {
        public bool enabled;

        public bool isActiveAndEnabled
        {
            get { return enabled && active; }
        }
    }

    public abstract class MonoBehaviour : Behaviour
    {
        void Awake()
        {
            OnEnable();
        }

        void Start()
        {
        }

        void FixedUpdate()
        {
        }

        void Update()
        {
        }

        void LateUpdate()
        {
        }

        void Coroutine()
        {

        }

        void Rendering()
        {
        }

        void Quitting()
        {
            OnDestroy();
        }

        void OnEnable()
        {
        }

        void OnDisable()
        {
        }

        void OnDestroy()
        {
            OnApplicationQuit();
        }

        void OnApplicationQuit()
        {
            OnDisable();
        }


        void OnCollisionEnter(Collision collision)
        {
        }

        void OnCollisionExit(Collision collision)
        {
        }

        void OnCollisionStay(Collision collision)
        {
        }

        void OnTriggerEnter(Collider other)
        {
        }

        void OnTriggerStay(Collider other)
        {
        }

        void OnTriggerExit(Collider other)
        {
        }
    }

    public class Rigidbody : Component
    {
        public void OnCollisionEnter(Collision collision)
        {
            Console.WriteLine("Rigidbody:OnCollisionEnter");
        }

        public void OnCollisionStay(Collision collision)
        {
            Console.WriteLine("Rigidbody:OnCollisionStay");
        }

        public void OnCollisionExit(Collision collision)
        {
            Console.WriteLine("Rigidbody:OnCollisionExit");
        }
    }

    public class Collider : Component
    {
        public bool isTrigger;
        public void OnCollisionEnter(Collision collision)
        {
            Console.WriteLine("BoxCollider:OnCollisionEnter");
        }

        public void OnCollisionStay(Collision collision)
        {
            Console.WriteLine("BoxCollider:OnCollisionStay");
        }

        public void OnCollisionExit(Collision collision)
        {
            Console.WriteLine("BoxCollider:OnCollisionExit");
        }

        public void OnTriggerEnter(Collider other)
        {
            Console.WriteLine("BoxCollider:OnTriggerEnter");
        }

        public void OnTriggerStay(Collider other)
        {
            Console.WriteLine("BoxCollider:OnTriggerStay");
        }

        public void OnTriggerExit(Collider other)
        {
            Console.WriteLine("BoxCollider:OnTriggerExit");
        }
    }

    public class EmptyObject : GameObject
    {
        public EmptyObject(string name)
            : base(name)
        {
            AddComponent<Transform>();
        }
    }

    public class TestMono : MonoBehaviour
    {
        public void PrintDatas()
        {
            Console.WriteLine("TestMono");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EmptyObject emptyObject = new EmptyObject("EmptyObject");
            emptyObject.AddComponent<Rigidbody>();
            emptyObject.AddComponent<Collider>();

            TestMono mono = emptyObject.AddComponent<TestMono>() as TestMono;
            if (mono != null)
            {
                mono.PrintDatas();
            }

            Transform transform = emptyObject.GetComponent<Transform>() as Transform;

            if (transform != null)
            {
                Console.WriteLine("position.x = " + transform.position.x);
                Console.WriteLine("position.y = " + transform.position.y);
                Console.WriteLine("position.z = " + transform.position.z);
                Console.WriteLine("-------------------------------------------");

                transform.position.x += 1.0f;
                transform.position.y += 2.0f;
                transform.position.z += 3.0f;

                Console.WriteLine("position.x = " + transform.position.x);
                Console.WriteLine("position.y = " + transform.position.y);
                Console.WriteLine("position.z = " + transform.position.z);
                Console.WriteLine("-------------------------------------------");

                TestMono mono2 = transform.GetComponent<TestMono>() as TestMono;
                if (mono2 != null)
                {
                    mono2.PrintDatas();
                }
                Console.WriteLine("-------------------------------------------");
            }

            GameObject.Destroy(mono);
            TestMono mono1 = emptyObject.GetComponent<TestMono>() as TestMono;
            if (mono1 != null)
            {
                mono1.PrintDatas();
            }
        }
    }
}
