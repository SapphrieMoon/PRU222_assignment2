using Microsoft.AspNetCore.SignalR;

namespace Assignment2.Hubs
{
    public class NewsHub : Hub
    {
        // Called when a news article is created
        public async Task CreateNews(object newsArticle)
        {
            await Clients.All.SendAsync("NewsCreated", newsArticle);
        }

        // Called when a news article is added (if different from create)
        public async Task AddNews(object newsArticle)
        {
            await Clients.All.SendAsync("NewsAdded", newsArticle);
        }
    }
} 