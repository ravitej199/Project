using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repository.IRepository;
using Project.Services;

namespace Project.Controllers
{
    [Authorize(Roles = "SecurityOperator")]
    public class GateOperatorDashboardController : Controller
    {

        private readonly TruckGoodsService _truckGoodsService;

        private readonly ILogger<GateOperatorDashboardController> _logger;
        public GateOperatorDashboardController(TruckGoodsService truckGoodsService, ILogger<GateOperatorDashboardController> logger)
        {
            _truckGoodsService = truckGoodsService;
            _logger = logger;

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TruckGoodsModel truckGoodsModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _truckGoodsService.CreateTruckGoodsAsync(truckGoodsModel);
                    TempData["SuccessMessage"] = "QR Code successfully generated!";
                    return RedirectToAction(nameof(Create));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occured while saving data ");

                    TempData["ErrorMessage"] = "An error occurred while saving the data.";
                }
            }

            return View(truckGoodsModel);
        }

    }
}
