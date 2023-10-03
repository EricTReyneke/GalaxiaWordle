using Business.DynamicModelReflector.DataOperations;
using Business.DynamicModelReflector.Interfaces;
using Business.DynamicModelReflector.ModelReflectors;
using Business.DynamicModelReflector.QueryBuilders;
using Business.GalaxiaWordle.Interfaces;
using Business.GalaxiaWordle.Login.Logins;
using Business.GalaxiaWordle.PasswordHasers;
using Business.GalaxiaWordle.Registrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

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
#endregion

builder.Services.AddRazorPages();

var app = builder.Build();
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
