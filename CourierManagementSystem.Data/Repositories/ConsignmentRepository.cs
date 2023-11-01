using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CourierManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using CourierManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using CourierManagementSystem.Data.Seeders;

namespace CourierManagementSystem.Data.Repositories
{
    public class ConsignmentRepository : IConsignmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConsignmentRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddAsync(Consignment consignment)
        {
            //---------------------------------------------
            //
            //            Fill automatic values 
            //
            //_____________________________________________

            //  Fill the tracking number
            TrackingNumberGenerator tracker = new TrackingNumberGenerator(_context);
            consignment.TrackingNumber = await tracker.Generate();

            //  Fill the sender Id
            var user = _httpContextAccessor.HttpContext.User; //logged in user
            var senderId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            consignment.SenderId = senderId;

            //  Fill the sender Email
            var email = user.FindFirstValue(ClaimTypes.Email);
            consignment.SenderEmail = email;

            //  Fill the sender phone
            //if (consignment.SenderPhoneNumber == null)
            //{
            //    var phone = user.FindFirstValue(ClaimTypes.HomePhone);
            //    consignment.SenderPhoneNumber = phone;
            //}

            //  Fill the status
            consignment.Status = "Pending";

            //  Fill the date
            consignment.OrderDate = DateTime.Now;

            //  Fill the delivery date
            consignment.EstimatedDeliveryDate = DateTime.Now.AddDays(3);

            //  Fill the shipping cost
            consignment.ShippingCost = 100;

            //  Fill the payment completion
            consignment.PaymentCompletion = false;

            //  Add a new TrackingLog
            consignment.TrackingLogs = new List<TrackingLog>
            {
                new TrackingLog
                {
                    TrackingNumber = consignment.TrackingNumber,
                    Location = consignment.SenderAddress,
                    Status = "Ordered",
                    Date = DateTime.Now
                }
            };
            

            // Save the model in dbcontext

            await _context.Consignments.AddAsync(consignment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Consignment consignment)
        {
            _context.Consignments.Update(consignment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var consignment = await _context.Consignments.FindAsync(id);
            if (consignment != null)
            {
                _context.Consignments.Remove(consignment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Consignment> GetAsync(int id)
        {
            return await _context.Consignments.FindAsync(id);
        }


        //for consignments only of logged in user
        public async Task <IEnumerable<Consignment>> GetAllAsync()
        {
            List<Consignment> consignments = await _context.Consignments.Include(c => c.TrackingLogs).ToListAsync();
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User; //logged in user
            IEnumerable<Consignment> consignmentsByUser = consignments.Where(
                x => x.SenderId == user.FindFirstValue(ClaimTypes.NameIdentifier));
            return consignmentsByUser;
        }

        // for all consignments of all users
        public async Task<IEnumerable<Consignment>> GetAllAsyncAdmin()
        {
            List<Consignment> consignments = await _context.Consignments.Include(c => c.TrackingLogs).ToListAsync();
            return consignments;
        }

        public async Task<ICollection<TrackingLog>> GetTrackerAsync(string trackingNumber)
        {
            Consignment consignment = await _context.Consignments
                .Include(c => c.TrackingLogs)
                .FirstOrDefaultAsync(c => c.TrackingNumber == trackingNumber);

            return await Task.FromResult(consignment.TrackingLogs);
        }

        public async Task AddTrackingLogAsync(string trackingNumber, TrackingLog trackingLog)
        {
            
            var consignment = await _context.Consignments
                .Include(c => c.TrackingLogs) // Include tracking logs to avoid lazy loading
                .FirstOrDefaultAsync(c => c.TrackingNumber == trackingNumber);

            if (consignment != null)
            {
                // Add the new tracking log to the consignment's tracking logs
                consignment.TrackingLogs.Add(trackingLog);

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Consignment not found with the provided tracking number.");
            }
        }

    }
}
