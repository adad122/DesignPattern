using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullObjectPatternApply
{
    /// <summary>
    /// 对于多处使用且使用时需要判空的泛用性对象，但是不用关心对象是否的确创建成功，且通过工厂或统一接口进行创建，
    /// 1. 可创建实现了通用方法的空对象进行返回，从而不会造成过多的判空操作特化处理
    /// 2. 可通过空对象的创建监控问题的发生
    /// 3. 使用前提必须是不用关心对象是否的确创建成功且他的生命周期是独立的不会造成其他影响(比如粒子特效的粒子)
    /// </summary>
    class Program
    {
        public abstract class AbstractPlayer
        {
            public String Name;
            public abstract bool IsNull();

            public abstract void Show();
        }

        public class Player : AbstractPlayer
        {
            public Player(string name)
            {
                Name = name;
            }

            public override bool IsNull()
            {
                return false;
            }

            public override void Show()
            {
                Console.WriteLine(Name);
            }
        }

        public class NullPlayer : AbstractPlayer
        {
            public override bool IsNull()
            {
                return true;
            }

            public override void Show()
            {
                Console.WriteLine("该玩家不存在");
            }
        }

        public class PlayerDataBase
        {
            private Dictionary<string, AbstractPlayer> _players = new Dictionary<string, AbstractPlayer>();

            public void AddNewPlayer(AbstractPlayer player)
            {
                _players.Add(player.Name, player);
            }

            public AbstractPlayer GetPlayer(string name)
            {
                return _players.ContainsKey(name) ? _players[name] : new NullPlayer();
            }
        }


        static void Main(string[] args)
        {
            PlayerDataBase playerDataBase = new PlayerDataBase();
            playerDataBase.AddNewPlayer(new Player("青羊小霸王"));
            playerDataBase.AddNewPlayer(new Player("武侯一霸"));

            AbstractPlayer player = playerDataBase.GetPlayer("青羊小霸王");
            player.Show();


            player = playerDataBase.GetPlayer("武侯一霸");
            player.Show();

            player = playerDataBase.GetPlayer("高新区方少");
            player.Show();
        }
    }
}
