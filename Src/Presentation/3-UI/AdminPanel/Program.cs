using Serilog;
using TaskoMask.Infrastructure.Data.Read.DataProviders;
using TaskoMask.Infrastructure.Data.WriteModel.DataProviders;
using TaskoMask.Presentation.Framework.Web.Configuration.Startup;

Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));
    builder.Services.AddMvcProjectConfigureServices(builder.Configuration, builder.Environment);
    var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseMvcProjectConfigure(app.Services, builder.Environment);
    WriteDbSeedData.SeedSampleData(app.Services);
    ReadDbSeedData.SyncSampleData(app.Services);
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=login}/{id?}");

    });
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}


