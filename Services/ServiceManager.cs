using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IProductService _productService ;
        private readonly ICategoryServices _categoryService ;
        private readonly IOrderService _orderService ;
        private readonly IAuthService _authService;


        public ServiceManager(IProductService productService, ICategoryServices categoryServices, IOrderService orderService, IAuthService authService)
        {
            _productService = productService;
            _categoryService = categoryServices;
            _orderService = orderService;
            _authService = authService;
        }

        public IProductService ProductService => _productService;

        public ICategoryServices CategoryServices =>_categoryService;

        public IOrderService OrderService => _orderService;

        public IAuthService authService => _authService;
    }
}