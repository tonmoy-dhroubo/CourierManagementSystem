using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CourierManagementSystem.Data.Repositories;
using CourierManagementSystem.Models.Entities;
using X.PagedList;
using CourierManagementSystem.Web.Services;

namespace CourierManagementSystem.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConsignmentController : Controller
    {

        private readonly IConsignmentRepository _consignmentRepository;

        public ConsignmentController(IConsignmentRepository consignmentRepository)
        {
            _consignmentRepository = consignmentRepository;
        }


        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            IEnumerable<Consignment> consignments = await _consignmentRepository.GetAllAsyncAdmin();
            IPagedList<Consignment> paginatedConsignments = consignments
                .OrderBy(o => o.OrderDate)
                .ToPagedList(page, pageSize); 
                
            return View(paginatedConsignments);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int Id)
        {
            Consignment consignment = await _consignmentRepository.GetAsync(Id);
            return View(consignment);
        }

        public async Task<IActionResult> Report()
        {
            // Retrieve all consignments from your data source
            IEnumerable<Consignment> consignments = await _consignmentRepository.GetAllAsyncAdmin();

            PdfGenerationService pdfService = new PdfGenerationService();
            byte[] pdfData = pdfService.GeneratePdfReport(consignments);

            return File(pdfData, "application/pdf", "ConsignmentReport.pdf");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _consignmentRepository.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }


    }
}
