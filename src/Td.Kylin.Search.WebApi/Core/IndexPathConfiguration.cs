using System.IO;

namespace Td.Kylin.Search.WebApi.Core
{
    public class IndexConfiguration
    {
        /// <summary>
        /// 分词版本
        /// </summary>
        public static Lucene.Net.Util.Version LuceneMatchVersion { get { return Lucene.Net.Util.Version.LUCENE_29; } }

        /// <summary>
        /// 索引文件根目录
        /// </summary>
        private static string indexRoot = Startup.Configuration["IndexPath:Root"];

        /// <summary>
        /// 获取区域索引文件路径（如 北京：D:\\luceneindex\area\110000）
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public static string GetAreaPath(int areaID)
        {
            string path = string.Format(@"area\{0}", areaID);

            return Path.Combine(Startup.WebRootPath, indexRoot, path);
        }

        /// <summary>
        /// 获取精品汇（B2C）商品索引文件路径（如 D:\\luceneindex\mallproduct）
        /// </summary>
        /// <returns></returns>
        public static string GetMallProductPath()
        {
            string path = @"mallproduct";

            return Path.Combine(Startup.WebRootPath, indexRoot, path);
        }

        /// <summary>
        /// 获取附近购（商家）商品索引文件路径（如 D:\\luceneindex\merchantproduct）
        /// </summary>
        /// <returns></returns>
        public static string GetMerchantProductPath()
        {
            string path = @"merchantproduct";

            return Path.Combine(Startup.WebRootPath, indexRoot, path);
        }

        /// <summary>
        /// 获取商家索引文件路径（如 D:\\luceneindex\merchant）
        /// </summary>
        /// <returns></returns>
        public static string GetMerchantPath()
        {
            string path = @"merchant";

            return Path.Combine(Startup.WebRootPath, indexRoot, path);
        }

        /// <summary>
        /// 获取职位索引文件路径（如 D:\\luceneindex\job）
        /// </summary>
        /// <returns></returns>
        public static string GetJobPath()
        {
            string path = @"job";

            return Path.Combine(Startup.WebRootPath, indexRoot, path);
        }
    }
}
