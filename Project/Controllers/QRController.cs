using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services;

namespace Project.Controllers
{
    public class QRController : Controller
    {
        private readonly QRServices _qrCodeService;

        private readonly ILogger<QRController> _logger;
        public QRController(QRServices qrCodeService, ILogger<QRController> logger)
        {
            _qrCodeService = qrCodeService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Generate(string content)
        {
            try
            {
                var qrCode = _qrCodeService.GenerateQRCode(content);
                var qrCodeBase64 = Convert.ToBase64String(qrCode);
                TempData["SuccessMessage"] = "QR Code Generated Successfully";
                return Json(new { qrCodeBase64 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Generating QR code");

                TempData["ErrorMessage"] = "An error occurred while fetching the data.";
                return RedirectToAction("Details", "GateOperatorDashboard");

            }
         
        }
    }
}
