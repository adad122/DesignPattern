using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//优缺点
//在全面解析完代理模式后，我来分析下其优缺点：

//1 优点
//协调调用者和被调用者，降低了系统的耦合度
//代理对象作为客户端和目标对象之间的中介，起到了保护目标对象的作用

//2 缺点
//由于在客户端和真实主题之间增加了代理对象，因此会造成请求的处理速度变慢；
//实现代理模式需要额外的工作（有些代理模式的实现非常复杂），从而增加了系统实现的复杂度。 

//2. 应用场景
//当需要为一个对象再不同的地址空间提供局部的代表时 
//此时的代理模式称为远程代理：为一个对象在不同的地址空间提供局部代表。

//目的：隐藏一个对象存在于不同地址空间的事实；

//远程机器可能具有更好的计算性能与处理速度，可以快速响应并处理客户端请求。
//当需要创建开销非常大的对象时 
//此时的代理模式称为虚拟代理：通过使用过一个小的对象代理一个大对象。

//目的：减少系统的开销。

//当需要控制对原始对象的访问时 
//此时的代理模式称为保护代理：控制目标对象的访问，给不同用户提供不同的访问权限 

//目的：用来控制对真实对象的访问权限

//当需要在访问对象时附加额外操作时 
//此时的代理模式称为智能引用代理，额外操作包括耗时操作、计算访问次数等等 

//目的：在不影响对象类的情况下，在访问对象时进行更多的操作

//以上是最常用的使用场景，其他还包括：
//防火墙代理：保护目标不让恶意用户靠近
//Cache代理：为结果提供临时的存储空间，以便其他客户端调用 

namespace ProxyPatternApply
{
    //游戏中类似应用之: 聊天翻译
    //游戏中玩家的对话需要进行翻译时，需要向Google翻译之类的的API进行调用，然后根据Google翻译返回的结果进行对应的设置
    //问题1. 游戏中频繁的向Google翻译API请求会增大消耗(Google翻译按字数统计价格)
    //问题2. 国内防火墙对Google翻译的API可能存在屏蔽或者访问耗时过长
    //问题3. 重复翻译的次数和内容太多


    //实际使用 增加翻译代理功能，同时增加中间层翻译服务器
    //本地翻译代理对对应语句的索引id进行记录查找
    //存在本地翻译历史记录->直接进行翻译
    //不存在本地翻译历史记录->向翻译服务器发起请求

    //聊天服务器:根据客户端发送的索引id查看对应的对话
    //对应索引的id的对话已经进行了翻译->直接返回给客户端
    //对应索引的id的对话未进行翻译->(可能存在中间层翻译服务器用于隔离开聊天服务器)向Google翻译API进行请求
    //->获取翻译结果->返回给客户端

    //优点:
    //1. 减少了直接对翻译API的请求次数，降低了重复内容的请求
    //2. 减少了不同地区直接请求翻译API造成的地区性网络差异的影响
    //3. 隔离开游戏直接请求翻译防止被他人破解之后盗用key和secret(游戏服务器只会向聊天服务器发送需要翻译的对话的索引id)

    //缺点:
    //增加了翻译服务器这个中间层，加大了网络开支，同时需要连续的网络请求，出错的可能性增加。

    public delegate void TranslateCallback(string result);

    /// <summary>
    /// 翻译接口
    /// </summary>
    public interface ITranslateContent
    {
        void GetTranslate(string lan, TranslateCallback callback);

        string GetOriginContent();

        long GetId();
    }

    /// <summary>
    /// 翻译对象实体
    /// </summary>
    public class ChatContent : ITranslateContent
    {
        private const string OriginContentMark = "none";
        private readonly long _id;

        private readonly Dictionary<string, string> _content = new Dictionary<string, string>();

        public ChatContent(long id, string content)
        {
            _id = id;
            _content.Add(OriginContentMark, content);
        }
        public void GetTranslate(string lan, TranslateCallback callback)
        {
            if (callback == null)
                return;

            callback(IsLanTranslated(lan) ? _content[lan] : "");
        }

        public string GetOriginContent()
        {
            return _content[OriginContentMark];
        }

        public long GetId()
        {
            return _id;
        }

