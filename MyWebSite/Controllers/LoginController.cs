using Microsoft.AspNetCore.Mvc;
using MyWebSite.ViewModel;
using Microsoft.AspNetCore.Http;

namespace MyWebSite.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new LoginModel();
            model.Email = Request.Query.ContainsKey("Email") ? Request.Query["Email"] : "";
            ViewBag.Title = "GET-запрос";
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                HttpContext.Session.SetString("username", model.Email);
                Response.Cookies.Append("usercookie", model.Email,
                    new CookieOptions()
                    {
                        Path = "/",
                        Expires = DateTimeOffset.Now.AddDays(10)
                    }
            );
                return Redirect("/");
            }
            ViewBag.Title = "POST-запрос";
            return View(model);
        }
    }
}
