using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using web_lab4.Models;

namespace web_lab4.Hubs
{
    public class ChatHub : Hub
    {
        // Connection username - connection string
        private static readonly Dictionary<string, string> Users = new();
        private string Username => Context.User?.Identity?.Name;

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", new Message
            {
                Username = Username,
                Text = message
            });
        }

        public async Task SendPrivateMessage(string username, string message)
        {
            var msg = new Message
            {
                Username = Username,
                Text = message
            };
            await Clients.Caller.SendAsync("ReceiveMessage", msg);
            await Clients.Client(Users[username]).SendAsync("ReceiveMessage", msg);
        }

        public override async Task OnConnectedAsync()
        {
            var username = Username;
            Users[username] = Context.ConnectionId;
            await Clients.Others.SendAsync("Notify", new Message
            {
                Username = username,
                Text = $"{username} has just joined the chat"
            });
            await Clients.Caller.SendAsync("Notify", new Message
            {
                Username = username,
                Text = $"{username} welcome to the chat"
            });
            await Clients.All.SendAsync("Users", Users.Keys);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var username = Username;
            Users.Remove(username);
            await Clients.Others.SendAsync("Notify", new Message
            {
                Username = username,
                Text = $"{username} has left the chat"
            });
            await Clients.All.SendAsync("Users", Users);
            await base.OnDisconnectedAsync(exception);
        }
    }
}