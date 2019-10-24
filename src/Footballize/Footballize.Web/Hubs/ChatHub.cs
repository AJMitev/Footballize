namespace Footballize.Web.Hubs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Models;
    using ViewModels.Shared;

    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IRepository<ChatMessage> chatRepository;

        public ChatHub(IRepository<ChatMessage> chatRepository) 
            => this.chatRepository = chatRepository;

        public async Task Send(string message, string gatherId)
        {
            var newMessage = new ChatMessage
            {
                GatherId = gatherId,
                Text =  message,
                Sender = this.Context.User.Identity.Name
            };


            await this.chatRepository.AddAsync(newMessage);
            await this.chatRepository.SaveChangesAsync();

            await this.Clients.All.SendAsync(
                "NewMessage",
                new MessageViewModel { User = this.Context.User.Identity.Name, Text = message, });
        }


        public async Task GetHistory(string gatherId)
        {
            var mesageHistory = this.chatRepository
                .All()
                .Where(x => x.GatherId == gatherId)
                .OrderBy(x=>x.CreatedOn);

            foreach (var chatMessage in mesageHistory)
            {
                await this.Clients.Caller.SendAsync("NewMessage",
                    new MessageViewModel {User = chatMessage.Sender, Text = chatMessage.Text});
            }
        }

        
        public override async  Task OnConnectedAsync()
        {
            await this.Clients.Caller.SendAsync("LoadChatHistory");

            await this.Clients.All.SendAsync(
                "UserJoin",
                new MessageViewModel { User = this.Context.User.Identity.Name});
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await this.Clients.All.SendAsync(
                "UserLeave",
                new MessageViewModel { User = this.Context.User.Identity.Name});
        }
    }
}