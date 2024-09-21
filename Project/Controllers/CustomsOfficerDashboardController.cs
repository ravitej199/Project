using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using Microsoft.Extensions.Logging;
using Project.Repository.IRepository;
using Project.Services;

namespace Project.Controllers
{
    [Authorize(Roles = "CustomsOfficer")]

    public class CustomsOfficerDashboardController : Controller
    {
        private readonly TruckGoodsService _truckGoodsService;
        private readonly ILogger<CustomsOfficerDashboardController> _logger;

        public CustomsOfficerDashboardController(TruckGoodsService truckGoodsService, ILogger<CustomsOfficerDashboardController> logger)
            
        {
            _truckGoodsService = truckGoodsService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                var internationalGoods = await _truckGoodsService.GetInternationalGoodsAsync();
                return View(internationalGoods.ToList());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error fetching data in Customs Dashboard");

                TempData["ErrorMessage"] = "An error occurred while fetching the data.";
                return RedirectToAction("Error", "Home");
            }

        }

        public async Task<IActionResult> ApproveGoods(int id)
        { try{
            await _truckGoodsService.ApproveGood(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error approving goods with ID {id}");

                TempData["ErrorMessage"] = "An error occurred while approving the goods.";
                return RedirectToAction("Error", "Home");
            }
        }



        public async Task<IActionResult> Reject(int id)
        {
            try
            {

                await _truckGoodsService.RejectGood(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting good with ID {id}");

                TempData["ErrorMessage"] = "An error occurred while Deleting the good.";
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
