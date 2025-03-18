using Microsoft.AspNetCore.Mvc;
using Resto4.Models;
using System.Diagnostics;

namespace Resto4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository ;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Chef()
        {
            return View();
        }

        public async Task<IActionResult> Privacy(string sterm = "", int categoryId = 0)
        {
            IEnumerable<Plat> plats = await _homeRepository.GetPlats(sterm, categoryId);
            IEnumerable<Category> categorys = await _homeRepository.Categorys(); //hadi methode khsk tzidi ()

            PlatDisplayModel platModel = new PlatDisplayModel
            {
                Plats = plats,
                Categories = categorys,
                STerm =sterm,
                CategoryId=categoryId
            };
            

            return View(platModel);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}