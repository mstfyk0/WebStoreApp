namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        ICategoryServices CategoryServices { get; }
        IOrderService OrderService { get; }
        IAuthService authService {get;}
        
    }
}