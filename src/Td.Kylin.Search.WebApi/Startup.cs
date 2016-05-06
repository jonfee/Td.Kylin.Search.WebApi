using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Td.Kylin.DataCache;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.WebApi;

namespace Td.Kylin.Search.WebApi
{
    public class Startup
    {
        //wwwroot的根目录
        public static string WebRootPath { get; set; }

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            WebRootPath = env.WebRootPath;

            Configuration = builder.Build();

            IndexManager.Instance.StartNewThread();
        }

        public static IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            string redisConn = Configuration["Redis:ConnectString"];//Redis缓存服务器信息
            var SqlConn = Configuration["Data:DefaultConnection:ConnectionString"];

            app.UseDataCache(redisConn, DataCache.SqlProviderType.PostgreSQL, SqlConn);

            app.UseKylinWebApi(Configuration);

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
