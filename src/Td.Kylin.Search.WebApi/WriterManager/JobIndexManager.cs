using Lucene.Net.Index;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.WriterManager
{
    /// <summary>
    /// 招聘数据索引库管理
    /// </summary>
    public sealed class JobIndexManager : BaseIndexManager<Job>
    {
        #region 单例

        private volatile static JobIndexManager _instance;

        private static readonly object locker = new object();

        public static JobIndexManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (locker)
                    {
                        if (null == _instance)
                        {
                            _instance = new JobIndexManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private JobIndexManager()
        {
            _config = new IndexConfig();
            _config.IndexPath = IndexConfiguration.GetJobPath();
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
