using Microsoft.EntityFrameworkCore;

namespace CourierManagementSystem.Data.Seeders
{
    public class TrackingNumberGenerator
    {
        private readonly ApplicationDbContext _context;

        public TrackingNumberGenerator(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Generate()
        {
            var trackingNumber = "";
            var random = new Random();
            var trackingNumberExists = true;

            while (trackingNumberExists)
            {
                trackingNumber = random.Next(100000000, 999999999).ToString();
                trackingNumberExists = await _context.Consignments.AnyAsync(c => c.TrackingNumber == trackingNumber);
            }

            return trackingNumber;
        }
    }
}
