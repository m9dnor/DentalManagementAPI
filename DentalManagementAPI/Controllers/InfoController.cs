using Microsoft.AspNetCore.Mvc;

namespace DentalManagementAPI.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Guide()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
