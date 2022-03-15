var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7133") });

builder.Services.AddBlazoredLocalStorage();

// Authorization
builder.Services.AddAuthorizationCore();


// Contracts, Services


// AutoMapper


await builder.Build().RunAsync();
