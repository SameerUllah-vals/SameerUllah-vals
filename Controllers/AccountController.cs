using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {   
            return View();
        }
        public IActionResult About()
        {          
            return View();
        }
    }
}
