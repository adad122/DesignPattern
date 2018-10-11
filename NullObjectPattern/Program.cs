#region 空对象模式
//在空对象模式（Null Object Pattern）中，一个空对象取代 NULL 对象实例的检查。Null 对象不是检查空值，而是反应一个不做任何动作的关系。这样的 Null 对象也可以在数据不可用的时候提供默认的行为。

//在空对象模式中，我们创建一个指定各种要执行的操作的抽象类和扩展该类的实体类，还创建一个未对该类做任何实现的空对象类，该空对象类将无缝地使用在需要检查空值的地方。
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullObjectPattern
{
    public abstract class AbstractCustomer
    {
        protected String Name;
        public abstract bool IsNull();

        public virtual String GetName()
        {
            return Name;
        }
    }

    public class RealCustomer : AbstractCustomer
    {
        public RealCustomer(String name)
        {
            Name = name;
        }

        public override bool IsNull()
        {
            return false;
        }
    }

    public class NullCustomer : AbstractCustomer
    {
        public override String GetName()
        {
            return "NullCustomer";
        }

        public override bool IsNull()
        {
            return true;
        }
    }

    public class CustomerFactory
    {
        public static String[] NameStrings =
        {
            "Rob", "Joe", "Julie"
        };

        public static AbstractCustomer GetCustomer(String name)
        {
            foreach (string nameString in NameStrings)
            {
                if (nameString.Equals(name))
                    return new RealCustomer(name);
            }

            return new NullCustomer();
        }
}
    class Program
    {
        static void Main(string[] args)
        {
            AbstractCustomer customer1 = CustomerFactory.GetCustomer("Rob");
            AbstractCustomer customer2 = CustomerFactory.GetCustomer("Alice");
            Console.WriteLine(customer1.GetName());
            Console.WriteLine(customer2.GetName());
        }
    }
}
