using Configuration_Assignment_2;
using Configuration_Assignment_2.ServiceContracts;
using Configuration_Assignment_2.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IFinnhubService, FinnhubService>();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection(nameof(TradingOptions)));

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseStaticFiles();


app.Run();
