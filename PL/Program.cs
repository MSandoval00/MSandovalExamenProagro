var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Configuration
var configuration = builder.Configuration;
builder.Configuration.Bind("ConnectionStrings", new ConnectionStrings());
builder.Configuration.Bind("AppSettings", new AppSettings());
var appSettings = builder.Configuration.Get<AppSettings>();
var connectionStrings=builder.Configuration.Get<ConnectionStrings>();
//sesion
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
public class AppSettings
{
    public string TipoExcel { get; set; }
    public string RutaCargaMasica { get; set; }
}
public class ConnectionStrings
{
    public string OleDbConnection { get; set; }
}
