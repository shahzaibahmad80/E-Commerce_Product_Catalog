var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register application services for dependency injection
builder.Services.AddSingleton<E_Commerce_Product_Catalog.Data.DbConnectionFactory>();
builder.Services.AddScoped<E_Commerce_Product_Catalog.Data.IProductRepository, E_Commerce_Product_Catalog.Data.ProductRepository>();
builder.Services.AddScoped<E_Commerce_Product_Catalog.Services.IProductService, E_Commerce_Product_Catalog.Services.ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
