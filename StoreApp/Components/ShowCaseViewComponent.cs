using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Components
{
    public class ShowCaseViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;
        public ShowCaseViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public IViewComponentResult Invoke(string page ="default")
        {

            var product =_manager.ProductService.GetShowCaseProducts(false);
            return 
            page.Equals("default")
            ? View(product)
            : View("List",product);
        }
    }
}