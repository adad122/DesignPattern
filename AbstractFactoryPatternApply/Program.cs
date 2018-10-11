using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactoryPatternApply
{
    public enum SoilderType
    {
        Inventory,
        Rider,
        Archer,
    }
    public abstract class Soilder
    {
        protected String Name;
        protected SoilderType SoilderType;

        protected Soilder(string name, SoilderType soilderType)
        {
            Name = name;
            SoilderType = soilderType;
        }

        public virtual void SoilderAction()
        {
            Console.WriteLine("Soilder:SoilderAction");
        }
    }

    public interface IAsiaSpecialBuff
    {
        void AsiaSpecialBuff();
    }

    public interface IAmericanSpecialBuff
    {
        void AmericanSpecialBuff();
    }

    public class AsiaSoilder : Soilder, IAsiaSpecialBuff
    {
        public AsiaSoilder(string name, SoilderType soilderType) : base(name, soilderType)
        {
        }

        public void AsiaSpecialBuff()
        {
            Console.WriteLine("AsiaSpecialBuff");
        }

        public override void SoilderAction()
        {
            Console.WriteLine("AsiaSoilder:SoilderAction");
            AsiaSpecialBuff();
        }
    }

    public class AmericanSoilder : Soilder, IAmericanSpecialBuff
    {
        public AmericanSoilder(string name, SoilderType soilderType)
            : base(name, soilderType)
        {
        }

        public void AmericanSpecialBuff()
        {
            Console.WriteLine("AmericanSpecialBuff");
        }

        public override void SoilderAction()
        {
            Console.WriteLine("AmericanSoilder:SoilderAction");
            AmericanSpecialBuff();
        }
    }

    public class AmericanInventory : AmericanSoilder
    {
        public AmericanInventory(string name, SoilderType soilderType = SoilderType.Inventory)
            : base(name, soilderType)
        {
        }

        public override void SoilderAction()
        {
            base.SoilderAction();
            Console.WriteLine("AmericanInventory:SoilderAction");
        }
    }

    public class AmericanRider : AmericanSoilder
    {
        public AmericanRider(string name, SoilderType soilderType = SoilderType.Rider)
            : base(name, soilderType)
        {
        }

        public override void SoilderAction()
        {
            base.SoilderAction();
            Console.WriteLine("AmericanRider:SoilderAction");
        }
    }

    public class AmericanArcher : AmericanSoilder
    {
        public AmericanArcher(string name, SoilderType soilderType = SoilderType.Archer)
            : base(name, soilderType)
        {
        }

        public override void SoilderAction()
        {
            base.SoilderAction();
            Console.WriteLine("AmericanArcher:SoilderAction");
        }
    }

    public class AsiaInventory : AsiaSoilder
    {
        public AsiaInventory(string name, SoilderType soilderType = SoilderType.Inventory)
            : base(name, soilderType)
        {
        }

        public override void SoilderAction()
        {
            base.SoilderAction();
            Console.WriteLine("AsiaInventory:SoilderAction");
        }
    }

    public class AsiaRider : AsiaSoilder
    {
        public AsiaRider(string name, SoilderType soilderType = SoilderType.Rider)
            : base(name, soilderType)
        {
        }

        public override void SoilderAction()
        {
            base.SoilderAction();
            Console.WriteLine("AsiaRider:SoilderAction");
        }
    }

    public class AsiaArcher : AsiaSoilder
    {
        public AsiaArcher(string name, SoilderType soilderType = SoilderType.Archer)
            : base(name, soilderType)
        {
        }

        public override void SoilderAction()
        {
            base.SoilderAction();
            Console.WriteLine("AsiaArcher:SoilderAction");
        }
    }

    public abstract class AbstractFactory
    {
        public abstract Soilder GetSoilder(SoilderType soilderType);
    }

    public class AsiaSoilderFactory : AbstractFactory
    {
        public override Soilder GetSoilder(SoilderType soilderType)
        {
            switch (soilderType)
            {
                case SoilderType.Archer:
                    return new AsiaArcher("AsiaArcher");
                case SoilderType.Inventory:
                    return new AsiaInventory("AsiaInventory");
                case SoilderType.Rider:
                    return new AsiaRider("AsiaRider");
            }
            return null;
        }
    }

    public class AmericanSoilderFactory : AbstractFactory
    {
        public override Soilder GetSoilder(SoilderType soilderType)
        {
            switch (soilderType)
            {
                case SoilderType.Archer:
                    return new AmericanArcher("AmericanArcher");
                case SoilderType.Inventory:
                    return new AmericanInventory("AmericanInventory");
                case SoilderType.Rider:
                    return new AmericanRider("AmericanRider");
            }
            return null;
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            AbstractFactory asiaFactory = new AsiaSoilderFactory();

            Soilder asiaArcher = asiaFactory.GetSoilder(SoilderType.Archer);
            asiaArcher.SoilderAction();

            Soilder asiaInventory = asiaFactory.GetSoilder(SoilderType.Inventory);
            asiaInventory.SoilderAction();

            Soilder asiaRider = asiaFactory.GetSoilder(SoilderType.Rider);
            asiaRider.SoilderAction();

            AbstractFactory americanFactory = new AmericanSoilderFactory();

            Soilder americanArcher = americanFactory.GetSoilder(SoilderType.Archer);
            americanArcher.SoilderAction();

            Soilder americanInventory = americanFactory.GetSoilder(SoilderType.Inventory);
            americanInventory.SoilderAction();

            Soilder americanRider = americanFactory.GetSoilder(SoilderType.Rider);
            americanRider.SoilderAction();
        }
    }
}