using Lucene.Net.Index;
using System.Collections;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.WriterManager
{
    /// <summary>
    /// 区域数据索引库管理
    /// </summary>
    public sealed class AreaIndexManager : BaseIndexManager<BaseIndexModel>
    {
        #region 单例

        private volatile static AreaIndexManager _instance;

        private static readonly object locker = new object();

        public static AreaIndexManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (locker)
                    {
                        if (null == _instance)
                        {
                            _instance = new AreaIndexManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private AreaIndexManager()
        {
            areaHash = Hashtable.Synchronized(new Hashtable());
        }

        #endregion

        /// <summary>
        /// 区域索引库对象Hashtable集合
        /// </summary>
        Hashtable areaHash;

        protected override IndexWriter GetIndex(QueueModel state)
        {
            if (null == state) return null;

            int areaID = state.AreaID;

            IndexConfig config = null;
            if (!areaHash.ContainsKey(areaID) && null == areaHash[areaID])
            {
                config = new IndexConfig();
                config.IndexPath = IndexConfiguration.GetAreaPath(areaID);

                areaHash[areaID] = config;
            }
            else
            {
                config = areaHash[areaID] as IndexConfig;
            }

            return config?.Writer;
        }


        protected override void Commit()
        {
           foreach(var config in areaHash.Values)
            {
                if(config is IndexConfig)
                {
                    var item = (IndexConfig)config;

                    if (null != item)
                    {
                        item.Writer.Optimize();
                        item.Writer.Commit();
                    }
                }
            }
        }
    }
}
