using System;
using System.Collections.Generic;

namespace Td.Kylin.Searcher
{
    /// <summary>
    /// 搜索器注入类
    /// </summary>
    public static class SearcherExtensions
    {
        /// <summary>
        /// 注入搜索
        /// </summary>
        /// <param name="partnerID">搜索接口合作者ID</param>
        /// <param name="secret">搜索接口合作者密钥</param>
        /// <param name="configs">搜索接口配置</param>
        /// <returns></returns>
        public static void Factory(string partnerID, string secret, List<ApiConfig> configs)
        {
            if (string.IsNullOrWhiteSpace(partnerID))
            {
                throw new InvalidOperationException("partnerID is empty");
            }

            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidOperationException("secret is empty");
            }

            ApiConfigRoot.PartnerID = partnerID;
            ApiConfigRoot.Secret = secret;
            ApiConfigRoot.Configs = configs;
        }
    }
}
