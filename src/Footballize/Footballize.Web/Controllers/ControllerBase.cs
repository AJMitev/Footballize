namespace Footballize.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public abstract class ControllerBase : Controller
    {
        public void DisplayError(string errorMessage)
        {
            this.TempData["Error"] = errorMessage;
        }
    }
}