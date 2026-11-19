using Microsoft.AspNetCore.Mvc;
using MyWebSite.Views;

namespace MyWebSite
{
    public class HomeController : Controller
    {
        public IActionResult Index(string id)
        {
            return View();
        }

        public string list(string id)
        {
            return "return list";
        }
    }
}
