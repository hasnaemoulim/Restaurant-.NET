using Microsoft.AspNetCore.Mvc;

namespace Resto4.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Test()
        {
            return Content("This is the Test action in the TestController.");
        }
    }
}
