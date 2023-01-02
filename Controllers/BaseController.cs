using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Models;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ValsTechnologiesContext dbContext;        
        public BaseController()
        {
            dbContext = new ValsTechnologiesContext();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.Layout = "_M1Layout";
            base.OnActionExecuting(context);    
        }
    }
}
