#region 原型模式
//原型模式（Prototype Pattern）是用于创建重复的对象，同时又能保证性能。这种类型的设计模式属于创建型模式，它提供了一种创建对象的最佳方式。

//这种模式是实现了一个原型接口，该接口用于创建当前对象的克隆。当直接创建对象的代价比较大时，则采用这种模式。例如，一个对象需要在一个高代价的数据库操作之后被创建。我们可以缓存该对象，在下一个请求时返回它的克隆，在需要的时候更新数据库，以此来减少数据库调用。

//介绍
//意图：用原型实例指定创建对象的种类，并且通过拷贝这些原型创建新的对象。

//主要解决：在运行期建立和删除原型。

//何时使用： 1、当一个系统应该独立于它的产品创建，构成和表示时。 2、当要实例化的类是在运行时刻指定时，例如，通过动态装载。 3、为了避免创建一个与产品类层次平行的工厂类层次时。 4、当一个类的实例只能有几个不同状态组合中的一种时。建立相应数目的原型并克隆它们可能比每次用合适的状态手工实例化该类更方便一些。

//如何解决：利用已有的一个原型对象，快速地生成和原型对象一样的实例。

//关键代码： 1、实现克隆操作，在 JAVA 继承 Cloneable，重写 clone()，在 .NET 中可以使用 Object 类的 MemberwiseClone() 方法来实现对象的浅拷贝或通过序列化的方式来实现深拷贝。 2、原型模式同样用于隔离类对象的使用者和具体类型（易变类）之间的耦合关系，它同样要求这些"易变类"拥有稳定的接口。

//应用实例： 1、细胞分裂。 2、JAVA 中的 Object clone() 方法。

//优点： 1、性能提高。 2、逃避构造函数的约束。

//缺点： 1、配备克隆方法需要对类的功能进行通盘考虑，这对于全新的类不是很难，但对于已有的类不一定很容易，特别当一个类引用不支持串行化的间接对象，或者引用含有循环结构的时候。 2、必须实现 Cloneable 接口。

//使用场景： 1、资源优化场景。 2、类初始化需要消化非常多的资源，这个资源包括数据、硬件资源等。 3、性能和安全要求的场景。 4、通过 new 产生一个对象需要非常繁琐的数据准备或访问权限，则可以使用原型模式。 5、一个对象多个修改者的场景。 6、一个对象需要提供给其他对象访问，而且各个调用者可能都需要修改其值时，可以考虑使用原型模式拷贝多个对象供调用者使用。 7、在实际项目中，原型模式很少单独出现，一般是和工厂方法模式一起出现，通过 clone 的方法创建一个对象，然后由工厂方法提供给调用者。原型模式已经与 Java 融为浑然一体，大家可以随手拿来使用。

//注意事项：与通过对一个类进行实例化来构造新对象不同的是，原型模式是通过拷贝一个现有对象生成新对象的。浅拷贝实现 Cloneable，重写，深拷贝是通过实现 Serializable 读取二进制流。
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{
    [Serializable]
    public abstract class Shape : Object
    {
        private String _id;
        protected String ShapeType;
        public int[] Arr = {1, 2, 3};

        public abstract void Draw();

        public String GetShapeType()
        {
            return ShapeType;
        }

        public String GetId()
        {
            return _id;
        }

        public void SetId(String id)
        {
            this._id = id;
        }

        public Shape ShallowCopy()
        {
            Object clone = null;
            try
            {
                clone = MemberwiseClone();
            }
            catch (Exception e)
            {
                Console.WriteLine("ErrorCode: " + e.HResult + "Message: " + e.Message);
                throw;
            }

            return clone as Shape;
        }

        public Shape DeepCopy()
        {
            //序列化深度拷贝仅限于简单数据结构，复杂数据结构得根据结构性特殊拷贝
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as Shape;
        }
    }

    [Serializable]
    public class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Rectangle:Draw");
        }

        public Rectangle()
        {
            ShapeType = "Rectangle";
        }
    }

    [Serializable]
    public class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Circle:Draw");
        }

        public Circle()
        {
            ShapeType = "Circle";
        }
    }

    [Serializable]
    public class Square : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Square:Draw");
        }

        public Square()
        {
            ShapeType = "Square";
        }
    }

    public class ShapeCache
    {
        private static readonly Dictionary<String, Shape> ShapesMap = new Dictionary<String, Shape>();

        public static Shape GetShape(String shapeId)
        {
            Shape shape = ShapesMap[shapeId];
            return shape.DeepCopy();
        }

        public static void LoadCache()
        {
            Circle circle = new Circle();
            circle.SetId("1");

            ShapesMap.Add(circle.GetId(), circle);

            Square square = new Square();
            square.SetId("2");

            ShapesMap.Add(square.GetId(), square);

            Rectangle rectangle = new Rectangle();
            rectangle.SetId("3");

            ShapesMap.Add(rectangle.GetId(), rectangle);
        }
    }

    public 
    class Program
    {
        static void Main(string[] args)
        {
            ShapeCache.LoadCache();

            Shape cloneShape1 = ShapeCache.GetShape("1");
            cloneShape1.Draw();
            cloneShape1.Arr[1] = 4;

            Shape cloneShape2 = ShapeCache.GetShape("2");
            cloneShape2.Draw();


            Shape cloneShape3 = ShapeCache.GetShape("3");
            cloneShape3.Draw();

            Console.WriteLine("test Deep Copy1:" + cloneShape1.Arr[1]);
            Console.WriteLine("test Deep Copy3:" + cloneShape3.Arr[1]);
        }
    }
}
