using Microsoft.AspNetCore.HttpOverrides;
namespace WebAPIsHostingInventory;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Configuracion de servicios
    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureSqlServer(Configuration);
        services.AddControllers();
        services.ConfigureCors();
    }

    // Configuracion de middleware
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseForwardedHeaders(new ForwardedHeadersOptions {
            ForwardedHeaders = ForwardedHeaders.All
        });

        app.UseRouting();
        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome");
            });
        });
    }
}