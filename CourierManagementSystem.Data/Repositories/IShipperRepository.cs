using CourierManagementSystem.Models.Entities;

namespace CourierManagementSystem.Data.Repositories
{
    public interface IShipperRepository
    {
        Task AddAsync(Shipper shipper);
        Task UpdateAsync(Shipper shipper);
        Task DeleteAsync(int id);
        Task<Shipper> GetAsync(int id);
        Task<IEnumerable<Shipper>> GetAllAsync();
    }
}