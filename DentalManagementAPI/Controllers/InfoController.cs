using Microsoft.AspNetCore.Mvc;

namespace DentalManagementAPI.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
