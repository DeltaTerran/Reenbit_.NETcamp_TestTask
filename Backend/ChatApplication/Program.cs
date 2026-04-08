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
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
        Console.WriteLine("DB CAN CONNECT: " + db.Database.CanConnect());
    }
    catch (Exception ex)
    {
        Console.WriteLine("=== DB CONNECTION ERROR ===");
        Console.WriteLine(ex.ToString());
    }
}
app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(AllowAngular);

//using (var scope = app.Services.CreateScope())
//{
//    try
//    {
//        var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
//        Console.WriteLine("Starting database migration...");
//        db.Database.Migrate();
//        Console.WriteLine("Database migration completed.");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("DATABASE MIGRATION ERROR:");
//        Console.WriteLine(ex.ToString());
//        throw;
//    }
//}

app.MapHub<ChatHub>("/chat").RequireCors(AllowAngular);

app.Run();