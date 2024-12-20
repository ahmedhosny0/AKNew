using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CK.Controllers
{
    public class BaseController : Controller
    {
        protected string username;
        protected string Password;
        protected string Role;
        protected string StoreIddynamic;
        protected string StoreIdRms;
        protected string PriceCategory;
        protected string Isuser;
        protected string checkStart;
        protected string checkEnd;
        protected string Inventlocation;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Retrieve session values once for all actions
            username = HttpContext.Session.GetString("Username");
            Password = HttpContext.Session.GetString("Password");
            Role = HttpContext.Session.GetString("Role");
            StoreIddynamic = HttpContext.Session.GetString("StoreIddynamic");
            StoreIdRms = HttpContext.Session.GetString("StoreIdRms");
            PriceCategory = HttpContext.Session.GetString("PriceCategory");
            Isuser = HttpContext.Session.GetString("isUsername");
            checkStart = HttpContext.Session.GetString("StartDate");
            checkEnd = HttpContext.Session.GetString("EndDate");
            Inventlocation = HttpContext.Session.GetString("Inventlocation");
            // Store them in ViewBag for views
            ViewBag.Username = username;
            ViewBag.Password = Password;
            ViewBag.Role = Role;
            ViewBag.StoreIddynamic = StoreIddynamic;
            ViewBag.StoreIdRms = StoreIdRms;
            ViewBag.PriceCategory = PriceCategory;
            ViewBag.isUsername = Isuser;
            ViewBag.checkStart = checkStart;
            ViewBag.checkEnd = checkEnd;
            ViewBag.uuu = Isuser;
        }
    }
}
