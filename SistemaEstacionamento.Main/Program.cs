using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaEstacionamento.Main.Data;
using SistemaEstacionamento.Main.Models.Configuration;
using SistemaEstacionamento.Main.Utilitarios.Helper;
using SistemaEstacionamento.Main.Utilitarios.Helper.Interfaces;
using SistemaEstacionamento.Main.Utilitarios.Services;
using SistemaEstacionamento.Main.Utilitarios.Services.Interface;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SistemaEstacionamentoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SistemaEstacionamentoContext") ?? throw new InvalidOperationException("Connection string 'SistemaEstacionamentoContext' not found.")));

builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddSingleton<IEmailSenders, EmailSender>();

builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

var cultureInfo = new CultureInfo("pt-BR");
cultureInfo.NumberFormat.CurrencySymbol = "R$";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

// Middleware de localização
var supportedCultures = new[] { cultureInfo };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autenticacao}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
