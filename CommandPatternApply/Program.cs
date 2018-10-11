using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPatternApply
{
    public interface ICommand
    {
        void ExecuteCommand();
        void UndoCommand();
    }

    public abstract class ConcreateCommand : ICommand
    {
        protected IReciver Reciver;
        protected ConcreateCommand(IReciver reciver)
        {
            Reciver = reciver;
        }

        public abstract void ExecuteCommand();

        public abstract void UndoCommand();
    }

    public interface IReciver
    {
        void OnReciveCommand();
        void OnReciveUndoCommand();
    }

    public abstract class Invoker
    {
        protected List<ICommand> Commands = new List<ICommand>();
        protected List<ICommand> UndoCommands = new List<ICommand>();

        public void AddCommand(ICommand command)
        {
            Commands.Add(command);
        }

        public void AddUndoCommand(ICommand command)
        {
            UndoCommands.Add(command);
        }

        public void ExecuteCommands()
        {
            foreach (ICommand command in Commands)
            {
                command.ExecuteCommand();
            }

            Commands.Clear();

            foreach (ICommand command in UndoCommands)
            {
                command.ExecuteCommand();
            }

            UndoCommands.Clear();
        }
    }

    public class BuildingActionInvoker : Invoker
    {
        
    }

    public class BuildingOptController
    {
        private readonly Invoker _invoker;

        public BuildingOptController(Invoker invoker)
        {
            _invoker = invoker;
        }
        public void ComitCommand(ICommand command)
        {
            _invoker.AddCommand(command);
        }

        public void ComitUndoCommand(ICommand command)
        {
            _invoker.AddUndoCommand(command);
        }
    }

    public class BuildingShowDetailCommand : ConcreateCommand
    {
        public BuildingShowDetailCommand(IReciver reciver) : base(reciver)
        {
        }

        public override void ExecuteCommand()
        {
            ((IBuildingCommandReciver)Reciver).OnReciveShowDetailCommand();
        }

        public override void UndoCommand()
        {
            ((IBuildingCommandReciver)Reciver).OnReciveShowDetailUndoCommand();
        }
    }

    public class BuildingUpgradeCommand : ConcreateCommand
    {
        public BuildingUpgradeCommand(IReciver reciver)
            : base(reciver)
        {
        }

        public override void ExecuteCommand()
        {
            ((IBuildingCommandReciver)Reciver).OnReciveUpgradeCommand();
        }

        public override void UndoCommand()
        {
            ((IBuildingCommandReciver)Reciver).OnReciveUpgradeUndoCommand();
        }
    }

    public class BuildingTrainCommand : ConcreateCommand
    {
        public BuildingTrainCommand(IReciver reciver)
            : base(reciver)
        {
        }

        public override void ExecuteCommand()
        {
            ((IBuildingCommandReciver)Reciver).OnReciveTrainCommand();
        }

        public override void UndoCommand()
        {
            ((IBuildingCommandReciver)Reciver).OnReciveTrainUndoCommand();
        }
    }

    public class BuildingTechCommand : ConcreateCommand
    {
        public BuildingTechCommand(IReciver reciver)
            : base(reciver)
        {
        }

        public override void ExecuteCommand()
        {
            ((IBuildingCommandReciver)Reciver).OnReciveTechCommand();
        }

        public override void UndoCommand()
        {
            ((IBuildingCommandReciver)Reciver).OnReciveTechUndoCommand();
        }
    }

    public interface IBuildingCommandReciver : IReciver
    {
        void OnReciveShowDetailCommand();

        void OnReciveShowDetailUndoCommand();

        void OnReciveUpgradeCommand();

        void OnReciveUpgradeUndoCommand();

        void OnReciveTrainCommand();

        void OnReciveTrainUndoCommand();

        void OnReciveTechCommand();

        void OnReciveTechUndoCommand();
    }

    public abstract class Building : IBuildingCommandReciver
    {
        public void OnReciveCommand()
        {
        }

        public void OnReciveUndoCommand()
        {
        }

        public virtual void OnReciveShowDetailCommand()
        {
        }

        public virtual void OnReciveShowDetailUndoCommand()
        {
        }

        public virtual void OnReciveUpgradeCommand()
        {
        }

        public virtual void OnReciveUpgradeUndoCommand()
        {
        }

        public virtual void OnReciveTrainCommand()
        {
        }

        public virtual void OnReciveTrainUndoCommand()
        {
        }

        public virtual void OnReciveTechCommand()
        {
        }

        public virtual void OnReciveTechUndoCommand()
        {
        }
    }

    public class CenterBuilding : Building
    {
        public override void OnReciveShowDetailCommand()
        {
            base.OnReciveShowDetailCommand();
            Console.WriteLine("CenterBuilding:OnReciveShowDetailCommand");
        }

        public override void OnReciveShowDetailUndoCommand()
        {
            base.OnReciveShowDetailUndoCommand();
            Console.WriteLine("CenterBuilding:OnReciveShowDetailUndoCommand");
        }

        public override void OnReciveUpgradeCommand()
        {
            base.OnReciveUpgradeCommand();
            Console.WriteLine("CenterBuilding:OnReciveUpgradeCommand");
        }

        public override void OnReciveUpgradeUndoCommand()
        {
            base.OnReciveUpgradeUndoCommand();
            Console.WriteLine("CenterBuilding:OnReciveUpgradeUndoCommand");
        }
    }

    public class TrainBuilding : Building
    {
        public override void OnReciveShowDetailCommand()
        {
            base.OnReciveShowDetailCommand();
            Console.WriteLine("TrainBuilding:OnReciveShowDetailCommand");
        }

        public override void OnReciveShowDetailUndoCommand()
        {
            base.OnReciveShowDetailUndoCommand();
            Console.WriteLine("TrainBuilding:OnReciveShowDetailUndoCommand");
        }

        public override void OnReciveUpgradeCommand()
        {
            base.OnReciveUpgradeCommand();
            Console.WriteLine("TrainBuilding:OnReciveUpgradeCommand");
        }

        public override void OnReciveUpgradeUndoCommand()
        {
            base.OnReciveUpgradeUndoCommand();
            Console.WriteLine("TrainBuilding:OnReciveUpgradeUndoCommand");
        }

        public override void OnReciveTrainCommand()
        {
            base.OnReciveTrainCommand();
            Console.WriteLine("TrainBuilding:OnReciveTrainCommand");
        }

        public override void OnReciveTrainUndoCommand()
        {
            base.OnReciveTrainUndoCommand();
            Console.WriteLine("TrainBuilding:OnReciveTrainUndoCommand");
        }
    }

    public class TechBuilding : Building
    {
        public override void OnReciveShowDetailCommand()
        {
            base.OnReciveShowDetailCommand();
            Console.WriteLine("TechBuilding:OnReciveShowDetailCommand");
        }

        public override void OnReciveShowDetailUndoCommand()
        {
            base.OnReciveShowDetailUndoCommand();
            Console.WriteLine("TechBuilding:OnReciveShowDetailUndoCommand");
        }

        public override void OnReciveUpgradeCommand()
        {
            base.OnReciveUpgradeCommand();
            Console.WriteLine("TechBuilding:OnReciveUpgradeCommand");
        }

        public override void OnReciveUpgradeUndoCommand()
        {
            base.OnReciveUpgradeUndoCommand();
            Console.WriteLine("TechBuilding:OnReciveUpgradeUndoCommand");
        }

        public override void OnReciveTechCommand()
        {
            base.OnReciveTechCommand();
            Console.WriteLine("TechBuilding:OnReciveTechCommand");
        }

        public override void OnReciveTechUndoCommand()
        {
            base.OnReciveTechUndoCommand();
            Console.WriteLine("TechBuilding:OnReciveTechUndoCommand");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            IReciver centerBuilding = new CenterBuilding();
            IReciver trainBuilding = new TrainBuilding();
            IReciver techBuilding = new TechBuilding();

            Invoker buildingActionInvoker = new BuildingActionInvoker();
            BuildingOptController buildingOptController = new BuildingOptController(buildingActionInvoker);

            buildingOptController.ComitCommand(new BuildingShowDetailCommand(centerBuilding));
            buildingActionInvoker.ExecuteCommands();

            buildingOptController.ComitCommand(new BuildingTrainCommand(trainBuilding));
            buildingActionInvoker.ExecuteCommands();

            buildingOptController.ComitCommand(new BuildingTechCommand(techBuilding));
            buildingActionInvoker.ExecuteCommands();

            buildingOptController.ComitCommand(new BuildingShowDetailCommand(centerBuilding));
            buildingOptController.ComitCommand(new BuildingShowDetailCommand(trainBuilding));
            buildingOptController.ComitCommand(new BuildingShowDetailCommand(techBuilding));
            buildingActionInvoker.ExecuteCommands();

            buildingOptController.ComitCommand(new BuildingTechCommand(centerBuilding));
            buildingActionInvoker.ExecuteCommands();
        }
    }
}
