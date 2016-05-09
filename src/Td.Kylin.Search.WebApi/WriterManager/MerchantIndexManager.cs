using Lucene.Net.Index;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.WriterManager
{
    /// <summary>
    /// 商家数据索引库管理
    /// </summary>
    public sealed class MerchantIndexManager : BaseIndexManager<Merchant>
    {
        #region 单例

        private volatile static MerchantIndexManager _instance;

        private static readonly object locker = new object();

        public static MerchantIndexManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (locker)
                    {
                        if (null == _instance)
                        {
                            _instance = new MerchantIndexManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private MerchantIndexManager()
        {
            _config = new IndexConfig();
            _config.IndexPath = IndexConfiguration.GetMerchantPath();
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
