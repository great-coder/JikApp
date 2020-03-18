using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JikApp.Data;
using Microsoft.AspNetCore.SignalR;

namespace JikApp.Hubs
{
    //[Authorize]
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            // TODO: online_users++
            return Clients.All.SendAsync("SendAction", new Jik()
            {
                SenderId = "System",
                Created_At = DateTime.Now,
                Message = $"{Context.User.Identity.Name} joined."
            });
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // TODO: online_users--
            return Clients.All.SendAsync("SendAction",
                new Jik()
                {
                    SenderId = "System",
                    Created_At = DateTime.Now,
                    Message = $"{Context.User.Identity.Name} left."
                });
        }

        public Task GetJiks()
        {
            var dbContext = EF_Model.dbContext;
            return Clients.All.SendAsync("GetJiksAction", new List<Jik>(dbContext.Jiks));
        }

        public async Task<Task> Send(Jik jik)
        {
            var dbContext = EF_Model.dbContext;
            dbContext.Jiks.Add(jik);
            await dbContext.SaveChangesAsync();
            return Clients.All.SendAsync("SendMessage", jik);
        }
    }
}
