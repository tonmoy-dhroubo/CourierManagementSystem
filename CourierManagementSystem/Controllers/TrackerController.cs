using CourierManagementSystem.Data.Repositories;
using CourierManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourierManagementSystem.Web.Controllers
{
    public class TrackerController : Controller
    {

        private readonly IConsignmentRepository _consignmentRepository;

        public TrackerController(IConsignmentRepository consignmentRepository)
        {
            _consignmentRepository = consignmentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Show(string trackingNumber)
        {
            ICollection<TrackingLog> trackingLogs = await _consignmentRepository.GetTrackerAsync(trackingNumber);
            return View(trackingLogs);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(string trackingNumber)
        {
            ViewData["trackingNumber"] = trackingNumber;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(TrackingLog trackingLog)
        {
            trackingLog.Date = DateTime.Now;
            await _consignmentRepository.AddTrackingLogAsync(trackingLog.TrackingNumber, trackingLog);
            return RedirectToAction(nameof(Show), new { trackingNumber = trackingLog.TrackingNumber});
        }
    }
}
