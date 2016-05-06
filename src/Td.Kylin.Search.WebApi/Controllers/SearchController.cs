using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.IO;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.IndexModel;
using Td.Kylin.WebApi;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Td.Kylin.Search.WebApi.Controllers
{
    [Route("v1")]
    public class SearchController : BaseController
    {
        [HttpGet("search")]
        public IActionResult Search(int areaID, string wd, int pageIndex, int pageSize)
        {
            string indexPath = IndexConfiguration.GetAreaPath(areaID);

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());

            IndexReader reader = IndexReader.Open(directory, true);

            IndexSearcher searcher = new IndexSearcher(reader);

            Analyzer analyzer = new StandardAnalyzer(IndexConfiguration.LuceneMatchVersion);

            string[] fields = new[] { "name", "desc" };

            Occur[] flags = new Occur[] { Occur.SHOULD, Occur.SHOULD };

            Query query = MultiFieldQueryParser.Parse(IndexConfiguration.LuceneMatchVersion, wd, fields, flags, analyzer);

            var docs = searcher.Search(query, 200);

            List<BaseIndexModel> list = new List<BaseIndexModel>();

            foreach (var scoreDoc in docs.ScoreDocs)
            {
                Document doc = searcher.Doc(scoreDoc.Doc);

                var data = new IndexFactory(doc).Result;

                list.Add(data);
            }

            return Success(list);
        }
    }
}
