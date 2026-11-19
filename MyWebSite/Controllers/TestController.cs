using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using MyWebSite.ViewModel;

namespace MyWebSite.Controllers
{
    [Route("my/test")]
    public class TestController : Controller
    {
        [Route("show")]
        public IActionResult Index()
        {
            return Content("Index Test method");
        }

        [Route("details/{id}")]
        public string Details(string id)
        {
            return "ID Value = " + id;
        }

        [Route("person")]
        public IActionResult person()
        {
            return new ObjectResult(new Person
                {
                    FirstName = "Дмитрий",
                    LastName = "Комаров"
                }
            );
        }
    }
}
