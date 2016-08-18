using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Td.Diagnostics;
using Td.Kylin.DataCache;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Search.WebApi.WriterManager;
using Td.Kylin.WebApi;
using Td.Web;

namespace Td.Kylin.Search.WebApi
{
    public class Startup
    {
        //wwwroot的根目录
        public static string WebRootPath { get; set; }

        private static SqlProviderType _sqlType;
        private static string _sqlConn;

        public Startup(IHostingEnvironment env)
        {
            Application.Start(new ApplicationContext(env));

            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            WebRootPath = env.WebRootPath;

            Configuration = builder.Build();

            _sqlType = new Func<SqlProviderType>(() =>
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

            _sqlConn = Configuration["Data:DefaultConnection:ConnectionString"];
            string redisConn = Configuration["Redis:ConnectString"];//Redis缓存服务器信息

            //使用缓存
            DataCacheExtensions.UseDataCache(new DataCacheServerOptions
            {
                KeepAlive = true,
                CacheItems = null,
                RedisConnectionString = redisConn,
                InitIfNull = false,
                SqlType = _sqlType,
                SqlConnection = _sqlConn,
                Level2CacheSeconds = int.Parse(Configuration["Redis:Level2CacheSeconds"])
            });

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
            var log = new LogOptions()
            {
                LogUrl = Configuration["Log:Url"],
                ProgramVersion = Configuration["Log:ProgramVersion"],
                RunEnvironmental = Configuration["Log:RunEnvironmental"],
                SystemVersion = Configuration["Log:SystemVersion"]
            };

            app.UseKylinWebApi(Configuration["ServerId"], _sqlConn, _sqlType, log);

            app.UseStaticFiles();

            app.UseMvc();
        }

    }
}
