using Microsoft.AspNetCore.Mvc;

namespace CourierManagementSystem.Web.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
