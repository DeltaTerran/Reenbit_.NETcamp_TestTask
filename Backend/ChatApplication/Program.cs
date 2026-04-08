using ChatApplication.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR().AddAzureSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins(
            "http://localhost:4200",
            "https://thankful-stone-01d738003.1.azurestaticapps.net/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});
var app = builder.Build();
app.UseCors("AllowAngular");
app.MapHub<ChatHub>("/chat");

app.Run();
