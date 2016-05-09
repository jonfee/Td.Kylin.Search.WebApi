using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System.IO;
using Td.Kylin.Search.WebApi.Core;

namespace Td.Kylin.Search.WebApi.WriterManager
{
    /// <summary>
    /// 索引库配置类
    /// </summary>
    public class IndexConfig
    {
        private string _indexPath;
        /// <summary>
        /// 索引库路径
        /// </summary>
        public string IndexPath
        {
            get
            {
                return _indexPath;
            }
            set
            {
                this._indexPath = value;
            }
        }

        private IndexWriter _writer;
        /// <summary>
        /// IndexWriter
        /// </summary>
        public IndexWriter Writer
        {
            get
            {
                try
                {
                    if (null == _writer && !string.IsNullOrWhiteSpace(IndexPath))
                    {
                        FSDirectory _directory = FSDirectory.Open(new DirectoryInfo(IndexPath), new NativeFSLockFactory());

                        bool isExist = IndexReader.IndexExists(_directory);

                        if (IndexWriter.IsLocked(_directory))
                        {
                            IndexWriter.Unlock(_directory);
                        }

                        _writer = new IndexWriter(_directory, new StandardAnalyzer(IndexConfiguration.LuceneMatchVersion), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
                    }
                }
                catch { }

                return _writer;
            }
        }
    }
}
