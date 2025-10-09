using FruitKhaShop.Areas.Admin.InterfaceRepositories;
using FruitKhaShop.Areas.Admin.Repositories;
using FruitKhaShop.InterfaceRepositories;
using FruitKhaShop.Models;
using FruitKhaShop.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSqlServer<DataContext>(builder.Configuration.GetConnectionString("FruitKhaShopContext") ?? 
    throw new InvalidOperationException("Connection string 'FruitKhaShopContext' not found.")); ;
// Cấu hình Cloudinary
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor(); // Add this line
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<ICategoryAdmin, CategoryAdmin>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IProductAdmin, ProductAdmin>();
var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
    app.UseHsts();
}
else
{
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// routing cho admin
app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
