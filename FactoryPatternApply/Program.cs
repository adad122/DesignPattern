using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternApply
{
    public enum BuildingType
    {
        Barrack,
        Center,
        Food,
        Range,
        Stone,
    }
    public abstract class BuildingObj
    {
        protected String Name;
        protected BuildingType BuildingType;
        protected BuildingObj(String name, BuildingType buildingType)
        {
            Name = name;
            BuildingType = buildingType;
        }

        public virtual void DoUpgradeActions()
        {
            Console.WriteLine("BuildingObj:DoUpgradeActions " + BuildingType);
        }
    }

    public interface IBuildingTrain
    {
        void DoTrainActions();
    }

    public interface IBuildingTech
    {
        void DoTechActions();
    }

    public interface IBuildingCollect
    {
        void DoCollectActions();
    }

    public class CenterBuilding : BuildingObj
    {
        public CenterBuilding(String name, BuildingType buildingType)
            : base(name, buildingType)
        {
        }

        public override void DoUpgradeActions()
        {
            base.DoUpgradeActions();
            Console.WriteLine("CenterBuilding:DoUpgradeActions " + BuildingType);
        }
    }

    public class BarrackBuilding : BuildingObj, IBuildingTrain
    {
        public BarrackBuilding(String name, BuildingType buildingType)
            : base(name, buildingType)
        {
        }

        public override void DoUpgradeActions()
        {
            base.DoUpgradeActions();
            Console.WriteLine("BarrackBuildingBuilding:DoUpgradeActions " + BuildingType);
        }

        public void DoTrainActions()
        {

        }
    }

    public class ProductBuilding : BuildingObj, IBuildingCollect
    {
        public ProductBuilding(String name, BuildingType buildingType)
            : base(name, buildingType)
        {
        }

        public override void DoUpgradeActions()
        {
            base.DoUpgradeActions();
            Console.WriteLine("ProductBuilding:DoUpgradeActions " + BuildingType);
        }

        public void DoCollectActions()
        {
            Console.WriteLine("ProductBuilding:DoCollectActions " + BuildingType);
        }
    }

    public class BuildingFactory
    {
        public static BuildingObj GetBuildingObj(BuildingType type)
        {
            switch (type)
            {
                case BuildingType.Barrack:
                    return new BarrackBuilding("Barrack", type);
                case BuildingType.Center:
                    return new CenterBuilding("Center", type);
                case BuildingType.Food:
                    return new ProductBuilding("Food", type);
                case BuildingType.Range:
                    return new BarrackBuilding("Range", type);
                case BuildingType.Stone:
                    return new ProductBuilding("Stone", type);
            }

            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CenterBuilding centerBuilding = BuildingFactory.GetBuildingObj(BuildingType.Center) as CenterBuilding;
            if (centerBuilding != null) centerBuilding.DoUpgradeActions();

            BarrackBuilding barrackBuilding = BuildingFactory.GetBuildingObj(BuildingType.Barrack) as BarrackBuilding;
            if (barrackBuilding != null)
            {
                barrackBuilding.DoTrainActions();
                barrackBuilding.DoUpgradeActions();
            }

            BarrackBuilding rangeBuilding = BuildingFactory.GetBuildingObj(BuildingType.Range) as BarrackBuilding;
            if (rangeBuilding != null)
            {
                rangeBuilding.DoTrainActions();
                rangeBuilding.DoUpgradeActions();
            }

            ProductBuilding foodProductBuilding = BuildingFactory.GetBuildingObj(BuildingType.Food) as ProductBuilding;

            if (foodProductBuilding != null)
            {
                foodProductBuilding.DoCollectActions();
                foodProductBuilding.DoUpgradeActions();
            }

            ProductBuilding stoneProductBuilding = BuildingFactory.GetBuildingObj(BuildingType.Stone) as ProductBuilding;

            if (stoneProductBuilding != null)
            {
                stoneProductBuilding.DoCollectActions();
                stoneProductBuilding.DoUpgradeActions();
            }
        }
    }
}
