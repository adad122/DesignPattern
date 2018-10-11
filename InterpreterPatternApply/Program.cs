using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterPatternApply
{
    //几乎用不到，随便写一点
    //1	Context	Context	上下文类
    //2	Expression	AbstractExpression	抽象表达式
    //3	Constant	TerminalExpression	常量，是终结符表达式
    //4	Variable	TerminalExpression	变量，是终结符表达式
    //5	Add	NonterminalExpression	加法，是非终结符表达式
    //6	Subtract	NonterminalExpression	减法，是非终结符表达式
    //7	Multiply	NonterminalExpression	乘法，是非终结符表达式
    //8	Division	NonterminalExpression	除法，是非终结符表达式
    //9	InterpreterMain	客户端	演示调用

    public class Context
    {
        private Dictionary<Variable, int> _map = new Dictionary<Variable, int>();

        public void Assign(Variable key, int value)
        {
            _map.Add(key, value);
        }

        public int GetValue(Variable key)
        {
            if (_map.ContainsKey(key))
            {
                return _map[key];
            }

            return 0;
        }
    }

    public abstract class Expression
    {
        public abstract int Interpret(Context context);
    }

    public class Constant : Expression
    {
        private int _i;

        public Constant(int i)
        {
            _i = i;
        }

        public override int Interpret(Context context)
        {
            return _i;
        }
    }

    public class Variable : Expression
    {
        public override int Interpret(Context context)
        {
            return context.GetValue(this);
        }
    }

    public class Add : Expression
    {
        private readonly Expression _augend;
        private readonly Expression _addend;

        public Add(Expression augend, Expression addend)
        {
            _augend = augend;
            _addend = addend;
        }
        public override int Interpret(Context context)
        {
            return _augend.Interpret(context) + _addend.Interpret(context);
        }
    }

    public class Subtract : Expression
    {
        private readonly Expression _minuend;
        private readonly Expression _subtrahend;

        public Subtract(Expression minuend, Expression subtrahend)
        {
            _minuend = minuend;
            _subtrahend = subtrahend;
        }
        public override int Interpret(Context context)
        {
            return _minuend.Interpret(context) - _subtrahend.Interpret(context);
        }
    }

    public class Multiply : Expression
    {
        private readonly Expression _multiplicand;
        private readonly Expression _multiplier;

        public Multiply(Expression multiplicand, Expression multiplier)
        {
            _multiplicand = multiplicand;
            _multiplier = multiplier;
        }
        public override int Interpret(Context context)
        {
            return _multiplicand.Interpret(context) * _multiplier.Interpret(context);
        }
    }

    public class Division : Expression
    {
        private readonly Expression _dividend;
        private readonly Expression _divisor;

        public Division(Expression dividend, Expression divisor)
        {
            _dividend = dividend;
            _divisor = divisor;
        }
        public override int Interpret(Context context)
        {
            try
            {
                return _dividend.Interpret(context)/_divisor.Interpret(context);
            }
            catch (Exception)
            {
                Console.WriteLine("Divisor can't be 0");
                throw;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            Variable a = new Variable();
            Variable b = new Variable();
            Constant c = new Constant(2);

            context.Assign(a, 6);
            context.Assign(b, 3);

            Expression expression = new Add(new Division(a, b), c);
            Console.WriteLine("(6 / 3) + 2 = " + expression.Interpret(context));
        }
    }
}
