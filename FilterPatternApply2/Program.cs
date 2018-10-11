using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterPatternApply2
{
    public enum Country
    {
        American,
        Africa,
        Asia,
        WestEurope,
        EastEurope,
    }

    public enum Sex
    {
        Male,
        Female,
    }

    public class LivingThings
    {
        public string Name { get; protected set; }
        public int Star { get; protected set; }
        public Sex Sex { get; protected set; }

        public LivingThings(string name, int star, Sex sex)
        {
            Name = name;
            Star = star;
            Sex = sex;
        }

    }
    public class Hero : LivingThings
    {

        public Country Country { get; protected set; }

        public Hero(string name, int star, Sex sex, Country country)
            : base(name, star, sex)
        {
            Country = country;
        }
    }

    public enum MonsterType
    {
        Dragon,
        Wolf,
        Tiger,
        Lion,
    }

    public class Monster : LivingThings
    {
        public MonsterType MonsterType { get; protected set; }

        public Monster(string name, int star, Sex sex, MonsterType monsterType)
            : base(name, star, sex)
        {
            MonsterType = monsterType;
        }
    }

    public delegate bool Compare<in T>(T data);

    public interface ICriteria<T>
    {
        List<T> MeetCriteria(ref List<T> heroes);
    }

    public class Criteria<T> : ICriteria<T>
    {
        protected readonly Compare<T> HeroCompare;
        public Criteria(Compare<T> heroCompare)
        {
            HeroCompare = heroCompare;
        }
        public List<T> MeetCriteria(ref List<T> datas)
        {
            List<T> resultList = new List<T>();
            for (int i = 0; i < datas.Count; ++i)
            {
                if (HeroCompare(datas[i]))
                {
                    resultList.Add(datas[i]);
                }
            }
            return resultList;
        }
    }

    public class AndCriteria<T> : ICriteria<T>
    {
        private readonly ICriteria<T> _criteria;
        private readonly ICriteria<T> _otherCriteria;

        public AndCriteria(ICriteria<T> criteria, ICriteria<T> otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }

        public List<T> MeetCriteria(ref List<T> heroes)
        {
            List<T> firstHeroes = _criteria.MeetCriteria(ref heroes);
            return _otherCriteria.MeetCriteria(ref firstHeroes);
        }
    }

    public class OrCriteria<T> : ICriteria<T>
    {
        private readonly ICriteria<T> _criteria;
        private readonly ICriteria<T> _otherCriteria;

        public OrCriteria(ICriteria<T> criteria, ICriteria<T> otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }

        public List<T> MeetCriteria(ref List<T> heroes)
        {
            List<T> firstHeroes = _criteria.MeetCriteria(ref heroes);
            List<T> secondHeroes = _otherCriteria.MeetCriteria(ref heroes);

            foreach (var hero in secondHeroes)
            {
                if (!firstHeroes.Contains(hero))
                    firstHeroes.Add(hero);
            }

            return firstHeroes;
        }
    }

    class Program
    {
        public static void PrintHeros(List<Hero> heroes)
        {
            for (int i = 0; i < heroes.Count; ++i)
            {
                Console.WriteLine("["
                + "Name: " + heroes[i].Name
                + ", Country: " + heroes[i].Country
                + ", Star: " + heroes[i].Star
                + ", Sex: " + heroes[i].Sex
                + "]");
            }
        }

        public static void PrintMonster(List<Monster> heroes)
        {
            for (int i = 0; i < heroes.Count; ++i)
            {
                Console.WriteLine("["
                + "Name: " + heroes[i].Name
                + ", Type: " + heroes[i].MonsterType
                + ", Star: " + heroes[i].Star
                + ", Sex: " + heroes[i].Sex
                + "]");
            }
        }

        public static List<T> ShortAndCriteriaCombo<T>(ref List<T> datas, ref List<ICriteria<T>> criterias)
        {
            List<T> resultList = datas;

            for (int i = 0; i < criterias.Count; ++i)
            {
                resultList = criterias[i].MeetCriteria(ref resultList);
            }

            return resultList;
        }

        public static List<T> ShortOrCriteriaCombo<T>(ref List<T> heroes, ref List<ICriteria<T>> criterias)
        {
            List<T> resultList = new List<T>();

            for (int i = 0; i < criterias.Count; ++i)
            {
                var tempResults = criterias[i].MeetCriteria(ref heroes);
                for (int j = 0; j < tempResults.Count; j++)
                {
                    if (!resultList.Contains(tempResults[j]))
                    {
                        resultList.Add(tempResults[j]);
                    }
                }
            }

            return resultList;
        }

        public static Compare<LivingThings> FiveStartCompare = things => things.Star == 5;
        public static Compare<LivingThings> FourStartCompare = things => things.Star == 4;
        public static Compare<LivingThings> ThreeStartCompare = things => things.Star == 3;
        public static Compare<LivingThings> TwoStartCompare = things => things.Star == 2;
        public static Compare<LivingThings> OneStartCompare = things => things.Star == 1;

        public static Compare<Hero> AfricaCompare = hero => hero.Country == Country.Africa;
        public static Compare<Hero> AsiaCompare = hero => hero.Country == Country.Asia;
        public static Compare<Hero> AmericanCompare = hero => hero.Country == Country.American;
        public static Compare<Hero> EastEuropeCompare = hero => hero.Country == Country.EastEurope;
        public static Compare<Hero> WestEuropeCompare = hero => hero.Country == Country.WestEurope;

        public static Compare<LivingThings> MaleCompare = things => things.Sex == Sex.Male;
        public static Compare<LivingThings> FemaleCompare = things => things.Sex == Sex.Female;

        public static Compare<Monster> DragonCompare = monster => monster.MonsterType == MonsterType.Dragon;
        public static Compare<Monster> WolfCompare = monster => monster.MonsterType == MonsterType.Wolf;
        public static Compare<Monster> TigerCompare = monster => monster.MonsterType == MonsterType.Tiger;
        public static Compare<Monster> LionCompare = monster => monster.MonsterType == MonsterType.Lion;

        static void Main(string[] args)
        {

            List<Hero> heroes = new List<Hero>
            {
                new Hero("Native Villager", 1, Sex.Male, Country.American),
                new Hero("Lucy", 1, Sex.Female, Country.Africa),
                new Hero("Brahman Priest", 2, Sex.Male, Country.Asia),
                new Hero("Moses", 2, Sex.Male, Country.Africa),
                new Hero("Mapuche Hero", 3, Sex.Male, Country.American),
                new Hero("Cleopatra", 4, Sex.Female, Country.Africa),
                new Hero("Lao Tzu", 4, Sex.Male, Country.Asia),
                new Hero("Beowulf", 4, Sex.Male, Country.EastEurope),
                new Hero("King Arthur", 5, Sex.Male, Country.WestEurope),
                new Hero("Jeanne D'Arc", 5, Sex.Female, Country.EastEurope),
                new Hero("Itzamna", 5, Sex.Female, Country.American),
                new Hero("Mulan", 5, Sex.Female, Country.Asia)
            };

            List<ICriteria<Hero>> criterias = new List<ICriteria<Hero>>();

            //FiveStar && Male
            Console.WriteLine("Hero------------------Five Star && Male------------------");
            ICriteria<Hero> criteria = new AndCriteria<Hero>(new Criteria<Hero>(FiveStartCompare), new Criteria<Hero>(MaleCompare));
            var resultList = criteria.MeetCriteria(ref heroes);
            PrintHeros(resultList);

            //Asia && Female
            Console.WriteLine("Hero------------------Asia && Female------------------");
            criteria = new AndCriteria<Hero>(new Criteria<Hero>(AsiaCompare), new Criteria<Hero>(FemaleCompare));
            resultList = criteria.MeetCriteria(ref heroes);
            PrintHeros(resultList);

            //Asia || America || Africa
            Console.WriteLine("Hero------------------Asia || America || Africa------------------");
            criteria = new OrCriteria<Hero>(new OrCriteria<Hero>(new Criteria<Hero>(AsiaCompare), new Criteria<Hero>(AmericanCompare)),
                new Criteria<Hero>(AfricaCompare));
            resultList = criteria.MeetCriteria(ref heroes);
            PrintHeros(resultList);

            //(Four Star || Five Star) && Male
            Console.WriteLine("Hero------------------(Four Star || Five Star) && Male------------------");
            criteria = new AndCriteria<Hero>(new OrCriteria<Hero>(new Criteria<Hero>(FourStartCompare), new Criteria<Hero>(FiveStartCompare)), new Criteria<Hero>(MaleCompare));
            resultList = criteria.MeetCriteria(ref heroes);
            PrintHeros(resultList);

            //Four Star || Five Star || Three Star || Two Star
            Console.WriteLine("------------------Four Star || Five Star || Three Star || Two Star------------------");
            criterias.Clear();
            criterias.Add(new Criteria<Hero>(FiveStartCompare));
            criterias.Add(new Criteria<Hero>(FourStartCompare));
            criterias.Add(new Criteria<Hero>(ThreeStartCompare));
            criterias.Add(new Criteria<Hero>(TwoStartCompare));
            resultList = ShortOrCriteriaCombo(ref heroes, ref criterias);
            PrintHeros(resultList);

            //Asia && Five Star && Female
            Console.WriteLine("Hero------------------Asia && Five Star && Female------------------");
            criterias.Clear();
            criterias.Add(new Criteria<Hero>(AsiaCompare));
            criterias.Add(new Criteria<Hero>(FiveStartCompare));
            criterias.Add(new Criteria<Hero>(FemaleCompare));
            resultList = ShortAndCriteriaCombo(ref heroes, ref criterias);
            PrintHeros(resultList);

            List<Monster> monsters = new List<Monster>
            {
                new Monster("Red Dragon", 4, Sex.Male, MonsterType.Dragon),
                new Monster("Black Dragon", 5, Sex.Female, MonsterType.Dragon),
                new Monster("Blue Dragon", 4, Sex.Male, MonsterType.Dragon),
                new Monster("Tiny Dragon", 3, Sex.Female, MonsterType.Dragon),

                new Monster("Red Tiger", 3, Sex.Male, MonsterType.Tiger),
                new Monster("Black Tiger", 4, Sex.Female, MonsterType.Tiger),
                new Monster("Blue Tiger", 3, Sex.Male, MonsterType.Tiger),
                new Monster("Tiny Tiger", 2, Sex.Female, MonsterType.Tiger),

                new Monster("Red Lion", 3, Sex.Male, MonsterType.Lion),
                new Monster("Black Lion", 4, Sex.Female, MonsterType.Lion),
                new Monster("Blue Lion", 3, Sex.Male, MonsterType.Lion),
                new Monster("Tiny Lion", 2, Sex.Female, MonsterType.Lion),

                new Monster("Red Wolf", 2, Sex.Male, MonsterType.Wolf),
                new Monster("Black Wolf", 3, Sex.Female, MonsterType.Wolf),
                new Monster("Blue Wolf", 2, Sex.Male, MonsterType.Wolf),
                new Monster("Tiny Wolf", 1, Sex.Female, MonsterType.Wolf),
            };

            List<ICriteria<Monster>> monstersCriterias = new List<ICriteria<Monster>>();

            //FiveStar && Male
            Console.WriteLine("Monster------------------Five Star && Male------------------");
            ICriteria<Monster> monsterCriteria = new AndCriteria<Monster>(new Criteria<Monster>(FiveStartCompare), new Criteria<Monster>(MaleCompare));
            var resultList1 = monsterCriteria.MeetCriteria(ref monsters);
            PrintMonster(resultList1);

            //Dragon && Female
            Console.WriteLine("Monster------------------Dragon && Female------------------");
            monsterCriteria = new AndCriteria<Monster>(new Criteria<Monster>(DragonCompare), new Criteria<Monster>(FemaleCompare));
            resultList1 = monsterCriteria.MeetCriteria(ref monsters);
            PrintMonster(resultList1);

            //Four Star || Five Star || Three Star || Two Star
            Console.WriteLine("------------------Four Star || Five Star || Three Star || Two Star------------------");
            monstersCriterias.Clear();
            monstersCriterias.Add(new Criteria<Monster>(FiveStartCompare));
            monstersCriterias.Add(new Criteria<Monster>(FourStartCompare));
            monstersCriterias.Add(new Criteria<Monster>(ThreeStartCompare));
            monstersCriterias.Add(new Criteria<Monster>(TwoStartCompare));
            resultList1 = ShortOrCriteriaCombo(ref monsters, ref monstersCriterias);
            PrintMonster(resultList1);

        }
    }
}
