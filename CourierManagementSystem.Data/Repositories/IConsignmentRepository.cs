using CourierManagementSystem.Models.Entities;

namespace CourierManagementSystem.Data.Repositories
{
    public interface IConsignmentRepository
    {
        Task AddAsync(Consignment consignment);
        Task UpdateAsync(Consignment consignment);
        Task DeleteAsync(int id);
        Task<Consignment> GetAsync(int id);
        Task<IEnumerable<Consignment>> GetAllAsync();
        Task<IEnumerable<Consignment>> GetAllAsyncAdmin();

        Task<ICollection<TrackingLog>> GetTrackerAsync(string trackingNumber);
        Task AddTrackingLogAsync(string trackingNumber, TrackingLog trackingLog);
        
    }
}
