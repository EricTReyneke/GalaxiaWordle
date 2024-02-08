using Business.DynamicModelReflector.DataOperations;
using Business.DynamicModelReflector.Interfaces;
using Business.DynamicModelReflector.ModelReflectors;
using Business.DynamicModelReflector.QueryBuilders;
using Business.GalaxiaWordle.ApiClients;
using Business.GalaxiaWordle.ApiContext;
using Business.GalaxiaWordle.Games;
using Business.GalaxiaWordle.Interfaces;
using Business.GalaxiaWordle.Login.Logins;
using Business.GalaxiaWordle.PasswordHasers;
using Business.GalaxiaWordle.Registrations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json",
        optional: true,
        reloadOnChange: true);

#region Injections
builder.Services.AddScoped<IDataOperations, SqlDataOperations>();
builder.Services.AddScoped<IQueryBuilder, SqlQueryBuilder>();
builder.Services.AddScoped<IModelReflector, SqlModelReflector>();
builder.Services.AddScoped<IPasswordHasher, SaltPasswordHasing>();
builder.Services.AddScoped<ILogin, BasicLogin>();
builder.Services.AddScoped<IRegistration, BasicRegistration>();
builder.Services.AddScoped<IWordle, WordleGalaxia>();
builder.Services.AddScoped<IWordleContext, WordleContext>();
builder.Services.AddScoped<IWordleApiClient, WordleApiClient>();
#endregion

builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.Run();