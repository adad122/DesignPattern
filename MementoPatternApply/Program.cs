using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MementoPatternApply
{
    class Program
    {
        public delegate bool StageCondition(int data);
        public class Memento
        {
            private readonly int _stage;

            public Memento(int stage)
            {
                _stage = stage;
            }

            public int GetStage()
            {
                return _stage;
            }
        }

        public class Originator
        {
            private int _stage;

            public int GetStage()
            {
                return _stage;
            }

            public void SetStage(int stage)
            {
                _stage = stage;
            }

            public Memento SaveStateToMemento()
            {
                return new Memento(_stage);
            }

            public void GetStateFromMemento(Memento memento)
            {
                _stage = memento.GetStage();
            }
        }

        public class CareTaker
        {
            private readonly Stack<Memento> _mementoes = new Stack<Memento>(); 

            public void Add(Memento state)
            {
                _mementoes.Push(state);
            }

            public Memento Get()
            {
                return _mementoes.Pop();
            }
        }

        public class Stage
        {
            private readonly StageCondition _condition;
            public Stage(StageCondition condition)
            {
                _condition = condition;
            }

            public bool RunStage(int data)
            {
                Console.WriteLine("随机数为" + data);
                return _condition(data);
            }
        }

        static readonly StageCondition Stage1 = delegate(int data) { return data > 0; };
        static readonly StageCondition Stage2 = delegate(int data) { return data > 1; };
        static readonly StageCondition Stage3 = delegate(int data) { return data > 2; };
        static readonly StageCondition Stage4 = delegate(int data) { return data > 3; };
        static readonly StageCondition Stage5 = delegate(int data) { return data > 4; };


        static void Main(string[] args)
        {
            Stage[] stages = new[]
            {
                new Stage(Stage1),
                new Stage(Stage2),
                new Stage(Stage3),
                new Stage(Stage4),
                new Stage(Stage5),
            };


            Originator originator = new Originator();
            CareTaker careTaker = new CareTaker();

            originator.SetStage(0);

            while (true)
            {
                int curStage = originator.GetStage();

                if (curStage >= stages.Length)
                {
                    break;
                }

                Console.WriteLine("-------------当前关卡 " + (curStage + 1) + "-------------");

                careTaker.Add(originator.SaveStateToMemento());

                bool isStageWin = stages[curStage].RunStage(new Random().Next(1, 6));

                if (isStageWin)
                {
                    Console.WriteLine("恭喜获胜，即将进入下一关");
                    originator.SetStage(++curStage);
                }
                else
                {
                    Console.WriteLine("挑战失败，即将重新挑战");
                    curStage = careTaker.Get().GetStage();
                    originator.SetStage(curStage);
                }

                Console.WriteLine("------------------------------------");

                Thread.Sleep(1000);
            }
            Console.WriteLine("恭喜通过所有关卡");
        }
    }
}
