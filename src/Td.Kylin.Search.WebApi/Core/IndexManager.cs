using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Td.Kylin.Search.WebApi.Enums;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.Core
{
    /// <summary>
    /// 索引库文件管理
    /// </summary>
    public class IndexManager
    {
        private volatile static IndexManager _instance;

        private static readonly object mylock = new object();

        public static IndexManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (mylock)
                    {
                        if (null == _instance)
                        {
                            _instance = new IndexManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private IndexManager()
        {
            indexQueue = new Queue<QueueModel>();
        }

        /// <summary>
        /// 索引文件操作队列
        /// </summary>
        private Queue<QueueModel> indexQueue;

        /// <summary>
        /// 将新文件添加到队列结尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Insert<T>(T item) where T : BaseIndexModel
        {
            if (null != item)
            {
                QueueModel model = new QueueModel();
                model.ActionMode = ActionMode.Insert;
                model.Data = item;
                model.DataType = item.DataType;
                model.ID = item.ID;

                indexQueue.Enqueue(model);
            }
        }

        /// <summary>
        /// 将更新文件添加到队列结尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Modify<T>(T item) where T : BaseIndexModel
        {
            if (null != item)
            {
                QueueModel model = new QueueModel();
                model.ActionMode = ActionMode.Modify;
                model.Data = item;
                model.DataType = item.DataType;
                model.ID = item.ID;

                indexQueue.Enqueue(model);
            }
        }

        /// <summary>
        /// 将删除文件添加到队列结尾
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="id"></param>
        public void Delete(DataType dataType, long id)
        {
            if (id > 0)
            {
                QueueModel model = new QueueModel();
                model.ActionMode = ActionMode.Insert;
                model.Data = null;
                model.DataType = dataType;
                model.ID = id;

                indexQueue.Enqueue(model);
            }
        }

        /// <summary>
        /// 开启新线程处理索引队列
        /// </summary>
        public void StartNewThread() 
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(QueueToIndex));
        }

        /// <summary>
        /// 将队列中的数据取出来插入索引库中
        /// </summary>
        /// <param name="para"></param>
        private void QueueToIndex(object para)
        {
            while (true)
            {
                if (indexQueue.Count > 0)
                {
                    IndexDoWork();
                }
                else
                {
                    Thread.Sleep(5000);
                }
            }
        }

        /// <summary>
        /// 更新索引库操作
        /// </summary>
        private void IndexDoWork()
        {
            #region 
            Dictionary<int, FSDirectory> dicAreaDirectory = new Dictionary<int, FSDirectory>();
            FSDirectory mallproductDirectory = null;
            FSDirectory merchantproductDirectory = null;
            FSDirectory merchantDirectory = null;
            FSDirectory jobDirectory = null;

            Dictionary<int, IndexWriter> dicAreaWriter = new Dictionary<int, IndexWriter>();
            IndexWriter mallproductWriter = null;
            IndexWriter merchantproductWriter = null;
            IndexWriter merchantWriter = null;
            IndexWriter jobWriter = null;
            #endregion

            while (indexQueue.Count > 0)
            {
                QueueModel model = indexQueue.Dequeue();

                var data = model.Data as BaseIndexModel;

                #region//当前索引需要用到的区域 FSDirectory 文件
                FSDirectory areaDirectory = null;
                if (dicAreaDirectory.ContainsKey(data.AreaID))
                {
                    areaDirectory = dicAreaDirectory[data.AreaID];
                }
                else
                {
                    areaDirectory = FSDirectory.Open(new DirectoryInfo(IndexPathConfiguration.GetAreaPath(data.AreaID)), new NativeFSLockFactory());
                    dicAreaDirectory.Add(data.AreaID, areaDirectory);
                }
                #endregion
                #region//当前索引需要用到的区域 IndexWriter
                IndexWriter areaWriter = null;
                if (dicAreaWriter.ContainsKey(data.AreaID))
                {
                    areaWriter = dicAreaWriter[data.AreaID];
                }
                else
                {
                    bool isExist = IsIndexExists(areaDirectory, true);
                    areaWriter = new IndexWriter(mallproductDirectory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
                    dicAreaWriter.Add(data.AreaID, areaWriter);
                }
                #endregion

                #region  is MallProduct
                if (data is MallProduct)
                {
                    var mallProduct = (MallProduct)data;

                    if (null == mallproductDirectory) mallproductDirectory = FSDirectory.Open(new DirectoryInfo(IndexPathConfiguration.GetMallProductPath()), new NativeFSLockFactory());
                    if (null == mallproductWriter)
                    {
                        bool isExist = IsIndexExists(mallproductDirectory, true);
                        mallproductWriter = new IndexWriter(mallproductDirectory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
                    }

                    switch (model.ActionMode)
                    {
                        case ActionMode.Delete:
                            DeleteIndex(areaWriter, model.ID);
                            DeleteIndex(mallproductWriter, model.ID);
                            break;
                        case ActionMode.Insert:
                            AddIndexByMallProduct(areaWriter, mallProduct);
                            AddIndexByMallProduct(mallproductWriter, mallProduct);
                            break;
                        case ActionMode.Modify:
                            ModifyIndexByMallProduct(areaWriter, mallProduct);
                            ModifyIndexByMallProduct(mallproductWriter, mallProduct);
                            break;
                    }
                }
                #endregion
                #region is MerchantProduct
                else if (data is MerchantProduct)
                {
                    var merchantProduct = (MerchantProduct)data;

                    if (null == merchantproductDirectory) merchantproductDirectory = FSDirectory.Open(new DirectoryInfo(IndexPathConfiguration.GetMerchantProductPath()), new NativeFSLockFactory());
                    if (null == merchantproductWriter)
                    {
                        bool isExist = IsIndexExists(merchantproductDirectory, true);
                        merchantproductWriter = new IndexWriter(merchantproductDirectory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
                    }

                    switch (model.ActionMode)
                    {
                        case ActionMode.Delete:
                            DeleteIndex(areaWriter, model.ID);
                            DeleteIndex(merchantproductWriter, model.ID);
                            break;
                        case ActionMode.Insert:
                            AddIndexByMerchantProduct(areaWriter, merchantProduct);
                            AddIndexByMerchantProduct(merchantproductWriter, merchantProduct);
                            break;
                        case ActionMode.Modify:
                            ModifyIndexByMerchantProduct(areaWriter, merchantProduct);
                            ModifyIndexByMerchantProduct(merchantproductWriter, merchantProduct);
                            break;
                    }
                }
                #endregion
                #region is Merchant
                else if (data is Merchant)
                {
                    var merchant = (Merchant)data;

                    if (null == merchantDirectory) merchantDirectory = FSDirectory.Open(new DirectoryInfo(IndexPathConfiguration.GetMerchantPath()), new NativeFSLockFactory());
                    if (null == merchantWriter)
                    {
                        bool isExist = IsIndexExists(merchantDirectory, true);
                        merchantWriter = new IndexWriter(merchantDirectory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
                    }

                    switch (model.ActionMode)
                    {
                        case ActionMode.Delete:
                            DeleteIndex(areaWriter, model.ID);
                            DeleteIndex(merchantWriter, model.ID);
                            break;
                        case ActionMode.Insert:
                            AddIndexByMerchant(areaWriter, merchant);
                            AddIndexByMerchant(merchantWriter, merchant);
                            break;
                        case ActionMode.Modify:
                            ModifyIndexByMerchant(areaWriter, merchant);
                            ModifyIndexByMerchant(merchantWriter, merchant);
                            break;
                    }
                }
                #endregion
                #region is Job
                else if (data is Job)
                {
                    var job = (Job)data;

                    if (null == jobDirectory) jobDirectory = FSDirectory.Open(new DirectoryInfo(IndexPathConfiguration.GetJobPath()), new NativeFSLockFactory());
                    if (null == jobWriter)
                    {
                        bool isExist = IsIndexExists(jobDirectory, true);
                        jobWriter = new IndexWriter(jobDirectory, new PanGuAnalyzer(), !isExist, IndexWriter.MaxFieldLength.UNLIMITED);
                    }

                    switch (model.ActionMode)
                    {
                        case ActionMode.Delete:
                            DeleteIndex(areaWriter, model.ID);
                            DeleteIndex(jobWriter, model.ID);
                            break;
                        case ActionMode.Insert:
                            AddIndexByJob(areaWriter, job);
                            AddIndexByJob(merchantWriter, job);
                            break;
                        case ActionMode.Modify:
                            ModifyIndexByJob(areaWriter, job);
                            ModifyIndexByJob(merchantWriter, job);
                            break;
                    }
                }
                #endregion
            }

            foreach (var wrt in dicAreaWriter.Values)
            {
                wrt.Close();
            }

            foreach (var dir in dicAreaDirectory.Values)
            {
                dir.Close();
            }

            if (mallproductWriter != null) mallproductWriter.Close();
            if (merchantproductWriter != null) merchantproductWriter.Close();
            if (merchantWriter != null) merchantWriter.Close();
            if (jobWriter != null) jobWriter.Close();

            if (mallproductDirectory != null) mallproductDirectory.Close();
            if (merchantproductDirectory != null) merchantproductDirectory.Close();
            if (merchantDirectory != null) merchantDirectory.Close();
            if (jobDirectory != null) jobDirectory.Close();
        }

        #region 添加数据到索引文件

        /// <summary>
        /// 添加精品汇商品到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void AddIndexByMallProduct(IndexWriter writer, MallProduct item)
        {
            Document document = new Document();

            document.Add(new Field("id", item.ID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("datatype", item.DataType.ToString("d"), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("areaid", item.AreaID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("arealayer", item.AreaLayer, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("name", item.Name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("pic", item.Pic, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("createtime", item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("updatetime", item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));

            document.Add(new Field("productid", item.ProductID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("specs", item.Specs, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("sku", item.SKU, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("categoryid", item.CategoryID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("categoryname", item.CategoryName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("tagids", item.TagIDs, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("tagnames", item.TagNames, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("marketprice", item.MarketPrice.ToString("0.00"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("saleprice", item.SalePrice.ToString("0.00"), Field.Store.YES, Field.Index.NOT_ANALYZED));

            writer.AddDocument(document);
        }

        /// <summary>
        /// 添加商家商品到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void AddIndexByMerchantProduct(IndexWriter writer,MerchantProduct item)
        {
            Document document = new Document();

            document.Add(new Field("id", item.ID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("datatype", item.DataType.ToString("d"), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("areaid", item.AreaID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("arealayer", item.AreaLayer, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("name", item.Name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("pic", item.Pic, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("createtime", item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("updatetime", item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));

            document.Add(new Field("merchantid", item.MerchantID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("merchantname", item.MerchantName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("specification", item.Specification, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("systemcategoryid", item.SystemCategoryID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("systemcategoryname", item.SystemCategoryName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("latitude", item.Latitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("longitude", item.Longitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("originalprice", item.OriginalPrice.ToString("0.00"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("saleprice", item.SalePrice.ToString("0.00"), Field.Store.YES, Field.Index.NOT_ANALYZED));

            writer.AddDocument(document);
        }

        /// <summary>
        /// 添加商家到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void AddIndexByMerchant(IndexWriter writer, Merchant item)
        {
            Document document = new Document();

            document.Add(new Field("id", item.ID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("datatype", item.DataType.ToString("d"), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("areaid", item.AreaID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("arealayer", item.AreaLayer, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("name", item.Name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("pic", item.Pic, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("createtime", item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("updatetime", item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            
            document.Add(new Field("latitude", item.Latitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("longitude", item.Longitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Mobile", item.Mobile, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("IndustryID", item.IndustryID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("IndustryName", item.IndustryName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("LocationPlace", item.LocationPlace, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Street", item.Street, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("LinkMan", item.LinkMan, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Phone", item.Phone, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("CertificateStatus", item.CertificateStatus.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("BusinessBeginTime", item.BusinessBeginTime, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("BusinessEndTime", item.BusinessEndTime, Field.Store.YES, Field.Index.NOT_ANALYZED));

            writer.AddDocument(document);
        }

        /// <summary>
        /// 添加职位到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void AddIndexByJob(IndexWriter writer, Job item)
        {
            Document document = new Document();

            document.Add(new Field("id", item.ID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("datatype", item.DataType.ToString("d"), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("areaid", item.AreaID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("arealayer", item.AreaLayer, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("name", item.Name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("pic", item.Pic, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("createtime", item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("updatetime", item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));

            document.Add(new Field("latitude", item.Latitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("longitude", item.Longitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("merchantid", item.MerchantID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("merchantname", item.MerchantName, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("categoryid", item.CategoryID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("count", item.Count.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("minmonthly", item.MinMonthly.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("maxmonthly", item.MaxMonthly.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("merchantcertificatestatus", item.MerchantCertificateStatus.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("sex", item.Sex.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("minage", item.MinAge.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("maxage", item.MaxAge.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("mineducation", item.MinEducation.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("minjobyearstype", item.MinJobYearsType.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("jobtype", item.JobType.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("welfares", item.Welfares.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("wordaddress", item.WordAddress.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

            writer.AddDocument(document);
        }

        #endregion

        #region 删除数据到索引文件

        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="id"></param>
        private void DeleteIndex(IndexWriter writer, long id)
        {
            writer.DeleteDocuments(new Term("id", id.ToString()));
        }

        #endregion

        #region 修改数据到索引文件

        /// <summary>
        /// 修改精品汇商品到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void ModifyIndexByMallProduct(IndexWriter writer, MallProduct item)
        {
            DeleteIndex(writer, item.ID);
            AddIndexByMallProduct(writer, item);
        }

        /// <summary>
        /// 修改商家商品到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void ModifyIndexByMerchantProduct(IndexWriter writer, MerchantProduct item)
        {
            DeleteIndex(writer, item.ID);
            AddIndexByMerchantProduct(writer, item);
        }

        /// <summary>
        /// 修改商家到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void ModifyIndexByMerchant(IndexWriter writer, Merchant item)
        {
            DeleteIndex(writer, item.ID);
            AddIndexByMerchant(writer, item);
        }

        /// <summary>
        /// 修改职位到索引文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="item"></param>
        private void ModifyIndexByJob(IndexWriter writer, Job item)
        {
            DeleteIndex(writer, item.ID);
            AddIndexByJob(writer, item);
        }

        #endregion

        /// <summary>
        /// 检测索引文件是否存在
        /// </summary>
        /// <param name="directory">索引文件</param>
        /// <param name="unlock">是否需要手动解锁</param>
        /// <returns></returns>
        private bool IsIndexExists(FSDirectory directory, bool unlock = true)
        {
            bool isExist = IndexReader.IndexExists(directory);
            if (isExist && unlock)
            {
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }

            return isExist;
        }
    }
}
