
using WebSocketServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors(options => 
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:5270")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);

app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints => 
{
    endpoints.MapHub<MessageHub>("/message");
});
app.Run();
