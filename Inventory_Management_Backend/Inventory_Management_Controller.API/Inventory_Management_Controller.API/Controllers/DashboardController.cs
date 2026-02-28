using Inventory_Management.Application.DTO;
using Inventory_Management.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_Controller.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public ActionResult<IList<DashboardSummaryDTO>> GetDashboardSummary()
        {
            var result = _dashboardService.GetDashboardSummary();

            return Ok(result);
        }
    }
}
