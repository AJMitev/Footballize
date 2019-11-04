namespace Footballize.Web.Areas.Administration.Controllers
{
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.CanSeeAdminAreaRoleName)]
    public abstract class AdminController : Controller
    {
        
    }
}