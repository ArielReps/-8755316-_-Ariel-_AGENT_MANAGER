using FinalProjectWEB.Data;
using FinalProjectWEB.Sockets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddSignalR();

builder.Services.AddControllersWithViews();

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

app.MapHub<MatrixHub>("hub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Missions}/{action=Offers}/{id?}/{m?}");

app.Run();
