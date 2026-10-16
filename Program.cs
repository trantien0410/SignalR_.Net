using SignalRNetLab;

var builder = WebApplication.CreateBuilder(args);

// Add SignalR and bind to Azure SignalR Service
builder.Services.AddSignalR()
    .AddAzureSignalR(options =>
    {
        options.ConnectionString = builder.Configuration["Azure:SignalR:ConnectionString"];
    });

var app = builder.Build();

// Enable serving index.html from wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

// Map Hub endpoint (ASP.NET Core automatically handles /chatHub/negotiate)
app.MapHub<ChatHub>("/chatHub");

app.Run();