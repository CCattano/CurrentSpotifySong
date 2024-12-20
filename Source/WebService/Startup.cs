using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Adapters;
using Torty.Web.Apps.CurrentSpotifySong.Adapters.Translators;
using Torty.Web.Apps.CurrentSpotifySong.Data;
using Torty.Web.Apps.CurrentSpotifySong.Facades.Users;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient;
using Torty.Web.Apps.CurrentSpotifySong.Infrastructure.UtilityTypes;
using Torty.Web.Apps.CurrentSpotifySong.WebService.Controllers.BusinessModel_BusinessEntity;
using Torty.Web.Apps.CurrentSpotifySong.WebService.Middleware;
using static Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Extensions.ConfigurationExtensions;
using static Torty.Web.Apps.CurrentSpotifySong.Infrastructure.SystemConstants;

namespace Torty.Web.Apps.CurrentSpotifySong.WebService;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IWebHostEnvironment env)
    {
        _configuration = new ConfigurationBuilder()
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

        services.AddDataService(_configuration.GetConnectionString(AppSettings.ConnStrings.MongoDb));

        #endregion
        
        #region CLIENTS

        string spotifyClientId = _configuration.GetApiConfig(ApiType.Spotify, AppSettings.APIs.Spotify.ClientId);
        string spotifyClientSecret = _configuration.GetApiConfig(ApiType.Spotify, AppSettings.APIs.Spotify.ClientSecret);
        services.AddSpotifyClient(spotifyClientId, spotifyClientSecret);

        #endregion

        #region UTILITIES

        services.AddSingleton(provider =>
        {
            IHttpContextAccessor httpCtxAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            HttpRequest httpReq = httpCtxAccessor.HttpContext!.Request;
            string protocolScheme = httpReq.Scheme;
            string host = httpReq.Host.ToString();
            string baseUri = $"{protocolScheme}://{host}";
            return new ApiContextUtility(baseUri);
        });

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
        
        services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

        #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.MapWhen(
            // When request path is /status/isalive.
            path => path.Request.Path.Value == "/status/isalive",
            builder => builder.Run(async context =>
            {
                const string response = "CurrentSpotifySong Lambda is currently running.";
                Console.WriteLine(response);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                // Return this message.
                await context.Response.WriteAsync(response);
            })
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