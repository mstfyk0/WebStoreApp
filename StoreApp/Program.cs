
using StoreApp.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
//Old code 
//Old code Service Extension sınıfına aktarıldı.
// builder.Services.AddDistributedMemoryCache();
// builder.Services.AddSession(options =>
// {
//     options.Cookie.Name = "StoreApp.Session";
//     options.IdleTimeout = TimeSpan.FromMinutes(10);

// });
//New Code
builder.Services.ConfigureSession();


//Old code Service Extension sınıfına aktarıldı.
// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//Old code service extension sınıfına aktarılmıştır.
// builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
// builder.Services.AddScoped<IProductRepository, ProductRepository>();
// builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//new code
builder.Services.ConfigureRepositoryRegistration();

//Old code service extension sınıfına aktarılmıştır.
// builder.Services.AddScoped<IServiceManager, ServiceManager>();
// builder.Services.AddScoped<IProductService, ProductManager>();
// builder.Services.AddScoped<ICategoryServices, CategoryManager>();
// builder.Services.AddScoped<IOrderService, OrderManager>();
//new code
builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigurRouting();
builder.Services.ConfigureApplicationCookie();

//Old code 
// builder.Services.AddScoped<Card>(c => SessionCard.GetCard(c));


builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();


// app.MapGet("/", () => "Hello World!");
// app.MapGet("/btk", () => "BTK Akademi");

app.UseStaticFiles();
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

//Oturum açma veya yetkilendirme işlemleri için  routing ve endpoint 
//servislerinin çağrılmasının arasına koyulması gerekebilir.
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{

    endpoint.MapAreaControllerRoute(
        name: "Admin"
        , areaName: "Admin"
        , pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );
    endpoint.MapControllerRoute(
        name: "default"
        , pattern: "{controller=Home}/{action=Index}/{id?}"
        );
    endpoint.MapRazorPages();
    
    endpoint.MapControllers();
}
);


//Bu sayede migtaionlar otomatik güncellenicek
app.ConfigureAndCheckMigration();
app.ConfigureLocalization();
app.ConfigureDefaultAdminUser();
// app.MapControllerRoute(
//     "default"
//     , );

app.Run();


