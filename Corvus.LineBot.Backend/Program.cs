using Corvus.LineBot.Backend.Helpers;
using Corvus.LineBot.Backend.Services;
using Microsoft.Extensions.FileProviders;

namespace Corvus.LineBot.Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var env = builder.Environment;
        var services = builder.Services;

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<LineBotHelper>();

        services.AddScoped<LineBotService>();
        services.AddScoped<GptService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                RequestPath = "/images"
            });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}