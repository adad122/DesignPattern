using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrototypePatternApply
{
    class Program
    {
        [Serializable]
        public class SoilderObject
        {
            protected string Name;
            protected string Model;
            public SoilderObject(string name, string model)
            {
                InitDefData(name);
                LoadModel(model);
            }

            /// <summary>
            /// 模拟从数据库初始化的过程，大概需要0.005秒
            /// </summary>
            public void InitDefData(string name)
            {
                Thread.Sleep(5);
                Name = name;
            }

            /// <summary>
            /// 模拟模型实体化的过程，大概需要0.01秒
            /// </summary>
            public void LoadModel(string model)
            {
                Thread.Sleep(100);
                Model = model;
            }

            /// <summary>
            /// 深度拷贝
            /// </summary>
            /// <returns>拷贝对象</returns>
            public SoilderObject Copy()
            {
                MemoryStream stream = new MemoryStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                return formatter.Deserialize(stream) as SoilderObject;
            }
        }

        public class SoilderFactory
        {
            private Dictionary<string, SoilderObject>  _soilderObjects = new Dictionary<string, SoilderObject>();

            public SoilderFactory()
            {
                LoadCache();    
            }

            private void LoadCache()
            {
                SoilderObject soilderObject = new SoilderObject("Saber", "Saber");
                _soilderObjects.Add("Saber", soilderObject);
            }

            public SoilderObject CreateSoilderObject(string type)
            {
                if (type.Equals("Saber"))
                {
                    return new SoilderObject("Saber", "Saber");;
                }

                return null;
            }

            public SoilderObject CreateSoilderObjectByProtoType(string type)
            {
                if (type.Equals("Saber"))
                {
                    return _soilderObjects[type].Copy();
                }

                return null;
            }
        }

        public static long GetTimestamp()
        {
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);//ToUniversalTime()转换为标准时区的时间,去掉的话直接就用北京时间
            return (long)ts.TotalMilliseconds; //精确到毫秒
            //return (long)ts.TotalSeconds;//获取10位
        }

        static void Main(string[] args)
        {
            SoilderFactory soilderFactory = new SoilderFactory();
            long time = GetTimestamp();

            List<SoilderObject> soilderObjects = new List<SoilderObject>();

            for (int i = 0; i < 50; i++)
            {
                soilderObjects.Add(soilderFactory.CreateSoilderObject("Saber"));
            }

            Console.WriteLine("普通方式创建时间消耗: " + (GetTimestamp() - time));

            time = GetTimestamp();

            for (int i = 0; i < 50; i++)
            {
                soilderObjects.Add(soilderFactory.CreateSoilderObjectByProtoType("Saber"));
            }

            Console.WriteLine("原型方式创建时间消耗: " + (GetTimestamp() - time));
        }
    }
}
