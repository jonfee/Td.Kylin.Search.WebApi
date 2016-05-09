using Lucene.Net.Documents;
using System.Collections.Generic;
using Td.Common;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Search.WebApi.Enums;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.Core
{
    /// <summary>
    /// Lucene.Net.Document 构造器
    /// </summary>
    public class DocumentFactory
    {
        public DocumentFactory(BaseIndexModel model)
        {
            _model = model;
        }

        private BaseIndexModel _model;

        private Document _result;

        /// <summary>
        /// 索引数据转换为Document后的结果
        /// </summary>
        public Document Result
        {
            get
            {
                if (null == _result)
                {
                    _result = GetDocument();
                }

                return _result;
            }
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        private Document GetDocument()
        {
            Document document = null;

            switch (_model.DataType)
            {
                case IndexDataType.MallProduct:
                    document = GetByMallProduct();
                    break;
                case IndexDataType.MerchantProduct:
                    document = GetByMerchantProduct();
                    break;
                case IndexDataType.Merchant:
                    document = GetByMerchant();
                    break;
                case IndexDataType.Job:
                    document = GetByJob();
                    break;
            }

            return document;
        }

        private Document GetByMallProduct()
        {
            if (_model is MallProduct)
            {
                var item = _model as MallProduct;

                if (null == item) return null;

                Document document = new Document();

                document.Add(new Field("id", item.ID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("datatype", item.DataType.ToString("d"), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("areaid", item.AreaID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("arealayer", item.AreaLayer ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("name", item.Name ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("pic", item.Pic ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("createtime", item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("updatetime", item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("desc", string.Format("{0},{1},{2},{3}", item.Specs, item.SKU, item.CategoryName, item.TagNames), Field.Store.YES, Field.Index.ANALYZED));

                document.Add(new Field("productid", item.ProductID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("specs", item.Specs ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("sku", item.SKU ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("categoryid", item.CategoryID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("categoryname", item.CategoryName ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("tagids", item.TagIDs ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("tagnames", item.TagNames ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("marketprice", item.MarketPrice.ToString("0.00"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("saleprice", item.SalePrice.ToString("0.00"), Field.Store.YES, Field.Index.NOT_ANALYZED));

                return document;
            }

            return null;
        }

        private Document GetByMerchantProduct()
        {
            if(_model is MerchantProduct)
            {
                var item = _model as MerchantProduct;

                if (null == item) return null;

                Document document = new Document();

                document.Add(new Field("id", item.ID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("datatype", item.DataType.ToString("d"), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("areaid", item.AreaID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("arealayer", item.AreaLayer ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("name", item.Name ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("pic", item.Pic ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("createtime", item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("updatetime", item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("desc", string.Format("{0},{1},{2}", item.MerchantName, item.Specification, item.SystemCategoryName), Field.Store.YES, Field.Index.ANALYZED));

                document.Add(new Field("merchantid", item.MerchantID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("merchantname", item.MerchantName ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("specification", item.Specification ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("systemcategoryid", item.SystemCategoryID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("systemcategoryname", item.SystemCategoryName ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("latitude", item.Latitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("longitude", item.Longitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("originalprice", item.OriginalPrice.ToString("0.00"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("saleprice", item.SalePrice.ToString("0.00"), Field.Store.YES, Field.Index.NOT_ANALYZED));

                return document;
            }

            return null;
        }

        private Document GetByMerchant()
        {
            if(_model is Merchant)
            {
                var item = _model as Merchant;

                if (null == item) return null;

                Document document = new Document();

                document.Add(new Field("id", item.ID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("datatype", item.DataType.ToString("d"), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("areaid", item.AreaID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("arealayer", item.AreaLayer ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("name", item.Name ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("pic", item.Pic ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("createtime", item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("updatetime", item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("desc", string.Format("{0},{1},{2},{3}", item.IndustryName, item.LocationPlace, item.Street, item.LinkMan), Field.Store.YES, Field.Index.ANALYZED));

                document.Add(new Field("latitude", item.Latitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("longitude", item.Longitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("mobile", item.Mobile ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("industryid", item.IndustryID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("industryname", item.IndustryName ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("locationplace", item.LocationPlace ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("street", item.Street ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("linkman", item.LinkMan ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("phone", item.Phone ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("certificatestatus", item.CertificateStatus.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("businessbegintime", item.BusinessBeginTime ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("businessendtime", item.BusinessEndTime ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));

                return document;
            }

            return null;
        }

        private Document GetByJob()
        {
            if(_model is Job)
            {
                var item = _model as Job;

                if (null == item) return null;

                Document document = new Document();

                //工作性质
                string jobtype = EnumUtility.GetEnumDescription<JobNature>(item.JobType);
                //最低学历
                string education = EnumUtility.GetEnumDescription<Education>(item.MinEducation);
                //福利
                List<string> welfares = new List<string>();
                foreach (var kv in EnumUtility.GetEnumDescriptions(typeof(JobWelfare)))
                {
                    if ((item.Welfares & kv.Key) == kv.Key)
                    {
                        welfares.Add(kv.Value);
                    }
                }

                document.Add(new Field("id", item.ID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("datatype", item.DataType.ToString("d"), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("areaid", item.AreaID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("arealayer", item.AreaLayer ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("name", item.Name ?? string.Empty, Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("pic", item.Pic ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("createtime", item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("updatetime", item.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("desc", string.Format("{0},{1},{2},{3},{4}", item.MerchantName, item.WordAddress, jobtype, education, string.Join(",", welfares)), Field.Store.YES, Field.Index.ANALYZED));

                document.Add(new Field("latitude", item.Latitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("longitude", item.Longitude.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("merchantid", item.MerchantID.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                document.Add(new Field("merchantname", item.MerchantName ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));
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
                document.Add(new Field("wordaddress", item.WordAddress ?? string.Empty, Field.Store.YES, Field.Index.NOT_ANALYZED));

                return document;
            }

            return null;
        }
    }
}
