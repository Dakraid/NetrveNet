using NetrveNet.Services;
using Serilog;
using Squidex.ClientLibrary.ServiceExtensions;
using Syncfusion.Blazor;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console().WriteTo.File("NetrveNet.log")
            .CreateLogger();

        try
        {
            Log.Information($"Starting {nameof(Assembly.GetName)}");

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            builder.Services.AddSquidexClient();
            builder.Services.Configure<SquidexServiceOptions>(
                builder.Configuration.GetSection("Squidex"));

            builder.Services.AddSingleton<DataClient>();

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSyncfusionBlazor();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}