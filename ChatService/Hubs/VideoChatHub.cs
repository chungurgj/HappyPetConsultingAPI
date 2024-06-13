using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ChatService.Models;

namespace ChatService.Hubs
{
    public class VideoChatHub : Hub
    {
        private readonly IDictionary<string, UserConnection> _connections;

        public VideoChatHub(IDictionary<string, UserConnection> connections)
        {
            _connections = connections;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                await Clients.Group(userConnection.Room)
                    .SendAsync("UserLeft", userConnection.User);

                if (!_connections.Values.Any(c => c.Room == userConnection.Room))
                {
                    _connections.Remove(userConnection.Room);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinVideoChat(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            await Clients.Group(userConnection.Room).SendAsync("UserJoined", userConnection.User);

            // Send a list of connected users to the new user
            var users = _connections.Values.Where(c => c.Room == userConnection.Room).Select(c => c.User);
            await Clients.Caller.SendAsync("ConnectedUsers", users);
        }

        public async Task LeaveVideoChat(string roomId)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
                await Clients.Group(roomId).SendAsync("UserLeft", userConnection.User);

                if (!_connections.Values.Any(c => c.Room == roomId))
                {
                    _connections.Remove(roomId);
                }
            }
        }

        
    }
}
