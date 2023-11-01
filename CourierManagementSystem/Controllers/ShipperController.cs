using CourierManagementSystem.Data.Repositories;
using CourierManagementSystem.Models.Entities;
using CourierManagementSystem.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CourierManagementSystem.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShipperController : Controller
    {

        private readonly IShipperRepository _shipperRepository;

        public ShipperController(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            IEnumerable<Shipper> shippers = await _shipperRepository.GetAllAsync();
            IPagedList<Shipper> paginatedShippers = shippers
                .OrderBy(o => o.CompanyName)
                .ToPagedList(page, pageSize);
            return View(paginatedShippers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Shipper shipper)
        {

            await _shipperRepository.AddAsync(shipper);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Report()
        {
            // Retrieve all consignments from your data source
            IEnumerable<Shipper> shippers = await _shipperRepository.GetAllAsync();

            ShipperPdfGenerationService pdfService = new ShipperPdfGenerationService();
            byte[] pdfData = pdfService.GeneratePdfReport(shippers);

            return File(pdfData, "application/pdf", "ConsignmentReport.pdf");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _shipperRepository.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
