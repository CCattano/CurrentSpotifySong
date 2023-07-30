using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Translators;
using Torty.Web.Apps.CurrentSpotifySong.Data;
using Torty.Web.Apps.CurrentSpotifySong.Facades.Users;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Extensions;
using Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers.BusinessModel_BusinessEntity;
using Torty.Web.Apps.CurrentSpotifySong.WebService.Middleware;
using ApiType = Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Extensions.ConfigurationExtensions.ApiType;
using AppSettings = Torty.Web.Apps.CurrentSpotifySong.Infrastructure.SystemConstants.AppSettings;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService;

public class Startup
{
    public readonly IConfiguration Configuration;
    
    public Startup(IWebHostEnvironment env)
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false)
            .AddEnvironmentVariables()
            .Build();
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        #region FRAMEWORK

        services.AddHttpContextAccessor();
        services.AddControllers();
        
        #endregion

        #region ADAPTERS

        services.AddScoped<ISpotifyAdapter, SpotifyAdapter>();
        services.AddScoped<IUserAdapter, UserAdapter>();

        #endregion

        #region FACADES

        services.AddScoped<IUnauthenticatedUsersFacade, UnauthenticatedUsersFacade>();
        services.AddScoped<IUsersFacade, UsersFacade>();

        #endregion

        #region SERRVICES

        services.AddDataService(Configuration.GetConnectionString(AppSettings.ConnStrings.MongoDb));

        #endregion
        
        #region CLIENTS

        string spotifyClientId = Configuration.GetApiConfig(ApiType.Spotify, AppSettings.APIs.Spotify.ClientId);
        string spotifyClientSecret = Configuration.GetApiConfig(ApiType.Spotify, AppSettings.APIs.Spotify.ClientSecret);
        services.AddSpotifyClient(spotifyClientId, spotifyClientSecret);

        #endregion

        #region EXTERNAL

        services.AddAutoMapper(
            typeof(BusinessEntity_DomainEntity),
            typeof(BusinessModel_BusinessEntity)
        );

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CurrentSpotifySong", Version = "v1" });
        });

        #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.MapWhen(
            // When request path is /status/isalive.
            path => path.Request.Path.Value?.ToLower() == "/status/isalive",
            // Return this message.
            builder => builder.Run(async context =>
                await context.Response.WriteAsync("CurrentSpotifySong Lambda is currently running."))
        );

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrentSpotifySong v1"));
        }

        app.UseExceptionHandlerMiddleware();
        
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}