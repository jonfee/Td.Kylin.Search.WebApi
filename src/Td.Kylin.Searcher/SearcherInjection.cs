using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.Searcher
{
    public static class SearcherInjection
    {
        /// <summary>
        /// 注入搜索
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSearcher(this IApplicationBuilder builder, string partnerID, string secret, List<ApiConfig> configs)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Use(next => new SearchMiddleware(next, partnerID, secret, configs).Invoke);
        }

        /// <summary>
        /// 注入搜索
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static void UseSearcher(string partnerID, string secret, List<ApiConfig> configs)
        {
            new SearchMiddleware(partnerID, secret, configs);
        }
    }
}
