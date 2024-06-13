using HP.API.Data;
using HP.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HP.API.Hubs
{
    public class NotifyHub : Hub
    {
        private readonly HPDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly IDictionary<string, string> _connections;

        public NotifyHub(HPDbContext dbContext, UserManager<User> userManager,IDictionary<string,string> connections)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            _connections = connections;
        }

        public async Task StartConsultation(Guid consId, string starterId)
        {
            try
            {
                var cons = await dbContext.Consultations.FindAsync(consId);
                var user = await userManager.FindByIdAsync(starterId);

                if (user == null)
                {
                    throw new Exception("User not found");
                }
                if (cons == null)
                {
                    throw new Exception("Consultation not found");
                }

                string user1 = cons.Owner_Id;
                string user2 = cons.Vet_Id;

                Console.WriteLine($"**** User1 {user1}");
                Console.WriteLine($"**** User2 {user2}");

                _connections.TryGetValue(user1, out var conn);
                Console.WriteLine($"**** USER 1 CON ID {conn}");
                _connections.TryGetValue(user2, out var conn2);
                Console.WriteLine($"**** USER 2 CON ID {conn2}");

                if (starterId.Equals(user1))
                {
                    _connections.TryGetValue(user2, out var user2ConnectionId);
                    Console.WriteLine($"$$$$$$$$$$$$$$$$$$$$$$$$ {user2ConnectionId}");

                    if (user2ConnectionId != null)
                    {
                        await Clients.Client(user2ConnectionId).SendAsync("ConsultationStarted", consId);
       
                        Console.WriteLine("**********SEND NOTFICITATION TO VET");
                    }
                }
                else if (starterId.Equals(user2))
                {
                    _connections.TryGetValue(user1, out var user1ConnectionId);

                    if(user1ConnectionId != null)
                    {
                        Console.WriteLine("**********SEND NOTFICITATION TO CLIENT");
                        await Clients.Client(user1ConnectionId).SendAsync("ConsultationStarted", cons.Id);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in StartConsultation: {ex.Message}");
                throw;
            }
        }

        public override async Task OnConnectedAsync()
        {
            string userId = Context.GetHttpContext().Request.Query["userId"];

            if(userId!= null)
            { 
                if(_connections.ContainsKey(userId)) {
                    _connections[userId] = Context.ConnectionId;
                    Console.WriteLine($"User {userId} has reconnected");
                }
                else
                {
                    _connections.Add(userId, Context.ConnectionId);
                    Console.WriteLine($"User {userId}, {Context.ConnectionId} has connected");
                }       
            }      
            else
            {
                // Log or handle the case when Context.User is null
                Console.WriteLine("User is not authenticated.");
            }

            await base.OnConnectedAsync();
        }



        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = await userManager.GetUserAsync(Context.User);
            if (user != null)
            {
                _connections.Remove(user.Id);
            }
            await base.OnDisconnectedAsync(exception);
        }

    }
}
