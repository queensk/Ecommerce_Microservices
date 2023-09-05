using E_Cart.Data;
using E_Cart.Extensions;
using E_Cart.Services;
using E_Cart.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection to DB

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Registering the Base Url for the services
builder.Services.AddHttpClient("Product", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:ProductApi"]));
builder.Services.AddHttpClient("Coupon", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:CouponApi"]));
//Services
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();    
//custom builders

builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
