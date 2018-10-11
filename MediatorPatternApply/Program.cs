using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPatternApply
{
    /// <summary>
    /// 基本命令接口
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        void ExecuteCommand();
        ///// <summary>
        ///// 撤销命令
        ///// </summary>
        //void UndoCommand();
    }

    /// <summary>
    /// 命令接收者
    /// </summary>
    public interface IReciver
    {
        /// <summary>
        /// 响应接收到命令
        /// </summary>
        /// <param name="command">接收到的命令</param>
        void OnReciveCommand(ICommand command);
        ///// <summary>
        ///// 响应撤销命令
        ///// </summary>
        ///// <param name="command">接收到的命令</param>
        //void OnReciveUndoCommand(ICommand command);
    }

    /// <summary>
    /// 命令实体类
    /// </summary>
    public abstract class ConcreateCommand : ICommand
    {
        /// <summary>
        /// 命令的接收者
        /// </summary>
        protected IReciver Reciver;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="reciver">命令的接收者</param>
        protected ConcreateCommand(IReciver reciver)
        {
            Reciver = reciver;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        public abstract void ExecuteCommand();

        ///// <summary>
        ///// 撤销命令
        ///// </summary>
        //public abstract void UndoCommand();
    }

    /// <summary>
    /// 命令执行者
    /// </summary>
    public abstract class Invoker
    {
        /// <summary>
        /// 命令寄存器
        /// </summary>
        protected List<ICommand> Commands = new List<ICommand>();

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="command">添加的命令</param>
        public void AddCommand(ICommand command)
        {
            Commands.Add(command);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        public void ExecuteCommands()
        {
            foreach (ICommand command in Commands)
            {
                command.ExecuteCommand();
            }

            Commands.Clear();
        }
    }

    /// <summary>
    /// 战斗命令执行者
    /// </summary>
    public class BattleCommandInvoker : Invoker
    {

    }

    /// <summary>
    /// 中介者
    /// </summary>
    public interface IMediator
    {
        void Command(ICommand command);
    }

    public enum BattleState
    {
        /// <summary>
        /// 战斗中
        /// </summary>
        Battling,
        /// <summary>
        /// 胜利
        /// </summary>
        Win,
        /// <summary>
        /// 失败
        /// </summary>
        Lose,
    }


    /// <summary>
    /// 战斗管理器
    /// </summary>
    public class BattleMannager : IMediator, IReciver
    {
        /// <summary>
        /// 英雄对象
        /// </summary>
        public BattleObjects[] HeroObjectses = new BattleObjects[3];

        /// <summary>
        /// 英雄对象
        /// </summary>
        public BattleObjects[] MonsterObjectses = new BattleObjects[3];

        /// <summary>
        /// 战斗命令执行者实例
        /// </summary>
        private readonly BattleCommandInvoker _battleCommandInvoker;
        /// <summary>
        /// 临时对象
        /// </summary>
        private readonly BattleObjects[] _temp = new BattleObjects[6];
        /// <summary>
        /// 构造函数
        /// </summary>
        public BattleMannager()
        {
            _battleCommandInvoker = new BattleCommandInvoker();

            for (int i = 0; i < 3; ++i)
            {
                HeroObjectses[i] = null;
                MonsterObjectses[i] = null;
            }
        }

        /// <summary>
        /// 设置英雄对象
        /// </summary>
        /// <param name="hero">英雄对象</param>
        /// <param name="index">索引</param>
        public void SetHero(BattleObjects hero, int index)
        {
            if (hero == null || index < 0 || index > 2)
                return;

            HeroObjectses[index] = hero;
            hero.Index = index;
        }
        /// <summary>
        /// 设置怪物对象
        /// </summary>
        /// <param name="monster">怪物对象</param>
        /// <param name="index">索引</param>
        public void SetMonster(BattleObjects monster, int index)
        {
            if (monster == null || index < 0 || index > 2)
                return;

            MonsterObjectses[index] = monster;
            monster.Index = index;
        }
        /// <summary>
        /// 运行当前回合
        /// </summary>
        /// <returns>战斗状态</returns>
        public BattleState RunTurn()
        {
            _battleCommandInvoker.ExecuteCommands();
            return BattleEndCheck();
        }
        /// <summary>
        /// 判定战斗是否结束
        /// </summary>
        /// <returns></returns>
        private BattleState BattleEndCheck()
        {
            bool battleMark = true;
            for (int i = 0; i < 3; ++i)
            {
                if (MonsterObjectses[i] != null && MonsterObjectses[i].IsDead == false)
                {
                    battleMark = false;
                    break;
                }
            }

            if (battleMark)
            {
                return BattleState.Win;
            }

            battleMark = true;
            for (int i = 0; i < 3; ++i)
            {
                if (HeroObjectses[i] != null && HeroObjectses[i].IsDead == false)
                {
                    battleMark = false;
                    break;
                }
            }

            if (battleMark)
            {
                return BattleState.Lose;
            }

            return BattleState.Battling;
        }
        /// <summary>
        /// 寄存一条命令
        /// </summary>
        /// <param name="command">命令实例</param>
        public void Command(ICommand command)
        {
            _battleCommandInvoker.AddCommand(command);
        }
        /// <summary>
        /// 收到执行命令
        /// </summary>
        /// <param name="command">执行的命令</param>
        public void OnReciveCommand(ICommand command)
        {
            if (command.GetType() == typeof(BattleCommand))
            {
                BattleCommand battleCommand = command as BattleCommand;

                OnReciveBattleCommand(battleCommand);
            }
        }

        public bool IsLegalBattleTarget(BattleCommand command, BattleObjects battleObjects)
        {
            return command != null && battleObjects != null &&
                ((!battleObjects.IsDead && command.BuffType != BuffType.Revive) ||
                   (battleObjects.IsDead && command.BuffType == BuffType.Revive));
        }

        /// <summary>
        /// 收到战斗类消息
        /// </summary>
        /// <param name="command">执行的命令</param>
        private void OnReciveBattleCommand(BattleCommand command)
        {
            if (command == null || command.Index < 0 || command.Index >= 3)
            {
                return;
            }

            BattleObjects sender = command.Sender as BattleObjects;
            if (sender == null || sender.IsDead)
            {
                return;
            }

            for (int i = 0; i < 6; ++i)
            {
                _temp[i] = null;
            }

            int index = 0;
            int effectNum = command.ValueEffectNum;

            BattleObjects[] group = null;

            switch (command.EffectGroup)
            {
                case EffectGroup.Self:
                    group = sender.Group == GroupTag.Monster ? MonsterObjectses : HeroObjectses;
                    break;
                case EffectGroup.Enemy:
                    group = sender.Group == GroupTag.Monster ? HeroObjectses : MonsterObjectses;
                    break;
            }

            if (group == null)
            {
                return;
            }

            //优先处理选定对象
            if (IsLegalBattleTarget(command, group[command.Index]))
            {
                _temp[index++] = group[command.Index];
                effectNum--;
                Console.WriteLine(sender.Name + " 向 " + group[command.Index].Name + " 使用了 " +
                                  command.Name);
            }

            //处理附加对象
            for (int i = 0; i < 3 && effectNum > 0; ++i)
            {
                if (i != command.Index && IsLegalBattleTarget(command, group[i]))
                {
                    _temp[index++] = group[i];
                    effectNum--;
                    Console.WriteLine(sender.Name + " 向 " + group[command.Index].Name + " 使用了 " +
                        command.Name);
                }
            }

            for (int i = 0; i < index; ++i)
            {
                _temp[i].OnReciveCommand(command);
            }
        }
        ///// <summary>
        ///// 收到取消的命令
        ///// </summary>
        ///// <param name="command">取消的命令</param>
        //public void OnReciveUndoCommand(ICommand command)
        //{

        //}
    }

    /// <summary>
    /// 基础属性抽象类
    /// </summary>
    public abstract class BaseAttribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 最大生命
        /// </summary>
        public int MaxHp;
        /// <summary>
        /// 攻击力
        /// </summary>
        public int Attack;
        /// <summary>
        /// 防御力
        /// </summary>
        public int Defend;
        /// <summary>
        /// 当前生命值
        /// </summary>
        public int Hp;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxHp">最大生命</param>
        /// <param name="attack">攻击力</param>
        /// <param name="defend">防御力</param>
        /// <param name="name">名称</param>
        protected BaseAttribute(int maxHp, int attack, int defend, string name)
        {
            Hp = MaxHp = maxHp;
            Attack = attack;
            Defend = defend;
            Name = name;
        }
    }


    public class BattleCommand : ConcreateCommand
    {
        /// <summary>
        /// 作用值
        /// </summary>
        public int Value;
        /// <summary>
        /// 作用值数量
        /// </summary>
        public int ValueEffectNum;
        /// <summary>
        /// 作用对象
        /// </summary>
        public EffectGroup EffectGroup;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 命令发出者
        /// </summary>
        public IReciver Sender;
        /// <summary>
        /// 命令作用对象索引
        /// </summary>
        public int Index;
        /// <summary>
        /// 命令类型
        /// </summary>
        public BuffType BuffType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sender">命令发出者</param>
        /// <param name="reciver">命令接收者</param>
        /// <param name="value">作用值</param>
        /// <param name="valueEffectNum">作用值数量</param>
        /// <param name="name">命令名称</param>
        /// <param name="index">作用对象索引</param>
        /// <param name="buffType">Buff类型</param>
        /// <param name="effectGroup">作用对象</param>
        public BattleCommand(IReciver sender, IReciver reciver, int value, int valueEffectNum, string name,
            BuffType buffType, EffectGroup effectGroup, int index)
            : base(reciver)
        {
            Value = value;
            ValueEffectNum = valueEffectNum;
            Name = name;
            Sender = sender;
            Index = index;
            BuffType = buffType;
            EffectGroup = effectGroup;
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        public override void ExecuteCommand()
        {
            Reciver.OnReciveCommand(this);
        }
        ///// <summary>
        ///// 取消命令
        ///// </summary>
        //public override void UndoCommand()
        //{
        //    Reciver.OnReciveUndoCommand(this);
        //}
    }

    /// <summary>
    /// Buff类型
    /// </summary>
    public enum BuffType
    {
        /// <summary>
        /// 即时伤害
        /// </summary>
        Damage,
        /// <summary>
        /// 即时治疗
        /// </summary>
        Cure,
        /// <summary>
        /// 复活
        /// </summary>
        Revive,
    }

    /// <summary>
    /// 效果影响群体
    /// </summary>
    public enum EffectGroup
    {
        /// <summary>
        /// 己方单体
        /// </summary>
        Self,
        /// <summary>
        /// 敌方单体
        /// </summary>
        Enemy,
    }

    /// <summary>
    /// 阵营标识
    /// </summary>
    public enum GroupTag
    {
        /// <summary>
        /// 英雄
        /// </summary>
        Hero,
        /// <summary>
        /// 怪兽
        /// </summary>
        Monster,
    }

    /// <summary>
    /// 战斗类动作接口
    /// </summary>
    public interface IBattleObjectAction
    {
        void SendCommand(ICommand command);
    }

    public class SkillsAttribute
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 作用值
        /// </summary>
        public int Value;
        /// <summary>
        /// 作用值数量
        /// </summary>
        public int ValueEffectNum;
        /// <summary>
        /// 作用对象
        /// </summary>
        public EffectGroup EffectGroup;
        /// <summary>
        /// 类型
        /// </summary>
        public BuffType BuffType;

        public SkillsAttribute(string name, int value, int valueEffectNum, EffectGroup effectGroup, BuffType buffType)
        {
            Name = name;
            Value = value;
            ValueEffectNum = valueEffectNum;
            EffectGroup = effectGroup;
            BuffType = buffType;
        }
    }

    /// <summary>
    /// 战斗对象抽象类
    /// </summary>
    public class BattleObjects : BaseAttribute, IReciver, IBattleObjectAction
    {
        /// <summary>
        /// 位置索引
        /// </summary>
        public int Index;

        /// <summary>
        /// 阵营
        /// </summary>
        public GroupTag Group;

        /// <summary>
        /// 对象是否已经死亡
        /// </summary>
        public bool IsDead
        {
            get { return Hp == 0; }
        }

        /// <summary>
        /// 普通攻击
        /// </summary>
        public SkillsAttribute NormalAttack { get; protected set; }
        /// <summary>
        /// 特殊攻击
        /// </summary>
        public SkillsAttribute StrongAttack { get; protected set; }
        /// <summary>
        /// 绝招
        /// </summary>
        public SkillsAttribute SpecialAttack { get; protected set; }

        /// <summary>
        /// 中介者实例get/set
        /// </summary>
        public IMediator InsBattleController
        {
            get { return BattleController; }
            set { BattleController = value; }
        }

        /// <summary>
        /// 中介者实例
        /// </summary>
        protected IMediator BattleController;


        /// <summary>
        /// 收到命令通知
        /// </summary>
        /// <param name="command">收到的命令</param>
        public void OnReciveCommand(ICommand command)
        {
            if (command.GetType() == typeof(BattleCommand))
            {
                DoBattleCommand(command);
            }
        }

        /// <summary>
        /// 响应战斗类命令
        /// </summary>
        /// <param name="command">收到的命令</param>
        private void DoBattleCommand(ICommand command)
        {
            BattleCommand battleCommand = command as BattleCommand;
            if (battleCommand == null)
            {
                return;
            }

            switch (battleCommand.BuffType)
            {
                case BuffType.Damage:
                    DoDamageCommand(battleCommand);
                    break;
                case BuffType.Cure:
                    DoCureCommand(battleCommand);
                    break;
                case BuffType.Revive:
                    DoReviveCommand(battleCommand);
                    break;
            }

        }

        /// <summary>
        /// 响应伤害类命令
        /// </summary>
        /// <param name="command">收到的命令</param>
        protected void DoDamageCommand(BattleCommand command)
        {
            if (IsDead)
            {
                return;
            }

            BattleObjects sender = command.Sender as BattleObjects;

            if (sender == null)
            {
                return;
            }

            int finalDamage = sender.Attack + command.Value - Defend;
            finalDamage = finalDamage < 1 ? 1 : finalDamage;

            Console.WriteLine(Name + " 受到 " + finalDamage + " 点伤害");
            Hp = Hp - finalDamage;
            Hp = Hp < 0 ? 0 : Hp;

            if (IsDead)
            {
                Console.WriteLine(Name + " 死亡 ");
            }
        }

        /// <summary>
        /// 响应治疗类命令
        /// </summary>
        /// <param name="command">收到的命令</param>
        protected void DoCureCommand(BattleCommand command)
        {
            if (IsDead)
            {
                return;
            }

            BattleObjects sender = command.Sender as BattleObjects;

            if (sender == null)
            {
                return;
            }

            int finalHpAdd = sender.Attack / 2 + command.Value;

            Console.WriteLine(Name + " 获得 " + finalHpAdd + " 点治疗");
            Hp = Hp + finalHpAdd;
            Hp = Hp > MaxHp ? MaxHp : Hp;
        }

        /// <summary>
        /// 响应复活类命令
        /// </summary>
        /// <param name="command">收到的命令</param>
        protected void DoReviveCommand(BattleCommand command)
        {
            if (!IsDead)
            {
                return;
            }

            BattleObjects sender = command.Sender as BattleObjects;

            if (sender == null)
            {
                return;
            }

            int finalHpAdd = sender.Attack / 2 + command.Value;

            Hp = Hp + finalHpAdd;
            Hp = Hp > MaxHp ? MaxHp : Hp;

            Console.WriteLine(Name + " 复活!获得 " + finalHpAdd + " 点治疗");
        }

        //public void OnReciveUndoCommand(ICommand command){}
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxHp">最大Hp</param>
        /// <param name="attack">攻击力</param>
        /// <param name="defend">防御力</param>
        /// <param name="name">名字</param>
        /// <param name="groupTag">阵营</param>
        /// <param name="normalSkill">普通攻击</param>
        /// <param name="strongSkill">强力攻击</param>
        /// <param name="specialSkill">特殊攻击</param>
        protected BattleObjects(int maxHp, int attack, int defend, string name, GroupTag groupTag, SkillsAttribute normalSkill = null, SkillsAttribute strongSkill = null, SkillsAttribute specialSkill = null)
            : base(maxHp, attack, defend, name)
        {
            NormalAttack = normalSkill;
            StrongAttack = strongSkill;
            SpecialAttack = specialSkill;
            Group = groupTag;
        }

        /// <summary>
        /// 发送命令指令
        /// </summary>
        /// <param name="command">发送的命令</param>
        public void SendCommand(ICommand command)
        {
            if (IsDead)
            {
                return;
            }

            BattleController.Command(command);
        }
    }

    /// <summary>
    /// 剑士实体对象
    /// </summary>
    public class Saber : BattleObjects
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Saber()
            : base(200, 20, 15, "剑士", GroupTag.Hero)
        {
            NormalAttack = new SkillsAttribute("剑击", 10, 1, EffectGroup.Enemy, BuffType.Damage);
            StrongAttack = new SkillsAttribute("强力剑击", 20, 1, EffectGroup.Enemy, BuffType.Damage);
            SpecialAttack = new SkillsAttribute("无双剑击", 50, 3, EffectGroup.Enemy, BuffType.Damage);
        }
    }

    /// <summary>
    /// 弓兵实体对象
    /// </summary>
    public class Archer : BattleObjects
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Archer()
            : base(150, 15, 10, "弓兵", GroupTag.Hero)
        {
            NormalAttack = new SkillsAttribute("射击", 5, 1, EffectGroup.Enemy, BuffType.Damage);
            StrongAttack = new SkillsAttribute("齐射", 10, 2, EffectGroup.Enemy, BuffType.Damage);
            SpecialAttack = new SkillsAttribute("飞蝗", 15, 3, EffectGroup.Enemy, BuffType.Damage);
        }
    }

    /// <summary>
    /// 术士实体对象
    /// </summary>
    public class Caster : BattleObjects
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Caster()
            : base(100, 50, 5, "术士", GroupTag.Hero)
        {
            NormalAttack = new SkillsAttribute("火球术", 30, 1, EffectGroup.Enemy, BuffType.Damage);
            StrongAttack = new SkillsAttribute("治疗术", 50, 1, EffectGroup.Self, BuffType.Cure);
            SpecialAttack = new SkillsAttribute("复活术", 100, 1, EffectGroup.Self, BuffType.Revive);
        }
    }

    /// <summary>
    /// 哥布林士兵实体对象
    /// </summary>
    public class GoblinSoldier : BattleObjects
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GoblinSoldier()
            : base(100, 15, 5, "哥布林士兵", GroupTag.Monster)
        {
            NormalAttack = new SkillsAttribute("抓挠", 10, 1, EffectGroup.Enemy, BuffType.Damage);
            StrongAttack = new SkillsAttribute("进食", 30, 1, EffectGroup.Self, BuffType.Cure);
            SpecialAttack = new SkillsAttribute("重击", 20, 1, EffectGroup.Enemy, BuffType.Damage);
        }
    }

    /// <summary>
    /// 哥布林祭司实体对象
    /// </summary>
    public class GoblinPontifex : BattleObjects
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GoblinPontifex()
            : base(150, 50, 5, "哥布林祭司", GroupTag.Monster)
        {
            NormalAttack = new SkillsAttribute("火球术", 30, 1, EffectGroup.Enemy, BuffType.Damage);
            StrongAttack = new SkillsAttribute("神秘仪式", 50, 3, EffectGroup.Enemy, BuffType.Cure);
            SpecialAttack = new SkillsAttribute("黑魔法", 50, 3, EffectGroup.Enemy, BuffType.Damage);
        }
    }

    /// <summary>
    /// 哥布林王实体对象
    /// </summary>
    public class GoblinKing : BattleObjects
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GoblinKing()
            : base(400, 20, 20, "哥布林王", GroupTag.Monster)
        {
            NormalAttack = new SkillsAttribute("扫荡", 10, 3, EffectGroup.Enemy, BuffType.Damage);
            StrongAttack = new SkillsAttribute("强袭", 30, 1, EffectGroup.Enemy, BuffType.Damage);
            SpecialAttack = new SkillsAttribute("高级进食", 100, 1, EffectGroup.Self, BuffType.Cure);
        }
    }

    /// <summary>
    /// AI策略接口
    /// </summary>
    public interface IAiStrategy
    {
        /// <summary>
        /// 获取一条AI策略指令
        /// </summary>
        /// <returns>返回的指令</returns>
        ICommand GetStrategy();
    }

    /// <summary>
    /// AI策略抽象类
    /// </summary>
    public abstract class AiStrategy : IAiStrategy
    {
        /// <summary>
        /// 队友对象
        /// </summary>
        protected BattleObjects[] TeamMate;
        /// <summary>
        /// 敌人对象
        /// </summary>
        protected BattleObjects[] Enemy;
        /// <summary>
        /// 命令下达者
        /// </summary>
        protected BattleObjects Commander;
        /// <summary>
        /// 命令接收者
        /// </summary>
        protected IReciver Reciver;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="teamMate">队友对象</param>
        /// <param name="enemy">敌人对象</param>
        /// <param name="commander">命令下达者</param>
        /// <param name="reciver">命令接收者</param>
        protected AiStrategy(BattleObjects[] teamMate, BattleObjects[] enemy,
             BattleObjects commander, IReciver reciver)
        {
            TeamMate = teamMate;
            Enemy = enemy;
            Commander = commander;
            Reciver = reciver;
        }

        /// <summary>
        /// 获取一条AI策略指令
        /// </summary>
        /// <returns>返回的指令</returns>
        public abstract ICommand GetStrategy();

        /// <summary>
        /// 获取生命值最低的对象
        /// </summary>
        /// <returns></returns>
        protected BattleObjects GetHpMinTarget(BattleObjects[] datas)
        {
            if (datas == null)
            {
                return null;
            }

            BattleObjects battleObjects = null;

            for (int i = 0; i < datas.Length; ++i)
            {
                if (datas[i] != null && !datas[i].IsDead)
                {
                    if (battleObjects == null)
                        battleObjects = datas[i];

                    if (battleObjects.Hp > datas[i].Hp)
                        battleObjects = datas[i];
                }
            }

            return battleObjects;
        }

        /// <summary>
        /// 获取前排对象
        /// </summary>
        /// <returns></returns>
        protected BattleObjects GetFrontTarget(BattleObjects[] datas)
        {
            if (datas == null)
            {
                return null;
            }
            for (int i = 0; i < datas.Length; ++i)
            {
                if (datas[i] != null && !datas[i].IsDead)
                    return datas[i];
            }

            return null;
        }

        /// <summary>
        /// 获取阵亡对象
        /// </summary>
        /// <returns></returns>
        protected BattleObjects GetDeadTarget(BattleObjects[] datas)
        {
            if (datas == null)
            {
                return null;
            }
            for (int i = 0; i < datas.Length; ++i)
            {
                if (datas[i] != null && datas[i].IsDead)
                    return datas[i];
            }

            return null;
        }
    }

    /// <summary>
    /// 剑士的AI
    /// </summary>
    public class SaberAi : AiStrategy
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="teamMate">队友对象</param>
        /// <param name="enemy">敌人对象</param>
        /// <param name="commander">命令下达者</param>
        /// <param name="reciver">命令接收者</param>
        public SaberAi(BattleObjects[] teamMate, BattleObjects[] enemy,
             BattleObjects commander, IReciver reciver)
            : base(teamMate, enemy, commander, reciver)
        {
        }

        /// <summary>
        /// 获取一条AI策略指令
        /// </summary>
        /// <returns>返回的指令</returns>
        public override ICommand GetStrategy()
        {
            if (Commander == null)
            {
                return null;
            }

            int point = new Random().Next(0, 100);
            BattleObjects target;
            SkillsAttribute skills;

            //60%的几率进行普通攻击，攻击对象为队伍前排
            if (point <= 60)
            {
                target = GetFrontTarget(Enemy);
                skills = Commander.NormalAttack;
            }
            //30%的几率进行强力攻击，攻击敌方Hp最少的对象
            else if (point <= 90)
            {
                target = GetHpMinTarget(Enemy);
                skills = Commander.StrongAttack;
            }
            //10%的几率进行特殊攻击，攻击敌方Hp最少的对象
            else
            {
                target = GetHpMinTarget(Enemy);
                skills = Commander.SpecialAttack;
            }

            if (target == null || skills == null)
            {
                return null;
            }

            BattleCommand battleCommand = new BattleCommand(Commander, Reciver, skills.Value, skills.ValueEffectNum,
                skills.Name, skills.BuffType, skills.EffectGroup, target.Index);


            return battleCommand;
        }
    }

    /// <summary>
    /// 哥布林士兵的AI
    /// </summary>
    public class GoblinSoldierAi : AiStrategy
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="teamMate">队友对象</param>
        /// <param name="enemy">敌人对象</param>
        /// <param name="commander">命令下达者</param>
        /// <param name="reciver">命令接收者</param>
        public GoblinSoldierAi(BattleObjects[] teamMate, BattleObjects[] enemy,
             BattleObjects commander, IReciver reciver)
            : base(teamMate, enemy, commander, reciver)
        {
        }

        /// <summary>
        /// 获取一条AI策略指令
        /// </summary>
        /// <returns>返回的指令</returns>
        public override ICommand GetStrategy()
        {
            if (Commander == null)
            {
                return null;
            }

            int point = new Random().Next(0, 100);
            BattleObjects target;
            SkillsAttribute skills;

            //50%的几率进行普通攻击，攻击对象为队伍前排
            if (point <= 50)
            {
                target = GetFrontTarget(Enemy);
                skills = Commander.NormalAttack;
            }
            //45%的几率进行进食功能，回复自己的Hp
            else if (point <= 95)
            {
                target = Commander;
                skills = Commander.StrongAttack;
            }
            //5%的几率进行特殊攻击，攻击敌方Hp最少的对象
            else
            {
                target = GetHpMinTarget(Enemy);
                skills = Commander.SpecialAttack;
            }

            if (target == null || skills == null)
            {
                return null;
            }

            BattleCommand battleCommand = new BattleCommand(Commander, Reciver, skills.Value, skills.ValueEffectNum,
                skills.Name, skills.BuffType, skills.EffectGroup, target.Index);


            return battleCommand;
        }
    }

    /// <summary>
    /// AI装饰类
    /// </summary>
    public class Ai : IBattleObjectAction
    {
        /// <summary>
        /// 装饰对象
        /// </summary>
        protected IBattleObjectAction DecoratorBattleObjects;

        /// <summary>
        /// AI策略
        /// </summary>
        protected IAiStrategy Strategy;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="battleObjects">装饰对象</param>
        /// <param name="strategy">AI策略</param>
        public Ai(IBattleObjectAction battleObjects, IAiStrategy strategy = null)
        {
            DecoratorBattleObjects = battleObjects;
            Strategy = strategy;
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="command">指令</param>
        public void SendCommand(ICommand command = null)
        {
            if (Strategy == null)
            {
                return;
            }

            ICommand stg = Strategy.GetStrategy();

            if (stg == null)
            {
                return;
            }

            DecoratorBattleObjects.SendCommand(Strategy.GetStrategy());
        }
    }

    class Program
    {
        public static void DisPlayState(BattleObjects[] data)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                if (data[i] != null)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("名字: ");
                    builder.Append(data[i].Name);
                    builder.Append(" 攻击: ");
                    builder.Append(data[i].Attack);
                    builder.Append(" 防御: ");
                    builder.Append(data[i].Defend);
                    builder.Append(" 生命: ");
                    builder.Append(data[i].Hp);
                    builder.Append("/");
                    builder.Append(data[i].MaxHp);
                    Console.WriteLine(builder.ToString());
                }
            }
        }
        static void Main(string[] args)
        {
            BattleMannager battleMannager = new BattleMannager();

            BattleObjects saber = new Saber();
            BattleObjects archer = new Archer();
            BattleObjects goblinSoldier1 = new GoblinSoldier();
            BattleObjects goblinSoldier2 = new GoblinSoldier();
            BattleObjects goblinSoldier3 = new GoblinSoldier();
            saber.InsBattleController = battleMannager;
            archer.InsBattleController = battleMannager;
            goblinSoldier1.InsBattleController = battleMannager;
            goblinSoldier2.InsBattleController = battleMannager;
            goblinSoldier3.InsBattleController = battleMannager;

            Ai saberAi = new Ai(saber, new SaberAi(battleMannager.HeroObjectses,
                 battleMannager.MonsterObjectses, saber, battleMannager));

            Ai goblinSoldierAi1 = new Ai(goblinSoldier1, new GoblinSoldierAi(battleMannager.MonsterObjectses,
                 battleMannager.HeroObjectses, goblinSoldier1, battleMannager));

            Ai goblinSoldierAi2 = new Ai(goblinSoldier2, new GoblinSoldierAi(battleMannager.MonsterObjectses,
                 battleMannager.HeroObjectses, goblinSoldier2, battleMannager));

            Ai goblinSoldierAi3 = new Ai(goblinSoldier3, new GoblinSoldierAi(battleMannager.MonsterObjectses,
                 battleMannager.HeroObjectses, goblinSoldier3, battleMannager));

            battleMannager.SetHero(saber, 0);
            battleMannager.SetHero(archer, 1);
            battleMannager.SetMonster(goblinSoldier1, 0);
            battleMannager.SetMonster(goblinSoldier2, 1);
            battleMannager.SetMonster(goblinSoldier3, 2);

            int turn = 1;

            BattleState state = BattleState.Battling;
            while (state == BattleState.Battling)
            {
                Console.Clear();
                Console.WriteLine("第" + turn + "回合");
                saberAi.SendCommand();
                archer.SendCommand(new BattleCommand(archer, battleMannager, archer.StrongAttack.Value,
                    archer.StrongAttack.ValueEffectNum, archer.StrongAttack.Name, archer.StrongAttack.BuffType,
                    archer.StrongAttack.EffectGroup, 1));
                goblinSoldierAi1.SendCommand();
                goblinSoldierAi2.SendCommand();
                goblinSoldierAi3.SendCommand();
                state = battleMannager.RunTurn();
                DisPlayState(battleMannager.HeroObjectses);
                DisPlayState(battleMannager.MonsterObjectses);
                turn++;
                Thread.Sleep(3000);
            }

            Console.WriteLine("战斗结束");
        }
    }
}
