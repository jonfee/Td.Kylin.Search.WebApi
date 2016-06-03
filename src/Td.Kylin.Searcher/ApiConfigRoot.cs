using System.Collections.Generic;

namespace Td.Kylin.Searcher
{
    internal sealed class ApiConfigRoot
    {
        /// <summary>
        /// 合作者ID
        /// </summary>
        public static string PartnerID { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public static string Secret { get; set; }

        /// <summary>
        /// 接口配置
        /// </summary>
        public static List<ApiConfig> Configs { get; set; }
    }
}
