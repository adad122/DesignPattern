using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternApply
{
    public enum EquipmentType
    {
        None,
        Weapon,
        Armour,
        Shoe,
    }
    public abstract class EquipmentAttribute
    {
        protected int _hpAdd;
        protected int _attackAdd;
        protected int _defendAdd;
        protected int _speedAdd;
        protected EquipmentType _equipment;
        protected string _modal;

        protected EquipmentAttribute(int hpAdd, int attackAdd, int defendAdd, int speedAdd, EquipmentType equipmentType, string modal)
        {
            _hpAdd = hpAdd;
            _attackAdd = attackAdd;
            _defendAdd = defendAdd;
            _speedAdd = speedAdd;
            _equipment = equipmentType;
            _modal = modal;
        }

        public int HpAdd
        {
            get { return _hpAdd; }
        }

        public int AttackAdd
        {
            get { return _attackAdd; }
        }

        public int DefendAdd
        {
            get { return _defendAdd; }
        }

        public int SpeedAdd
        {
            get { return _speedAdd; }
        }

        public EquipmentType Equipment
        {
            get { return _equipment; }
        }

        public string Modal
        {
            get { return _modal; }
        }
    }
    public abstract class Weapon : EquipmentAttribute
    {
        protected Weapon(int hpAdd, int attackAdd, int defendAdd, int speedAdd, string modal)
            : base(hpAdd, attackAdd, defendAdd, speedAdd, EquipmentType.Weapon, modal)
        {
        }

        public virtual void AttackAction()
        {
            
        }
    }

    public abstract class Armour : EquipmentAttribute
    {
        protected Armour(int hpAdd, int attackAdd, int defendAdd, int speedAdd, string modal)
            : base(hpAdd, attackAdd, defendAdd, speedAdd, EquipmentType.Armour, modal)
        {
        }

        public virtual void DefendAction()
        {

        }
    }

    public abstract class Shoe : EquipmentAttribute
    {
        protected Shoe(int hpAdd, int attackAdd, int defendAdd, int speedAdd, string modal)
            : base(hpAdd, attackAdd, defendAdd, speedAdd, EquipmentType.Shoe, modal)
        {
        }

        public virtual void MoveAction()
        {

        }
    }

    public class Sword : Weapon
    {
        public Sword(int hpAdd = 0, int attackAdd = 100, int defendAdd = 0, int speedAdd = 0, 
            string modal = "Sword")
            : base(hpAdd, attackAdd, defendAdd, speedAdd, modal)
        {
        }

        public override void AttackAction()
        {
            base.AttackAction();
            Console.WriteLine("Sword:AttackAction");
        }
    }

    public class Bow : Weapon
    {
        public Bow(int hpAdd = 0, int attackAdd = 50, int defendAdd = 0, int speedAdd = 10, 
            string modal = "Bow")
            : base(hpAdd, attackAdd, defendAdd, speedAdd, modal)
        {
        }

        public override void AttackAction()
        {
            base.AttackAction();
            Console.WriteLine("Bow:AttackAction");
        }
    }

    public class LightArmor : Armour
    {
        public LightArmor(int hpAdd = 50, int attackAdd = 0, int defendAdd = 50, int speedAdd = -5, 
            string modal = "LightArmor")
            : base(hpAdd, attackAdd, defendAdd, speedAdd, modal)
        {
        }

        public override void DefendAction()
        {
            base.DefendAction();
            Console.WriteLine("LightArmor:DefendAction");
        }
    }

    public class HeavyArmor : Armour
    {
        public HeavyArmor(int hpAdd = 100, int attackAdd = 0, int defendAdd = 200, int speedAdd = -10,
            string modal = "HeavyArmor")
            : base(hpAdd, attackAdd, defendAdd, speedAdd, modal)
        {
        }

        public override void DefendAction()
        {
            base.DefendAction();
            Console.WriteLine("HeavyArmor:DefendAction");
        }
    }

    public class LeatherBoots : Shoe
    {
        public LeatherBoots(int hpAdd = 10, int attackAdd = 0, int defendAdd = 10, int speedAdd = 5,
            string modal = "LeatherBoots")
            : base(hpAdd, attackAdd, defendAdd, speedAdd, modal)
        {
        }

        public override void MoveAction()
        {
            base.MoveAction();
            Console.WriteLine("LeatherBoots:MoveAction");
        }
    }

    public class ClothShoes : Shoe
    {
        public ClothShoes(int hpAdd = 5, int attackAdd = 0, int defendAdd = 5, int speedAdd = 10,
            string modal = "ClothShoes")
            : base(hpAdd, attackAdd, defendAdd, speedAdd, modal)
        {
        }

        public override void MoveAction()
        {
            base.MoveAction();
            Console.WriteLine("ClothShoes:MoveAction");
        }
    }

    public class People
    {
        protected int _hp;
        protected int _attack;
        protected int _defend;
        protected int _speed;
        protected string _modal;

        public Armour Armour { get; set; }
        public Weapon Weapon { get; set; }
        public Shoe Shoe { get; set; }

        public int Hp
        {
            get { return _hp; }
        }

        public int TotalHp
        {
            get
            {
                int hp = Hp;
                if (Armour != null)
                {
                    hp += Armour.HpAdd;
                }

                if (Weapon != null)
                {
                    hp += Weapon.HpAdd;
                }

                if (Shoe != null)
                {
                    hp += Shoe.HpAdd;
                }

                return hp < 1 ? 1 : hp;
            }
        }

        public int Attack
        {
            get { return _attack; }
        }

        public int TotalAttack
        {
            get
            {
                int attack = Attack;
                if (Armour != null)
                {
                    attack += Armour.AttackAdd;
                }

                if (Weapon != null)
                {
                    attack += Weapon.AttackAdd;
                }

                if (Shoe != null)
                {
                    attack += Shoe.AttackAdd;
                }

                return attack < 1 ? 1 : attack;
            }
        }

        public int Defend
        {
            get { return _defend; }
        }

        public int TotalDefend
        {
            get
            {
                int defend = Defend;
                if (Armour != null)
                {
                    defend += Armour.DefendAdd;
                }

                if (Weapon != null)
                {
                    defend += Weapon.DefendAdd;
                }

                if (Shoe != null)
                {
                    defend += Shoe.DefendAdd;
                }

                return defend < 1 ? 1 : defend;
            }
        }

        public int Speed
        {
            get { return _speed; }
        }

        public int TotalSpeed
        {
            get
            {
                int speed = Speed;
                if (Armour != null)
                {
                    speed += Armour.SpeedAdd;
                }

                if (Weapon != null)
                {
                    speed += Weapon.SpeedAdd;
                }

                if (Shoe != null)
                {
                    speed += Shoe.SpeedAdd;
                }

                return speed < 1 ? 1 : speed;
            }
        }

        public string Modal
        {
            get { return _modal; }
        }

        public People(int hp, int attack, int defend, int speed, string modal)
        {
            _hp = hp;
            _attack = attack;
            _defend = defend;
            _speed = speed;
            _modal = modal;
        }

        public virtual void AttackAction()
        {
            if (Weapon != null)
            {
                Weapon.AttackAction();
            }
        }

        public virtual void DefendAction()
        {
            if (Armour != null)
            {
                Armour.DefendAction();
            }
        }

        public virtual void MoveAction()
        {
            if (Shoe != null)
            {
                Shoe.MoveAction();
            }
        }

        public void OutputAttribute()
        {
            Console.WriteLine("Name: " + _modal);
            Console.WriteLine("_weapon: " + (Weapon == null ? "none" : Weapon.Modal));
            Console.WriteLine("_armour: " + (Armour == null ? "none" : Armour.Modal));
            Console.WriteLine("_shoe: " + (Shoe == null ? "none" : Shoe.Modal));
            Console.WriteLine("Attack: " + TotalAttack);
            Console.WriteLine("Defend: " + TotalDefend);
            Console.WriteLine("Hp: " + TotalHp);
            Console.WriteLine("Speed: " + TotalSpeed);
        }
    }

    public class SoilderBuilder
    {
        public People GetFarmer()
        {
            People people = new People(100, 10, 10, 5, "Farmer")
            {
                Armour = new LightArmor(),
                Shoe = new ClothShoes()
            };
            return people;
        }

        public People GetSaber()
        {
            People people = new People(300, 50, 50, 10, "Saber")
            {
                Armour = new HeavyArmor(),
                Shoe = new LeatherBoots(),
                Weapon = new Sword()
            };
            return people;
        }

        public People GetArcher()
        {
            People people = new People(200, 30, 20, 20, "Archer")
            {
                Armour = new LightArmor(),
                Shoe = new ClothShoes(),
                Weapon = new Bow()
            };
            return people;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SoilderBuilder soilder = new SoilderBuilder();
            People farmer = soilder.GetFarmer();
            farmer.OutputAttribute();
            farmer.AttackAction();
            farmer.DefendAction();
            farmer.MoveAction();

            People saber = soilder.GetSaber();
            saber.OutputAttribute();
            saber.AttackAction();
            saber.DefendAction();
            saber.MoveAction();

            People archer = soilder.GetArcher();
            archer.OutputAttribute();
            archer.AttackAction();
            archer.DefendAction();
            archer.MoveAction();
        }
    }
}
