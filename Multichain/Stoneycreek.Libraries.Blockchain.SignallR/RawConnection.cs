using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;

using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Hosting;

namespace Stoneycreek.Libraries.Blockchain.SignallR
{
    public class RawConnection : PersistentConnection
    {
        private static readonly ConcurrentDictionary<string, string> users = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, string> clients = new ConcurrentDictionary<string, string>();

        protected override Task OnConnected(IRequest request, string connectionId)
        {
            Cookie userNameCookie;
            if (request.Cookies.TryGetValue("user", out userNameCookie) &&
                userNameCookie != null)
            {
                clients[connectionId] = userNameCookie.Value;
                users[userNameCookie.Value] = connectionId;
            }

            string clientIp = GetClientIP(request);

            string user = GetUser(connectionId);

            return Groups.Add(connectionId, "foo")
                .ContinueWith(_ => Connection.Broadcast(DateTime.Now + ": " + user + " joined from " + clientIp)).Unwrap();
        }

        protected override Task OnReconnected(IRequest request, string connectionId)
        {
            string user = GetUser(connectionId);

            return Connection.Broadcast(DateTime.Now + ": " + user + " reconnected");
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {

            string ignored;
            users.TryRemove(connectionId, out ignored);

            // string suffix = stopCalled ? "cleanly" : "uncleanly";
            return Connection.Broadcast(DateTime.Now + ": " + GetUser(connectionId) + " disconnected " + "bla");
        }

        //protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        //{
        //    string ignored;
        //    _users.TryRemove(connectionId, out ignored);

        //    string suffix = stopCalled ? "cleanly" : "uncleanly";
        //    return Connection.Broadcast(DateTime.Now + ": " + GetUser(connectionId) + " disconnected " + suffix);
        //}

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var message = JsonConvert.DeserializeObject<Message>(data);

            switch (message.Type)
            {
                case MessageType.Broadcast:
                    this.BroAddParameters(connectionId, message.Value);
                    break;
                case MessageType.BroadcastExceptMe:
                    this.BroAddParameters(connectionId, message.Value, connectionId);
                    break;
                case MessageType.Send:
                    Connection.Send(connectionId, new
                    {
                        type = MessageType.Send.ToString(),
                        from = GetUser(connectionId),
                        data = message.Value
                    });
                    break;
                case MessageType.Join:
                    string name = message.Value;
                    clients[connectionId] = name;
                    users[name] = connectionId;
                    Connection.Send(connectionId, new
                    {
                        type = MessageType.Join.ToString(),
                        data = message.Value
                    });
                    break;
                case MessageType.PrivateMessage:
                    var parts = message.Value.Split('|');
                    string user = parts[0];
                    string msg = parts[1];
                    string id = GetClient(user);
                    Connection.Send(id, new
                    {
                        from = GetUser(connectionId),
                        data = msg
                    });
                    break;
                case MessageType.AddToGroup:
                    Groups.Add(connectionId, message.Value);
                    break;
                case MessageType.RemoveFromGroup:
                    Groups.Remove(connectionId, message.Value);
                    break;
                case MessageType.SendToGroup:
                    var parts2 = message.Value.Split('|');
                    string groupName = parts2[0];
                    string val = parts2[1];
                    Groups.Send(groupName, val);
                    break;
            }

            return base.OnReceived(request, connectionId, data);
        }

        public new Task ProcessRequest(IDictionary<string, object> environment)
        {
            return new Task(() => GetUser(""));
        }
        public override Task ProcessRequest(HostContext context)
        {
            return new Task(() => GetUser(""));
        }

        private string GetUser(string connectionId)
        {
            string user;
            if (!clients.TryGetValue(connectionId, out user))
            {
                return connectionId;
            }
            return user;
        }

        private string GetClient(string user)
        {
            string connectionId;
            if (users.TryGetValue(user, out connectionId))
            {
                return connectionId;
            }
            return null;
        }

        enum MessageType
        {
            Send,
            Broadcast,
            Join,
            PrivateMessage,
            AddToGroup,
            RemoveFromGroup,
            SendToGroup,
            BroadcastExceptMe,
        }

        class Message
        {
            public MessageType Type { get; set; }
            public string Value { get; set; }
        }

        private static string GetClientIP(IRequest request)
        {
            return Get<string>(request.Environment, "server.RemoteIpAddress");
        }

        private static T Get<T>(IDictionary<string, object> env, string key)
        {
            object value;
            return env.TryGetValue(key, out value) ? (T)value : default(T);
        }

        private void BroAddParameters(string connectionId, object message, string connectionID = null)
        {
            Connection.Broadcast(new
            {
                type = MessageType.Broadcast.ToString(),
                from = this.GetUser(connectionId),
                data = message
            }, connectionID);
        }
    }
}
