using Microsoft.AspNetCore.Mvc;

namespace RaptorStreet.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
