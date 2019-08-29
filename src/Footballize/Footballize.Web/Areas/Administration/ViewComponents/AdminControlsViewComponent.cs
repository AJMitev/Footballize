namespace Footballize.Web.Areas.Administration.ViewComponents
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Shared;


    public class AdminControlsViewComponent : ViewComponent
    {
        public AdminControlsViewComponent()
        {
                
        }

         public IViewComponentResult Invoke(string id, string name, string controller)
         {
             var model = new AdminControlsViewModel
             {
                 Id = id,
                 Name= name,
                 Controller = controller
             };

             return this.View(model);
         }
    }
}