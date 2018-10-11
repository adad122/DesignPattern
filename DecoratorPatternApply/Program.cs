using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPatternApply
{
    public interface IHeroAction
    {
        void MoveAction();
        void AttackAction();
        void IdleAction();
        void DeadAction();
    }

    public abstract class Hero : IHeroAction
    {
        public virtual void MoveAction()
        {
            Console.WriteLine("Hero:MoveAction");
        }

        public virtual void AttackAction()
        {
            Console.WriteLine("Hero:AttackAction");
        }

        public virtual void IdleAction()
        {
            Console.WriteLine("Hero:IdleAction");
        }

        public virtual void DeadAction()
        {
            Console.WriteLine("Hero:DeadAction");
        }
    }

    public abstract class HeroActionDecorator : IHeroAction
    {
        protected IHeroAction DecoratorHero;
        protected HeroActionDecorator(IHeroAction decoratorHero)
        {
            DecoratorHero = decoratorHero;
        }

        public virtual void MoveAction()
        {
            DecoratorHero.MoveAction();
        }

        public virtual void AttackAction()
        {
            DecoratorHero.AttackAction();
        }

        public virtual void IdleAction()
        {
            DecoratorHero.IdleAction();
        }

        public virtual void DeadAction()
        {
            DecoratorHero.DeadAction();
        }
    }

    public class KingArthur : Hero
    {
        public override void MoveAction()
        {
            base.MoveAction();
            Console.WriteLine("KingArthur:MoveAction");
        }

        public override void AttackAction()
        {
            base.AttackAction();
            Console.WriteLine("KingArthur:AttackAction");
        }

        public override void IdleAction()
        {
            base.IdleAction();
            Console.WriteLine("KingArthur:IdleAction");
        }

        public override void DeadAction()
        {
            base.DeadAction();
            Console.WriteLine("KingArthur:DeadAction");
        }
    }

    public class Mulan : Hero
    {
        public override void MoveAction()
        {
            base.MoveAction();
            Console.WriteLine("Mulan:MoveAction");
        }

        public override void AttackAction()
        {
            base.AttackAction();
            Console.WriteLine("Mulan:AttackAction");
        }

        public override void IdleAction()
        {
            base.IdleAction();
            Console.WriteLine("Mulan:IdleAction");
        }

        public override void DeadAction()
        {
            base.DeadAction();
            Console.WriteLine("Mulan:DeadAction");
        }
    }

    public class NewYearDress : HeroActionDecorator
    {
        public NewYearDress(IHeroAction decoratorHero)
            : base(decoratorHero)
        {
        }

        public override void MoveAction()
        {
            base.MoveAction();
            Console.WriteLine("NewYearDress:MoveAction");
        }

        public override void AttackAction()
        {
            base.AttackAction();
            Console.WriteLine("NewYearDress:AttackAction");
        }

        public override void IdleAction()
        {
            base.IdleAction();
            Console.WriteLine("NewYearDress:IdleAction");
        }

        public override void DeadAction()
        {
            base.DeadAction();
            Console.WriteLine("NewYearDress:DeadAction");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IHeroAction kingArthur = new KingArthur();
            kingArthur.IdleAction();
            kingArthur.AttackAction();
            kingArthur.MoveAction();
            kingArthur.DeadAction();

            Console.WriteLine("---------------------------------------------------");

            IHeroAction kingArthurNewYear = new NewYearDress(new KingArthur());
            kingArthurNewYear.IdleAction();
            kingArthurNewYear.AttackAction();
            kingArthurNewYear.MoveAction();
            kingArthurNewYear.DeadAction();

            Console.WriteLine("---------------------------------------------------");
            IHeroAction mulan = new Mulan();
            mulan.IdleAction();
            mulan.AttackAction();
            mulan.MoveAction();
            mulan.DeadAction();

            Console.WriteLine("---------------------------------------------------");

            IHeroAction mulanNewYear = new NewYearDress(new Mulan());
            mulanNewYear.IdleAction();
            mulanNewYear.AttackAction();
            mulanNewYear.MoveAction();
            mulanNewYear.DeadAction();
        }
    }
}
