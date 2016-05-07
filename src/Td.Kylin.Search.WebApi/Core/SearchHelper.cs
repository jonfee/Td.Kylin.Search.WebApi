using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.Core
{
    public class SearchHelper
    {
        /// <summary>
        /// 按区域搜索（返回所有）
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="keyword"></param>
        /// <param name="fields"></param>
        /// <param name="flags"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static List<BaseIndexModel> AreaSearch(int areaID, string keyword, string[] fields, Occur[] flags, Sort sort)
        {
            //获取索引文件路径
            string indexPath = IndexConfiguration.GetAreaPath(areaID);

            var list = Search<BaseIndexModel>(indexPath, keyword, fields, flags, sort);

            return list;
        }

        /// <summary>
        /// 按区域搜索（带分页）
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="fields">索引字段</param>
        /// <param name="flags">多字段检索结果集之间的包含关系</param>
        /// <param name="sort">排序</param>
        /// <param name="pageIndex">当前搜索页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="count">总搜索结果数</param>
        /// <returns></returns>
        public static List<BaseIndexModel> AreaSearch(int areaID, string keyword, string[] fields, Occur[] flags, Sort sort, int pageIndex, int pageSize, out int count)
        {
            //获取索引文件路径
            string indexPath = IndexConfiguration.GetAreaPath(areaID);

            var list = Search<BaseIndexModel>(indexPath, keyword, fields, flags, sort, pageIndex, pageSize, out count);

            return list;
        }

        /// <summary>
        /// 搜索（返回所有）
        /// </summary>
        /// <param name="indexPath">返回结果数据类型</param>
        /// <param name="keyword"></param>
        /// <param name="fields"></param>
        /// <param name="flags"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static List<T> Search<T>(string indexPath, string keyword, string[] fields, Occur[] flags, Sort sort) where T : BaseIndexModel
        {
            if (null == fields || fields.Length < 1) return null;

            string[] queries = new string[fields.Length];

            for (var i = 0; i < fields.Length; i++)
            {
                queries[i] = keyword;
            }

            return Search<T>(indexPath, queries, fields, flags, sort);
        }

        /// <summary>
        /// 搜索（返回所有）
        /// </summary>
        /// <param name="indexPath">返回结果数据类型</param>
        /// <param name="keyword"></param>
        /// <param name="fields"></param>
        /// <param name="flags"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static List<T> Search<T>(string indexPath, string[] queries, string[] fields, Occur[] flags, Sort sort) where T : BaseIndexModel
        {
            if (string.IsNullOrWhiteSpace(indexPath)) return null;

            if (queries == null || queries.Length < 1) return null;

            queries = queries.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

            if (queries.Length != fields.Length) return null;

            if (fields.Length != flags.Length) return null;
            
            Analyzer analyzer = new StandardAnalyzer(IndexConfiguration.LuceneMatchVersion);

            Query query = MultiFieldQueryParser.Parse(IndexConfiguration.LuceneMatchVersion, queries, fields, flags, analyzer);

            var list = Search<T>(indexPath, query, sort);

            return list;
        }

        /// <summary>
        /// 搜索（返回所有）
        /// </summary>
        /// <param name="indexPath">返回结果数据类型</param>
        /// <param name="query">Query</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static List<T> Search<T>(string indexPath, Query query, Sort sort) where T : BaseIndexModel
        {
            if (string.IsNullOrWhiteSpace(indexPath)) return null;

            if (null == query) return null;

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());

            if (null == directory) return null;

            IndexReader reader = IndexReader.Open(directory, true);

            IndexSearcher searcher = new IndexSearcher(reader);

            ScoreDoc[] docs = null;
            if (null == sort)
            {
                docs = searcher.Search(query, 0).ScoreDocs;
            }
            else
            {
                docs = searcher.Search(query, null, 0, sort).ScoreDocs;
            }

            List<T> list = new List<T>();

            foreach (var scoreDoc in docs)
            {
                Document doc = searcher.Doc(scoreDoc.Doc);

                var data = new IndexFactory(doc).Result as T;

                if (null != data) list.Add(data);
            }

            return list;
        }

        /// <summary>
        /// 搜索（带分页）
        /// </summary>
        /// <typeparam name="T">返回结果数据类型</typeparam>
        /// <param name="indexPath">索引文件路径</param>
        /// <param name="keyword">关键词</param>
        /// <param name="fields">搜索的字段</param>
        /// <param name="flags">多字段检索结果集之间的包含关系</param>
        /// <param name="sort">排序</param>
        /// <param name="pageIndex">当前搜索页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="count">总搜索结果数</param>
        /// <returns></returns>
        public static List<T> Search<T>(string indexPath, string keyword, string[] fields, Occur[] flags, Sort sort, int pageIndex, int pageSize, out int count) where T : BaseIndexModel
        {
            count = 0;

            if (null == fields || fields.Length < 1) return null;

            string[] queries = new string[fields.Length];

            for (var i = 0; i < fields.Length; i++)
            {
                queries[i] = keyword;
            }

            return Search<T>(indexPath, queries, fields, flags, sort, pageIndex, pageSize, out count);
        }

        /// <summary>
        /// 搜索（带分页）
        /// </summary>
        /// <typeparam name="T">返回结果数据类型</typeparam>
        /// <param name="indexPath">索引文件路径</param>
        /// <param name="queries">关键词</param>
        /// <param name="fields">搜索的字段</param>
        /// <param name="flags">多字段检索结果集之间的包含关系</param>
        /// <param name="sort">排序</param>
        /// <param name="pageIndex">当前搜索页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="count">总搜索结果数</param>
        /// <returns></returns>
        public static List<T> Search<T>(string indexPath, string[] queries, string[] fields, Occur[] flags, Sort sort, int pageIndex, int pageSize, out int count) where T : BaseIndexModel
        {
            count = 0;

            if (string.IsNullOrWhiteSpace(indexPath)) return null;

            if (queries == null || queries.Length < 1) return null;

            queries = queries.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

            if (queries.Length != fields.Length) return null;

            if (fields.Length != flags.Length) return null;

            //分词器
            Analyzer analyzer = new StandardAnalyzer(IndexConfiguration.LuceneMatchVersion);

            //搜索条件
            Query query = MultiFieldQueryParser.Parse(IndexConfiguration.LuceneMatchVersion, queries, fields, flags, analyzer);

            var list = Search<T>(indexPath, query, sort, pageIndex, pageSize, out count);

            return list;
        }

        /// <summary>
        /// 搜索（带分页）
        /// </summary>
        /// <typeparam name="T">返回结果数据类型</typeparam>
        /// <param name="indexPath">索引文件路径</param>
        /// <param name="query">Query</param>
        /// <param name="sort">排序</param>
        /// <param name="pageIndex">当前搜索页</param>
        /// <param name="pageSize">每页显示数</param>
        /// <param name="count">总搜索结果数</param>
        /// <returns></returns>
        public static List<T> Search<T>(string indexPath, Query query, Sort sort, int pageIndex, int pageSize, out int count) where T : BaseIndexModel
        {
            count = 0;

            if (string.IsNullOrWhiteSpace(indexPath)) return null;

            if (null == query) return null;

            if (pageIndex < 1) pageIndex = 1;

            if (pageSize < 1) pageSize = 1;

            //打开索引文件
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());

            if (null == directory) return null;

            //创建一个只读的索引文件读取实例
            IndexReader reader = IndexReader.Open(directory, true);

            //实例化IndexSearcher搜索器
            IndexSearcher searcher = new IndexSearcher(reader);

            //起始搜索位置
            int start = (pageIndex - 1) * pageSize;

            //Collector
            TopFieldCollector results = TopFieldCollector.Create(sort, start + pageSize, false, false, false, false);

            //搜索
            searcher.Search(query, results);

            //总命中率（精算）
            count = results.TotalHits;

            //获取当前页文档
            var docs = results.TopDocs(start, pageSize).ScoreDocs;

            //定义一个结果返回变量
            List<T> list = new List<T>();

            //遍历当前页文档，并转换为需要的结果类型
            foreach (var scoreDoc in docs)
            {
                Document doc = searcher.Doc(scoreDoc.Doc);

                var data = new IndexFactory(doc).Result as T;

                if (null != data) list.Add(data);
            }

            return list;
        }
    }
}
