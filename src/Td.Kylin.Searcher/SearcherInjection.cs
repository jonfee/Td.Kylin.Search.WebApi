using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;

namespace Td.Kylin.Searcher
{
    /// <summary>
    /// 搜索器注入类
    /// </summary>
    public static class SearcherInjection
    {
        /// <summary>
        /// 注入搜索
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="partnerID">搜索接口合作者ID</param>
        /// <param name="secret">搜索接口合作者密钥</param>
        /// <param name="configs">搜索接口配置</param>
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
        /// <param name="partnerID">搜索接口合作者ID</param>
        /// <param name="secret">搜索接口合作者密钥</param>
        /// <param name="configs">搜索接口配置</param>
        /// <returns></returns>
        public static void UseSearcher(string partnerID, string secret, List<ApiConfig> configs)
        {
            new SearchMiddleware(partnerID, secret, configs);
        }
    }
}
