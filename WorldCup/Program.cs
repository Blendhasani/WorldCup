using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Security.Policy;
using WorldCup.Areas.Admin.Data.Services;
using WorldCup.Data;
using WorldCup.Data.Cart;
using WorldCup.Data.Helpers;
using WorldCup.Data.Services;
using WorldCup.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));


var apiKey = "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";
var client = new SendGridClient(apiKey);
var msg = new SendGridMessage()
{
    From = new EmailAddress("saraqialbin@gmail.com", "Albin"),
    Subject = "Sending with Twilio SendGrid is Fun",
    PlainTextContent = "and easy to do anywhere, especially with C#"
};
msg.AddTo(new EmailAddress("as51465@ubt-uni.net", "as51465@ubt-uni.net"));
var response = await client.SendEmailAsync(msg);

// A success status code means SendGrid received the email request and will process it.
// Errors can still occur when SendGrid tries to send the email. 
// If email is not received, use this URL to debug: https://app.sendgrid.com/email_activity 
Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
builder.Services.AddScoped<IHightlightsService, HighlightsService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.AddScoped<IStadiumsService, StadiumsService>();
builder.Services.AddScoped<IClubsService, ClubsService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
builder.Services.AddSession();
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
app.UseSession();
//Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();
app.UseAuthorization();
app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();
app.Run();
