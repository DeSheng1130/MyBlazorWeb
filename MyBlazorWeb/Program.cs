using MyBlazorWeb.Components;
using MyServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var webApiBaseAddress = builder.Configuration["WebsiteConfig:WebApiBaseAddress"] ?? "https://localhost:44343/";

// builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddHttpClient<IBookService, BookService>(client =>
{
    client.BaseAddress = new Uri(webApiBaseAddress);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseHttpsRedirection();

//app.Use(async(context, next) =>
//{
//    if (!context.Request.Path.Value!.Contains("Error"))
//    {
//        throw new Exception("some....error");
//    }

//    await next.Invoke();
//});

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MyBlazorWeb.Client._Imports).Assembly);

app.Run();
