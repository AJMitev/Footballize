namespace Footballize.Web.Areas.Administration.Controllers
{
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    [Authorize(Roles = GlobalConstants.CanSeeAdminAreaRoleName)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
