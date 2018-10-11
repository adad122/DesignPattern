using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace FilterPatternApply
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
    public class Hero
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public int Star { get; protected set; }
        public Country Country { get; protected set; }
        public Sex Sex { get; protected set; }

        public Hero(int id, string name, int star, Country country, Sex sex)
        {
            Id = id;
            Name = name;
            Star = star;
            Country = country;
            Sex = sex;
        }
    }

    public interface ICriteria
    {
        List<Hero> MeetCriteria(ref List<Hero> heroes);
    }

    public class CriteriaFiveStar : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Star == 5)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaFourStar : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Star == 4)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaThreeStar : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Star == 3)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaTwoStar : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Star == 2)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaOneStar : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Star == 1)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaAsia : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Country == Country.Asia)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaAfrica : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Country == Country.Africa)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaAmerican : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Country == Country.American)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaEastEurope : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Country == Country.EastEurope)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaWestEurope : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Country == Country.WestEurope)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaMale : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Sex == Sex.Male)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class CriteriaFemale : ICriteria
    {
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> resultList = new List<Hero>();
            for (int i = 0; i < heroes.Count; ++i)
            {
                if (heroes[i].Sex == Sex.Female)
                {
                    resultList.Add(heroes[i]);
                }
            }
            return resultList;
        }
    }

    public class AndCriteria : ICriteria
    {
        private readonly ICriteria _criteria;
        private readonly ICriteria _otherCriteria;

        public AndCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> firstHeroes = _criteria.MeetCriteria(ref heroes);
            return _otherCriteria.MeetCriteria(ref firstHeroes);
        }
    }

    public class OrCriteria : ICriteria
    {
        private readonly ICriteria _criteria;
        private readonly ICriteria _otherCriteria;

        public OrCriteria(ICriteria criteria, ICriteria otherCriteria)
        {
            _criteria = criteria;
            _otherCriteria = otherCriteria;
        }
        public List<Hero> MeetCriteria(ref List<Hero> heroes)
        {
            List<Hero> firstHeroes = _criteria.MeetCriteria(ref heroes);
            List<Hero> secondHeroes = _otherCriteria.MeetCriteria(ref heroes);

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

        public static List<Hero> ShortAndCriteriaCombo(ref List<Hero> heroes, ref List<ICriteria> criterias)
        {
            List<Hero> resultList = heroes;

            for (int i = 0; i < criterias.Count; ++i)
            {
                resultList = criterias[i].MeetCriteria(ref resultList);
            }

            return resultList;
        }

        public static List<Hero> ShortOrCriteriaCombo(ref List<Hero> heroes, ref List<ICriteria> criterias)
        {
            List<Hero> resultList = new List<Hero>();
            HashSet<int> idSet = new HashSet<int>();

            for (int i = 0; i < criterias.Count; ++i)
            {
                var tempResults = criterias[i].MeetCriteria(ref heroes);
                for (int j = 0; j < tempResults.Count; j++)
                {
                    if (!idSet.Contains(tempResults[j].Id))
                    {
                        idSet.Add(tempResults[j].Id);
                        resultList.Add(tempResults[j]);
                    }
                }
            }

            return resultList;
        }
        static void Main(string[] args)
        {
            List<Hero> heroes = new List<Hero>
            {
                new Hero(1, "Native Villager", 1, Country.American, Sex.Male),
                new Hero(2, "Lucy", 1, Country.Africa, Sex.Female),
                new Hero(3, "Brahman Priest", 2, Country.Asia, Sex.Male),
                new Hero(4, "Moses", 2, Country.Africa, Sex.Male),
                new Hero(5, "Mapuche Hero", 3, Country.American, Sex.Male),
                new Hero(6, "Cleopatra", 4, Country.Africa, Sex.Female),
                new Hero(7, "Lao Tzu", 4, Country.Asia, Sex.Male),
                new Hero(8, "Beowulf", 4, Country.EastEurope, Sex.Male),
                new Hero(9, "King Arthur", 5, Country.WestEurope, Sex.Male),
                new Hero(10, "Jeanne D'Arc", 5, Country.EastEurope, Sex.Female),
                new Hero(11, "Itzamna", 5, Country.American, Sex.Female),
                new Hero(12, "Mulan", 5, Country.Asia, Sex.Female)
            };

            List<Hero> resultList;
            List<ICriteria> criterias = new List<ICriteria>();
            
            //FiveStar && Male
            Console.WriteLine("------------------Five Star && Male------------------");
            ICriteria criteria = new AndCriteria(new CriteriaFiveStar(), new CriteriaMale());
            resultList = criteria.MeetCriteria(ref heroes);
            PrintHeros(resultList);

            //Asia && Female
            Console.WriteLine("------------------Asia && Female------------------");
            criteria = new AndCriteria(new CriteriaAsia(), new CriteriaFemale());
            resultList = criteria.MeetCriteria(ref heroes);
            PrintHeros(resultList);

            //Asia || America || Africa
            Console.WriteLine("------------------Asia || America || Africa------------------");
            criteria = new OrCriteria(new OrCriteria(new CriteriaAsia(), new CriteriaAmerican()), new CriteriaAfrica());
            resultList = criteria.MeetCriteria(ref heroes);
            PrintHeros(resultList);

            //(Four Star || Five Star) && Male
            Console.WriteLine("------------------(Four Star || Five Star) && Male------------------");
            criteria = new AndCriteria(new OrCriteria(new CriteriaFourStar(), new CriteriaFiveStar()), new CriteriaMale());
            resultList = criteria.MeetCriteria(ref heroes);
            PrintHeros(resultList);

            //Four Star || Five Star || Three Star || Two Star
            Console.WriteLine("------------------Four Star || Five Star || Three Star || Two Star------------------");
            criterias.Clear();
            criterias.Add(new CriteriaFiveStar());
            criterias.Add(new CriteriaFourStar());
            criterias.Add(new CriteriaThreeStar());
            criterias.Add(new CriteriaTwoStar());
            resultList = ShortOrCriteriaCombo(ref heroes, ref criterias);
            PrintHeros(resultList);

            //Asia && Five Star && Female
            Console.WriteLine("------------------Asia && Five Star && Male------------------");
            criterias.Clear();
            criterias.Add(new CriteriaAsia());
            criterias.Add(new CriteriaFiveStar());
            criterias.Add(new CriteriaFemale());
            resultList = ShortAndCriteriaCombo(ref heroes, ref criterias);
            PrintHeros(resultList);
        }
    }
}
