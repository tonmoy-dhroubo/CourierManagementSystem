using CourierManagementSystem.Data;
using CourierManagementSystem.Data.Repositories;
using CourierManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CourierManagementSystem.Controllers
{
    public class OrderController : Controller
    {

        private readonly IConsignmentRepository _consignmentRepository;
        //private readonly ApplicationDbContext _dbContext;

        public OrderController(IConsignmentRepository consignmentRepository )
        {
            _consignmentRepository = consignmentRepository;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {

            IEnumerable<Consignment> consignments = await _consignmentRepository.GetAllAsync();
            IPagedList<Consignment> paginatedConsignments = consignments
                .OrderBy(o => o.OrderDate)
                .ToPagedList(page, pageSize);

            return View(paginatedConsignments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Consignment consignment)
        {

            await _consignmentRepository.AddAsync(consignment);
            return RedirectToAction(nameof(Index));
        }
    }
}
