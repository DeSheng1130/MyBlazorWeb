using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyServices;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(client => new HttpClient
{
    BaseAddress = new Uri("https://localhost:44343/")
});

builder.Services.AddScoped<IBookService, BookService>();

await builder.Build().RunAsync();