        public bool IsLanTranslated(string lan)
        {
            return _content.ContainsKey(lan);
        }

        public void AddTranslate(string lan, string content)
        {
            _content[lan] = content;
        }
    }

    /// <summary>
    /// 翻译对象代理
    /// </summary>
    public class ProxyTranslate : ITranslateContent
    {
        private readonly ChatContent _chatContent;

        public ProxyTranslate(long id, string content)
        {
            _chatContent = new ChatContent(id, content);
        }

        public void GetTranslate(string lan, TranslateCallback callback)
        {
            if (callback == null)
                return;

            if (_chatContent.IsLanTranslated(lan))
            {
                Console.WriteLine("直接从本地获取翻译");
                _chatContent.GetTranslate(lan, callback);
            }
            else
            {
                HttpDelegate.TranslateChat(_chatContent.GetId(), lan, delegate(string result)
                {
                    Console.WriteLine("从聊天服务器获取翻译");
                    _chatContent.AddTranslate(lan, result);
                    callback(result);
                });
            }
        }

        public string GetOriginContent()
        {
            return _chatContent.GetOriginContent();
        }

        public long GetId()
        {
            return _chatContent.GetId();
        }
    }

    /// <summary>
    /// 模拟谷歌翻译API
    /// </summary>
    public class GoogleTranslateApi
    {
        private static readonly Dictionary<string, string> Result = new Dictionary<string, string>();

        public static void InitDatabase()
        {
            Result["en_你好"] = "Hello";
            Result["fran_你好"] = "Bonjour";
            Result["hans_你好"] = "你好";

            Result["en_Hello"] = "Hello";
            Result["fran_Hello"] = "Bonjour";
            Result["hans_Hello"] = "你好";

            Result["en_Bonjour"] = "Hello";
            Result["fran_Bonjour"] = "Bonjour";
            Result["hans_Bonjour"] = "你好";


            Result["en_再见"] = "Bye";
            Result["fran_再见"] = "Au revoir";
            Result["hans_再见"] = "再见";

            Result["en_Bye"] = "Bye";
            Result["fran_Bye"] = "Au revoir";
            Result["hans_Bye"] = "再见";

            Result["en_Au revoir"] = "Bye";
            Result["fran_Au revoir"] = "Au revoir";
            Result["hans_Au revoir"] = "再见";
        }

        public static string GetTranslate(string lan, string content)
        {
            Console.WriteLine("Google翻译解析并发回翻译内容");
            StringBuilder key = new StringBuilder();
            key.Append(lan);
            key.Append("_");
            key.Append(content);
            if (Result.ContainsKey(key.ToString()))
                return Result[key.ToString()];

            return "";
        }
    }

    /// <summary>
    /// 聊天服务器
    /// </summary>
    public class ChatServer
    {
        private long _chatId;
        class TranslateData
        {
            public string OriginContent { get; private set; }

            private readonly Dictionary<string, string> _translatedContent = new Dictionary<string, string>();

            public TranslateData(string content)
            {
                OriginContent = content;
            }

            public void AddTranslate(string lan, string content)
            {
                _translatedContent[lan] = content;
            }

            public bool IsTranslated(string lan)
            {
                return _translatedContent.ContainsKey(lan);
            }

            public string GetTranslate(string lan)
            {
                return IsTranslated(lan) ? _translatedContent[lan] : "";
            }
        }

        private readonly Dictionary<long, TranslateData> _chatContent = new Dictionary<long, TranslateData>();

        private readonly TranslateServer _translateServer;

        public static long GetTimestamp()
        {
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);//ToUniversalTime()转换为标准时区的时间,去掉的话直接就用北京时间
            return (long)ts.TotalMilliseconds; //精确到毫秒
            //return (long)ts.TotalSeconds;//获取10位
        }

        public void GetTranslate(long id, string lan, TranslateCallback callback)
        {
            if (callback == null || !_chatContent.ContainsKey(id))
                return;

            TranslateData translateData = _chatContent[id];

            if (!translateData.IsTranslated(lan))
            {
                Console.WriteLine("聊天服务器未记录当前语言的翻译，向翻译服务器发起请求");
                _translateServer.GetTranslate(lan, translateData.OriginContent, delegate(string result)
                    {
                        translateData.AddTranslate(lan, result);
                        callback(result);
                    });
            }
            else
            {
                Console.WriteLine("聊天服务器存在已翻译内容，直接向客户端回复");
                callback(translateData.GetTranslate(lan));
            }
        }

