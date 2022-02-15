using Microsoft.AspNetCore.SignalR;

namespace BlazorChatAppSignalR.Server.Hubs
{
    public class chathub : Hub

    {
        private static Dictionary<string, string> Users = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            string username = Context.GetHttpContext().Request.Query["username"];
            Users.Add(Context.ConnectionId, username);
            await AddMessageToChat(string.Empty, $"{username} kullanıcısı iniş yaptı");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string username=Users.FirstOrDefault(u=>u.Key==Context.ConnectionId).Value;
            await AddMessageToChat(string.Empty, $"{username} çıkış yaptı");
        }

        public async Task AddMessageToChat(string user, string message)
        {
            await Clients.All.SendAsync("GetThatMessageDude",user, message);
        }
    }
}
