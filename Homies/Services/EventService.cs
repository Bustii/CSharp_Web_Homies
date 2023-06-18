using Homies.Contracts;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.EntityFrameworkCore;


namespace Homies.Services
{
    public class EventService : IEventService
    {
        private readonly HomiesDbContext dbContext;

        public EventService(HomiesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddEventAsync(AddEventViewModel model)
        {
            Event events = new Event
            {
                Name = model.Name,
                Description = model.Description,
                Start = model.Start,
                End = model.End,
                TypeId = model.TypeId
            };

            await dbContext.Events.AddAsync(events);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllEventViewModel>> GetAllEventAsync()
        {
            return await dbContext
                .Events
                .Select(b => new AllEventViewModel
                {
                    Id = b.Id,
                    Organiser = b.Organiser.ToString(),
                    Name = b.Name,
                    Start = b.Start.ToString(),
                    Type = b.Type.ToString()
                }).ToListAsync();
        }

        public Task<IEnumerable<AllEventViewModel>> GetMyJoinedEventsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AddEventViewModel> GetNewAddEventModelAsync()
        {
            var types = await dbContext.Types
                .Select(t => new EventTypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToListAsync();

            var model = new AddEventViewModel
            {
                Types= types
            };

            return model;
        }
    }
}
