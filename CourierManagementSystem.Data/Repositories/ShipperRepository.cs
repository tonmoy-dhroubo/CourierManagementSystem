using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourierManagementSystem.Data.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly ApplicationDbContext _context;

        public ShipperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Shipper shipper)
        {
            _context.Shippers.Add(shipper);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shipper shipper)
        {
            _context.Shippers.Update(shipper);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var shipper = await _context.Shippers.FindAsync(id);
            if (shipper != null)
            {
                _context.Shippers.Remove(shipper);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Shipper> GetAsync(int id)
        {
            return await _context.Shippers.FindAsync(id);
        }

        public async Task<IEnumerable<Shipper>> GetAllAsync()
        {
            return await _context.Shippers.ToListAsync();
        }
    }
}