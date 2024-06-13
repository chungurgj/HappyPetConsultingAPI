using ChatService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace ChatService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _botUser;
        private readonly IDictionary<string, UserConnection> _connections;

        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            _botUser = "Бот";
            _connections = connections;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                Clients.Group(userConnection.Room)
                    .SendAsync("ReceiveMessage", _botUser,
                    $"{userConnection.User} го напушти разговорот");

            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {

                await Clients.Group(userConnection.Room)
                    .SendAsync("ReceiveMessage", userConnection.User, message);
            }
        }
        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            _connections[Context.ConnectionId] = userConnection;


            await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser,
                $"{userConnection.User} се вклучи во разговорот");

         

        }

        public async Task EndConsultation(Guid consId)
        {
            var connectionId = Context.ConnectionId;

            if (_connections.TryGetValue(connectionId, out var roomName))
            {
                var userName = roomName?.User; // Add null check here

                _connections.Remove(connectionId);

                // If there are no more connections in the group, remove the group from the dictionary
                if (!_connections.Values.Any(c => c.Room == roomName?.Room)) // Add null check here
                {
                    _connections.Remove(roomName?.Room); // Add null check here


                }

                await Clients.Group(roomName?.Room).SendAsync("ReceiveMessage", _botUser,
                    $"{userName} ја заврши консултацијата.");

                await Clients.Group(roomName?.Room).SendAsync("EndConsultation", consId);
                await Clients.Group(roomName?.Room).SendAsync("RefreshTextCons");

            }
        }


        public async Task LeaveConsultation()
        {
            var connectionId = Context.ConnectionId;

            if(_connections.TryGetValue(connectionId,out var RoomName))
            {
                var userName = RoomName.User;

                if(!_connections.Values.Any(c=>c.Room == RoomName.Room))
                {
                    _connections.Remove(RoomName.Room);
                }

                await Clients.Group(RoomName.Room).SendAsync("ReceiveMessage", _botUser,
                    $"{userName} ја напушти консултацијата");

                await Clients.Group(RoomName.Room).SendAsync("LeaveConsultation");
            }
        }


        public async Task RefreshTextCons()
        {
            await Clients.All.SendAsync("RefreshTextCons");
            Console.WriteLine("*******************************************REFRESHSSSS");
            
        }


    }
}

