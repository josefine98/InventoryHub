// InventoryHub - Minimal API Back-End (ServerApp)
// Activity 2: Copilot suggested the inline UseCors() overload as a simpler
//             alternative to a named policy, removing boilerplate while keeping
//             the middleware order correct (must precede endpoint mapping).
// Activity 3: Copilot recommended anonymous object literals for the response
//             so the JSON shape is defined inline and easy to read at a glance.
// Step 3:     Copilot added OutputCache so repeated requests are served from
//             memory instead of re-executing the handler on every call.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Step 3: Server-side output cache — responses are held in memory for 5 minutes,
// eliminating redundant handler executions under repeated front-end requests.
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromMinutes(5)));
});

var app = builder.Build();

// Activity 2: CORS policy allows any origin/method/header so the Blazor WASM
// client (a different port) can reach this API without browser security blocks.
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseOutputCache();

// Activity 1 + 3: Copilot generated the initial endpoint scaffold and later
// suggested adding the nested Category object to meet the standardised JSON
// format requirement. Each product now carries a fully structured category.
app.MapGet("/api/productlist", () =>
{
    return new[]
    {
        new
        {
            Id = 1,
            Name = "Laptop",
            Price = 1200.50,
            Stock = 25,
            Category = new { Id = 101, Name = "Electronics" }
        },
        new
        {
            Id = 2,
            Name = "Headphones",
            Price = 50.00,
            Stock = 100,
            Category = new { Id = 102, Name = "Accessories" }
        }
    };
}).CacheOutput();

app.Run();
