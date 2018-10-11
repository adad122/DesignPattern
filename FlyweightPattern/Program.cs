#region 享元模式
//享元模式（Flyweight Pattern）主要用于减少创建对象的数量，以减少内存占用和提高性能。这种类型的设计模式属于结构型模式，它提供了减少对象数量从而改善应用所需的对象结构的方式。

//享元模式尝试重用现有的同类对象，如果未找到匹配的对象，则创建新对象。我们将通过创建 5 个对象来画出 20 个分布于不同位置的圆来演示这种模式。由于只有 5 种可用的颜色，所以 color 属性被用来检查现有的 Circle 对象。

//介绍
//意图：运用共享技术有效地支持大量细粒度的对象。

//主要解决：在有大量对象时，有可能会造成内存溢出，我们把其中共同的部分抽象出来，如果有相同的业务请求，直接返回在内存中已有的对象，避免重新创建。

//何时使用： 1、系统中有大量对象。 2、这些对象消耗大量内存。 3、这些对象的状态大部分可以外部化。 4、这些对象可以按照内蕴状态分为很多组，当把外蕴对象从对象中剔除出来时，每一组对象都可以用一个对象来代替。 5、系统不依赖于这些对象身份，这些对象是不可分辨的。

//如何解决：用唯一标识码判断，如果在内存中有，则返回这个唯一标识码所标识的对象。

//关键代码：用 HashMap 存储这些对象。

//应用实例： 1、JAVA 中的 String，如果有则返回，如果没有则创建一个字符串保存在字符串缓存池里面。 2、数据库的数据池。

//优点：大大减少对象的创建，降低系统的内存，使效率提高。

//缺点：提高了系统的复杂度，需要分离出外部状态和内部状态，而且外部状态具有固有化的性质，不应该随着内部状态的变化而变化，否则会造成系统的混乱。

//使用场景： 1、系统有大量相似对象。 2、需要缓冲池的场景。

//注意事项： 1、注意划分外部状态和内部状态，否则可能会引起线程安全问题。 2、这些类必须有一个工厂对象加以控制。
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlyweightPattern
{
    public interface IShape
    {
        void Draw();
    }

    public class Circle : IShape
    {
        private String _color;
        private int _x;
        private int _y;
        private int _radius;

        public Circle(String color)
        {
            _color = color;
        }

        public void SetX(int x)
        {
            _x = x;
        }

        public void SetY(int y)
        {
            _y = y;
        }

        public void SetRadius(int radius)
        {
            _radius = radius;
        }

        public void Draw()
        {
            Console.WriteLine("Circle:" + "X=" + _x + " Y=" + _y + " Color=" + _color + " Radius=" + _radius);
        }
    }

    public class ShapeFactory
    {
        private static Dictionary<String, IShape> _sCircleMap = new Dictionary<string, IShape>();

        public static IShape GetCircle(String color)
        {
            if (!_sCircleMap.ContainsKey(color))
            {
                IShape circle = new Circle(color);
                _sCircleMap.Add(color, circle);
                Console.WriteLine("Creating circle of color :" + color);
            }

            return _sCircleMap[color];
        }
    }
    class Program
    {
        private static readonly String[] Colors = {"Red", "Green", "Blue", "White", "Black"};

        private static String GetRandomColor()
        {
            return Colors[new Random().Next(0, 4)];
        }

        private static int GetRandomX()
        {
            return new Random().Next(0, 100);
        }

        private static int GetRandomY()
        {
            return new Random().Next(0, 100);
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; ++i)
            {
                Circle circle = ShapeFactory.GetCircle(GetRandomColor()) as Circle;
                if (circle != null)
                {
                    circle.SetX(GetRandomX());
                    circle.SetY(GetRandomY());
                    circle.SetRadius(100);
                    circle.Draw();
                }
                Thread.Sleep(100);
            }
        }
    }
}
