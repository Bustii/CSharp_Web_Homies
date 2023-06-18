using Homies.Models;

namespace Homies.Contracts
{
    public interface IEventService
    {
        Task <IEnumerable<AllEventViewModel>> GetAllEventAsync();
        Task <IEnumerable<AllEventViewModel>> GetMyJoinedEventsAsync(string userId);
        Task<AddEventViewModel> GetNewAddEventModelAsync();
        Task AddEventAsync(AddEventViewModel model);
    }
}
