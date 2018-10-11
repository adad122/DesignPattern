using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainofResponsibilityPatternApply
{
    public enum ChatType
    {
        Global,
        Alliance,
        Private,
    }

    public abstract class ChatFilter
    {
        protected ChatType ChatType;

        protected ChatFilter NextFilter;

        protected ChatServer Server;

        protected ChatFilter(ChatServer server)
        {
            Server = server;
            NextFilter = null;
        }

        public void SetNextFilter(ChatFilter chatFilter)
        {
            NextFilter = chatFilter;
        }

        public void PassMessage(ChatType type, string message, string ipAddress, string reciver)
        {
            if (type == ChatType)
            {
                PopMessageToClient(message, ipAddress, reciver);
            }
            else if(NextFilter != null)
            {
                NextFilter.PassMessage(type, message, ipAddress, reciver);
            }
        }

        protected abstract void PopMessageToClient(string msessage, string ipAddress, string reciver);
    }

    public class GlobalChatFilter : ChatFilter
    {
        public GlobalChatFilter(ChatServer server) : base(server)
        {
            ChatType = ChatType.Global;
        }

        protected override void PopMessageToClient(string msessage, string ipAddress, string reciver)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(ipAddress);
            builder.Append(" said: ");
            builder.Append(msessage);

            foreach (KeyValuePair<string, ChatClient> client in Server.Clients)
            {
                client.Value.OnReciveMessage(ChatType, builder.ToString());
            }
        }
    }

    public class AllianceChatFilter : ChatFilter
    {
        public AllianceChatFilter(ChatServer server)
            : base(server)
        {
            ChatType = ChatType.Alliance;
        }

        protected override void PopMessageToClient(string msessage, string ipAddress, string reciver)
        {
            ChatClient speaker = Server.Clients[ipAddress];

            if (speaker.AllianceUid == 0)
            {
                return;
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(ipAddress);
            builder.Append(" said: ");
            builder.Append(msessage);

            foreach (KeyValuePair<string, ChatClient> client in Server.Clients)
            {
                if (client.Value.AllianceUid == speaker.AllianceUid)
                {
                    client.Value.OnReciveMessage(ChatType, builder.ToString());
                }
            }
        }
    }

    public class PrivateChatFilter : ChatFilter
    {
        public PrivateChatFilter(ChatServer server)
            : base(server)
        {
            ChatType = ChatType.Private;
        }

        protected override void PopMessageToClient(string msessage, string ipAddress, string reciver)
        {
            if (!Server.Clients.ContainsKey(reciver))
            {
                return;
            }

            ChatClient speakerClient = Server.Clients[ipAddress];
            ChatClient reciverClient = Server.Clients[reciver];

            StringBuilder builder = new StringBuilder();
            builder.Append(ipAddress);
            builder.Append(" said to ");
            builder.Append(reciver);
            builder.Append(": ");
            builder.Append(msessage);

            speakerClient.OnReciveMessage(ChatType, builder.ToString());
            reciverClient.OnReciveMessage(ChatType, builder.ToString());
        }
    }

    public class ChatServer
    {
        private readonly Dictionary<string, ChatClient> _clients = new Dictionary<string, ChatClient>();

        private readonly ChatFilter _chatFilter;

        public Dictionary<string, ChatClient> Clients
        {
            get { return _clients; }
        }

        public ChatServer()
        {
            ChatFilter globalFilter = new GlobalChatFilter(this);
            ChatFilter allianceFilter = new AllianceChatFilter(this);
            ChatFilter privateFilter = new PrivateChatFilter(this);

            globalFilter.SetNextFilter(allianceFilter);
            allianceFilter.SetNextFilter(privateFilter);

            _chatFilter = globalFilter;
        }

        public void OnReciveMessage(ChatType type, string message, string ipAddress, string reciver = null)
        {
            if (!_clients.ContainsKey(ipAddress))
            {
                return;
            }

            _chatFilter.PassMessage(type, message, ipAddress, reciver);
        }

        public void RegistClient(ChatClient chatClient)
        {
            _clients.Add(chatClient.IpAddress, chatClient);
        }

        public void RemoveClient(string ipAddress)
        {
            _clients.Remove(ipAddress);
        }
    }
    public class ChatClient
    {
        private readonly string _ipAddress;

        private ChatServer _server;

        private readonly int _allianceUid;

        private readonly List<string> _messagesList = new List<string>(); 

        public ChatClient(string ipAddress, int allianceUid = 0)
        {
            _ipAddress = ipAddress;
            _allianceUid = allianceUid;
        }

        public string IpAddress
        {
            get { return _ipAddress; }
        }

        public int AllianceUid
        {
            get { return _allianceUid; }
        }

        public void Connect(ChatServer server)
        {
            if (server == null)
            {
                Console.WriteLine("ChatServer is null.");
                return;
            }
            _server = server;
            _server.RegistClient(this);
        }

        public void Disconnect()
        {
            if (_server == null)
            {
                return;
            }

            _server.RemoveClient(_ipAddress);
        }

        public void SendMessage(ChatType type, string msessage, string reciver = null)
        {
            if (_server == null)
            {
                Console.WriteLine(IpAddress + "not connect to server yet.");
                return;
            }

            _server.OnReciveMessage(type, msessage, _ipAddress, reciver);
        }

        public void OnReciveMessage(ChatType type, string message)
        {
            StringBuilder builder = new StringBuilder();

            switch (type)
            {
                case ChatType.Global:
                    builder.Append("[Global]");
                    break;
                case ChatType.Alliance:
                    builder.Append("[Alliance]");
                    break;
                case ChatType.Private:
                    builder.Append("[Private]");
                    break;
            }
            builder.Append(message);
            _messagesList.Add(builder.ToString());
        }

        public void ShowMessages()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine(IpAddress + " recived message:");
            foreach (string s in _messagesList)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("--------------------------------------------");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ChatServer chatServer = new ChatServer();
            ChatClient client1 = new ChatClient("Alex");
            ChatClient client2 = new ChatClient("Jack", 1);
            ChatClient client3 = new ChatClient("Rose", 1);

            client1.Connect(chatServer);
            client1.SendMessage(ChatType.Global, "Hello");
            client2.Connect(chatServer);
            client2.SendMessage(ChatType.Global, "Hi~");
            client3.Connect(chatServer);
            client3.SendMessage(ChatType.Alliance, "This is Our Alliance.");
            client1.SendMessage(ChatType.Private, "Hi Rose. Nice to meet you.", client3.IpAddress);
            client2.SendMessage(ChatType.Global, "GoodBye Everyone.");
            client2.Disconnect();
            client1.SendMessage(ChatType.Global, "GoodBye Jack.");

            client1.ShowMessages();
            client2.ShowMessages();
            client3.ShowMessages();
        }
    }
}
