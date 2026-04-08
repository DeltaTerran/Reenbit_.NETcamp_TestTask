using ChatApplication.ContextDB;
using ChatApplication.Hubs;
using ChatApplication.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string AllowAngular = "AllowAngular";

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowAngular, policy =>
        policy
            .WithOrigins(
                "http://localhost:4200",
                "https://thankful-stone-01d738003.1.azurestaticapps.net")
            .WithMethods("GET", "POST", "OPTIONS")
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddSignalR().AddAzureSignalR();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IChatService, ChatService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(AllowAngular);

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    db.Database.Migrate();
}

app.MapHub<ChatHub>("/chat").RequireCors(AllowAngular);

app.Run();