        public ChatServer(TranslateServer translateServer)
        {
            _translateServer = translateServer;
            _chatId = GetTimestamp();
        }

        public long OnReciveChatMessage(string content)
        {
            _chatContent.Add(_chatId, new TranslateData(content));
            return _chatId++;
        }
    }

    /// <summary>
    /// 翻译服务器
    /// </summary>
    public class TranslateServer
    {
        public void GetTranslate(string lan, string content, TranslateCallback callback)
        {
            if (callback == null)
                return;

            Console.WriteLine("翻译服务器向Google翻译API发起翻译请求");
            callback(GoogleTranslateApi.GetTranslate(lan, content));
        }
    }

    /// <summary>
    /// 模拟游戏中Http请求
    /// </summary>
    public class HttpDelegate
    {
        public static ChatServer ChatServer;

        public static void TranslateChat(long id, string lan, TranslateCallback callback)
        {
            ChatServer.GetTranslate(id, lan, callback);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GoogleTranslateApi.InitDatabase();

            //服务器端
            TranslateServer translateServer = new TranslateServer();
            ChatServer chatServer = new ChatServer(translateServer);

            //客户端
            HttpDelegate.ChatServer = chatServer;

            string en = "en";
            string hans = "hans";
            string fran = "fran";

            string text1 = "你好";
            string text2 = "Bye";

            Console.WriteLine("-------------------------------------------------------------------------");
            long content1Id = chatServer.OnReciveChatMessage(text1);
            Console.WriteLine("玩家A: " + text1 + " 对话编号: " + content1Id);

            ITranslateContent content1 = new ProxyTranslate(content1Id, text1);
            Console.WriteLine("---------------玩家1请求翻译 " + content1Id + "的" + en + "---------------");
            content1.GetTranslate(en, delegate(string result)
            {
                Console.WriteLine("得到" + content1.GetOriginContent() + "的" + en + "翻译: " + result);
            });
            Console.WriteLine("---------------玩家1请求翻译 " + content1Id + "的" + hans + "---------------");
            content1.GetTranslate(hans, delegate(string result)
            {
                Console.WriteLine("得到" + content1.GetOriginContent() + "的" + hans + "翻译: " + result);
            });

            Console.WriteLine("-------------------------------------------------------------------------");
            long content2Id = chatServer.OnReciveChatMessage(text2);
            Console.WriteLine("玩家B: " + text2 + " 对话编号: " + content2Id);

            ITranslateContent content2 = new ProxyTranslate(content2Id, text2);
            Console.WriteLine("---------------玩家2请求翻译 " + content2Id + "的" + fran + "---------------");
            content2.GetTranslate(fran, delegate(string result)
            {
                Console.WriteLine("得到" + content2.GetOriginContent() + "的" + fran + "翻译: " + result);
            });
            Console.WriteLine("---------------玩家2请求翻译 " + content2Id + "的" + fran + "---------------");
            content2.GetTranslate(fran, delegate(string result)
            {
                Console.WriteLine("得到" + content2.GetOriginContent() + "的" + fran + "翻译: " + result);
            });


            ITranslateContent content3 = new ProxyTranslate(content2Id, text2);
            Console.WriteLine("---------------玩家3请求翻译 " + content2Id + "的" + fran + "---------------");
            content3.GetTranslate(fran, delegate(string result)
            {
                Console.WriteLine("得到" + content3.GetOriginContent() + "的" + fran + "翻译: " + result);
            });

            ITranslateContent content4 = new ProxyTranslate(content1Id, text1);
            Console.WriteLine("---------------玩家4请求翻译 " + content1Id + "的" + en + "---------------");
            content4.GetTranslate(en, delegate(string result)
            {
                Console.WriteLine("得到" + content4.GetOriginContent() + "的" + en + "翻译: " + result);
            });
        }
    }
}
