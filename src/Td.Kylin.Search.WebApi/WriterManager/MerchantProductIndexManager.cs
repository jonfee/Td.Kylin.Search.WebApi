using Lucene.Net.Index;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.WriterManager
{
    /// <summary>
    /// 附近购（商家）商品数据索引库管理
    /// </summary>
    public sealed class MerchantProductIndexManager:BaseIndexManager<MerchantProduct>
    {
        #region 单例

        private volatile static MerchantProductIndexManager _instance;

        private static readonly object locker = new object();

        public static MerchantProductIndexManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (locker)
                    {
                        if (null == _instance)
                        {
                            _instance = new MerchantProductIndexManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private MerchantProductIndexManager()
        {
            _config = new IndexConfig();
            _config.IndexPath = IndexConfiguration.GetMerchantProductPath();
        }

        #endregion

        private IndexConfig _config;

        protected override IndexWriter GetIndex(QueueModel state)
        {
            if (null != _config)
            {
                return _config.Writer;
            }

            return null;
        }

        protected override void Commit()
        {
            var writer = GetIndex(null);

            if (null != writer)
            {
                writer.Optimize();
                writer.Commit();
            }
        }
    }
}
