using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using FOMessenger.Code;
using FOMessenger.Code.Storage.Local;
using FOMessenger.Code.Storage.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<LocalStorageHandler>();
builder.Services.AddScoped<DatabaseHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Logger.LogInformation("Test");

Global.Logger = app.Logger;

Global.Logger.LogInformation("Test");

app.Run();
