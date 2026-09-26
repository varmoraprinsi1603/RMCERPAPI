using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMCERPAPI.Repository;
using System;

namespace RMCERPAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardRepository _dashboardRepository;

        public DashboardController(
            DashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet]
        public IActionResult GetDashboard()
        {
            try
            {
                var data = _dashboardRepository.GetDashboard();

                if (data == null)
                {
                    return NotFound(new
                    {
                        Status = 0,
                        Message = "Dashboard data not found."
                    });
                }

                return Ok(new
                {
                    Data = data,
                    Status = 1,
                    Message = "Dashboard data fetched successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = 0,
                    Message = $"Error fetching dashboard data: {ex.Message}"
                });
            }
        }
    }
}