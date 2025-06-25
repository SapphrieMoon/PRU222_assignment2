using Microsoft.AspNetCore.SignalR;

namespace Assignment2.Hubs
{
    public class CategoryHub : Hub
    {
        // Called when a category is created
        public async Task CreateCategory(object category)
        {
            await Clients.All.SendAsync("CategoryCreated", category);
        }

        // Called when a category is added (if different from create)
        public async Task AddCategory(object category)
        {
            await Clients.All.SendAsync("CategoryAdded", category);
        }
    }
} 