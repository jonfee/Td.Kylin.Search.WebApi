using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using Td.Diagnostics;
using Td.Kylin.DataCache;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.WriterManager;
using Td.Kylin.WebApi;
using Td.Web;

namespace Td.Kylin.Search.WebApi
{
    public class Startup
    {
        //wwwroot的根目录
        public static string WebRootPath { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            Application.Start(new ApplicationContext(env, appEnv));

            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            WebRootPath = env.WebRootPath;

            Configuration = builder.Build();

            #region 开启线程 执行索引库写队列处理

            AreaIndexManager.Instance.StartNewThread();

            MallProductIndexManager.Instance.StartNewThread();

            MerchantProductIndexManager.Instance.StartNewThread();

            MerchantIndexManager.Instance.StartNewThread();

            JobIndexManager.Instance.StartNewThread();

            #endregion
        }

        public static IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 添加异常拦截处理程序。
            ExceptionHandlerManager.Instance.Handlers.Add(new UnknownExceptionHandler());

            // 添加全局异常过滤器。
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new Td.Web.Filters.HandleExceptionFilter());
                options.Filters.Add(new Td.Kylin.WebApi.Filters.HandleArgumentFilter());
            });

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            SqlProviderType sqlType = new Func<SqlProviderType>(() =>
            {
                string sqltype = Configuration["Data:SqlType"] ?? string.Empty;

                switch (sqltype.ToLower())
                {
                    case "npgsql":
                        return SqlProviderType.NpgSQL;
                    case "mssql":
                    default:
                        return SqlProviderType.SqlServer;
                }
            }).Invoke();

            string redisConn = Configuration["Redis:ConnectString"];//Redis缓存服务器信息
            var sqlConn = Configuration["Data:DefaultConnection:ConnectionString"];

            app.UseDataCache(true, redisConn, sqlType, sqlConn, null, false);

            app.UseKylinWebApi(Configuration["ServerId"], sqlConn, sqlType);

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
