#region 过滤器模式
//过滤器模式（Filter Pattern）或标准模式（Criteria Pattern）是一种设计模式，这种模式允许开发人员使用不同的标准来过滤一组对象，通过逻辑运算以解耦的方式把它们连接起来。这种类型的设计模式属于结构型模式，它结合多个标准来获得单一标准。
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FilterPattern
{
    public class Person
    {
        public string Name { get; private set; }
        public string Gender { get; private set; }
        public string MaritalStatus { get; private set; }

        public Person(String name, String gender, String maritalStatus)
        {
            Name = name;
            Gender = gender;
            MaritalStatus = maritalStatus;
        }


    }
    public interface ICriteria
    {
        List<Person> MeetCriteria(List<Person> persons);
    }

    public class CriteriaMale : ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> malePersons = new List<Person>();
            foreach (var persion in persons)
            {
                if (persion.Gender.Equals("Male"))
                    malePersons.Add(persion);

            }

            return malePersons;
        }
    }

    public class CriteriaFemale : ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> malePersons = new List<Person>();
            foreach (var persion in persons)
            {
                if (persion.Gender.Equals("Female"))
                    malePersons.Add(persion);

            }

            return malePersons;
        }
    }

    public class CriteriaSingle : ICriteria
    {
        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> malePersons = new List<Person>();
            foreach (var persion in persons)
            {
                if (persion.MaritalStatus.Equals("Single"))
                    malePersons.Add(persion);

            }

            return malePersons;
        }
    }

    public class AndCriteria : ICriteria
    {
        private ICriteria criteria;
        private ICriteria otherCriteria;

        public AndCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            this.criteria = criteria;
            this.otherCriteria = otherCriteria;
        }
        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> firstPersons = criteria.MeetCriteria(persons);
            return otherCriteria.MeetCriteria(firstPersons);
        }
    }

    public class OrCriteria : ICriteria
    {
        private ICriteria criteria;
        private ICriteria otherCriteria;

        public OrCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            this.criteria = criteria;
            this.otherCriteria = otherCriteria;
        }
        public List<Person> MeetCriteria(List<Person> persons)
        {
            List<Person> firstPersons = criteria.MeetCriteria(persons);
            List<Person> secondPersons = otherCriteria.MeetCriteria(persons);

            foreach (var person in secondPersons)
            {
                if (!firstPersons.Contains(person))
                    firstPersons.Add(person);
            }

            return firstPersons;
        }
    }
    class Program
    {
        public static void PrintPersons(List<Person> persons)
        {
            foreach (var person in persons)
            {
                Console.WriteLine("Person : [ Name : " + person.Name 
                +", Gender : " + person.Gender 
                +", Marital Status : " + person.MaritalStatus
                +" ]");
            }
       }      
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();

            persons.Add(new Person("Robert", "Male", "Single"));
            persons.Add(new Person("John", "Male", "Married"));
            persons.Add(new Person("Laura", "Female", "Married"));
            persons.Add(new Person("Diana", "Female", "Single"));
            persons.Add(new Person("Mike", "Male", "Single"));
            persons.Add(new Person("Bobby", "Male", "Single"));

            ICriteria male = new CriteriaMale();
            ICriteria female = new CriteriaFemale();
            ICriteria single = new CriteriaSingle();
            ICriteria andCriteria = new AndCriteria(single, male);
            ICriteria orCriteria = new OrCriteria(single, female);

            Console.WriteLine("Male");
            PrintPersons(male.MeetCriteria(persons));

            Console.WriteLine("Female");
            PrintPersons(female.MeetCriteria(persons));

            Console.WriteLine("Single Male");
            PrintPersons(andCriteria.MeetCriteria(persons));

            Console.WriteLine("Single Female");
            PrintPersons(orCriteria.MeetCriteria(persons));
        }
    }
}
