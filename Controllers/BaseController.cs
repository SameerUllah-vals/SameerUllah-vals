using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Models;
using static Web.Helpers.ApplicationHelper;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly  ApplicationDbContext dbContext;        
        public BaseController()
        {
            dbContext = new ApplicationDbContext();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.Layout = "_M1Layout";
            ViewBag.Navigation = dbContext.DynamicForms
                .Where(x =>x.Status == EnumStatus.Enable && x.IsDeleted.Equals(false))
                .ToList();

            base.OnActionExecuting(context);    
        }
    }
}
