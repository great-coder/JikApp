using System;
using System.Linq;
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
            // $"{Context.User.Identity.Name} joined."
            return Clients.All.SendAsync("SendSystemAction", "A user joined.");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // TODO: online_users--
            // $"{Context.User.Identity.Name} left."
            return Clients.All.SendAsync("SendSystemAction", "A user left.");
        }

        public Task GetJiks()
        {
            var dbContext = EF_Model.dbContext;
            return Clients.All.SendAsync("GetJiksAction", new List<Jik>(dbContext.Jiks.OrderByDescending(j => j.Created_At)));
        }

        public async Task<Task> SendJik(Jik jik)
        {
            var dbContext = EF_Model.dbContext;
            dbContext.Jiks.Add(jik);
            await dbContext.SaveChangesAsync();
            return Clients.All.SendAsync("SendJikAction", jik);
        }
    }
}
