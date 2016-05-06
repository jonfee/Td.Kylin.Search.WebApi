using Lucene.Net.Documents;
using Lucene.Net.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.Search.WebApi.Enums;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.Core
{
    /// <summary>
    /// 索引库文件转换类
    /// </summary>
    public class IndexFactory
    {
        /// <summary>
        /// 初始化一个索引库文件转换实例
        /// </summary>
        /// <param name="doc"></param>
        public IndexFactory(Document doc)
        {
            _doc = doc;
        }

        private Document _doc;

        private BaseIndexModel _result;

        /// <summary>
        /// 索引库Document转换后的结果
        /// </summary>
        public BaseIndexModel Result
        {
            get
            {
                if (null == _result)
                {
                    _result = GetResult();
                }

                return _result;
            }
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <returns></returns>
        private BaseIndexModel GetResult()
        {
            BaseIndexModel model = null;

            var dtype = (IndexDataType)Enum.Parse(typeof(IndexDataType), _doc.Get("datatype"));

            switch (dtype)
            {
                case IndexDataType.MallProduct:
                    model = GetMallProduct();
                    break;
                case IndexDataType.MerchantProduct:
                    model = GetMerchantproduct();
                    break;
                case IndexDataType.Merchant:
                    model = GetMerchant();
                    break;
                case IndexDataType.Job:
                    model = GetJob();
                    break;
            }
            if (null != model)
            {
                model.DataType = dtype;
            }

            return model;
        }

        /// <summary>
        /// 从索引文档获取精品汇（B2C）商品
        /// </summary>
        /// <returns></returns>
        private MallProduct GetMallProduct()
        {
            var product = new MallProduct();

            product.DataType = IndexDataType.MallProduct;
            product.AreaID = Convert.ToInt32(_doc.Get("areaid"));
            product.AreaLayer = _doc.Get("arealayer");           
            product.CreateTime = DateTime.Parse(_doc.Get("createtime"));
            product.ID = Convert.ToInt64(_doc.Get("id"));
            product.Name = _doc.Get("name");
            product.Pic = _doc.Get("pic");
            product.UpdateTime = DateTime.Parse(_doc.Get("updatetime"));

            product.MarketPrice = decimal.Parse(_doc.Get("marketprice"));
            product.CategoryID = Convert.ToInt64(_doc.Get("cateogryid"));
            product.CategoryName = _doc.Get("categoryname");
            product.ProductID = Convert.ToInt64(_doc.Get("productid"));
            product.SalePrice = decimal.Parse(_doc.Get("saleprice"));
            product.SKU = _doc.Get("sku");
            product.Specs = _doc.Get("specs");
            product.TagIDs = _doc.Get("tagids");
            product.TagNames = _doc.Get("tagnames"); 

            return product;
        }
        
        /// <summary>
        /// 从索引文档获取附近购（商家）商品
        /// </summary>
        /// <returns></returns>
        private MerchantProduct GetMerchantproduct()
        {
            var product = new MerchantProduct();

            product.DataType = IndexDataType.MerchantProduct;
            product.AreaID = Convert.ToInt32(_doc.Get("areaid"));
            product.AreaLayer = _doc.Get("arealayer");
            product.CreateTime = DateTime.Parse(_doc.Get("createtime"));
            product.ID = Convert.ToInt64(_doc.Get("id"));
            product.Name = _doc.Get("name");
            product.Pic = _doc.Get("pic");
            product.UpdateTime = DateTime.Parse(_doc.Get("updatetime"));

            product.OriginalPrice = decimal.Parse(_doc.Get("originalprice"));
            product.SystemCategoryID = Convert.ToInt64(_doc.Get("systemcategoryid"));
            product.SystemCategoryName = _doc.Get("systemcategoryname");
            product.MerchantID = Convert.ToInt64(_doc.Get("merchantid"));
            product.MerchantName = _doc.Get("merchantname");
            product.SalePrice = decimal.Parse(_doc.Get("saleprice"));
            product.Specification = _doc.Get("specification");            
            product.Latitude = float.Parse(_doc.Get("latitude"));
            product.Longitude= float.Parse(_doc.Get("longitude"));
            

            return product;
        }

        /// <summary>
        /// 从索引文档获取商家
        /// </summary>
        /// <returns></returns>
        private Merchant GetMerchant()
        {
            var merchant = new Merchant();

            merchant.DataType = IndexDataType.Merchant;
            merchant.AreaID = Convert.ToInt32(_doc.Get("areaid"));
            merchant.AreaLayer = _doc.Get("arealayer");
            merchant.CreateTime = DateTime.Parse(_doc.Get("createtime"));
            merchant.ID = Convert.ToInt64(_doc.Get("id"));
            merchant.Name = _doc.Get("name");
            merchant.Pic = _doc.Get("pic");
            merchant.UpdateTime = DateTime.Parse(_doc.Get("updatetime"));

            merchant.Latitude = float.Parse(_doc.Get("latitude"));
            merchant.Longitude = float.Parse(_doc.Get("longitude"));
            merchant.BusinessBeginTime = _doc.Get("businessbegintime");
            merchant.BusinessEndTime = _doc.Get("businessendtime");
            merchant.CertificateStatus = Convert.ToInt32(_doc.Get("certificatestatus"));
            merchant.IndustryID = Convert.ToInt64(_doc.Get("industryid"));
            merchant.IndustryName = _doc.Get("industryname");
            merchant.LinkMan = _doc.Get("linkman");
            merchant.LocationPlace = _doc.Get("locationplace");
            merchant.Mobile = _doc.Get("moble");
            merchant.Phone = _doc.Get("phone");
            merchant.Street = _doc.Get("street");            

            return merchant;
        }

        /// <summary>
        /// 从索引文档获取招聘信息
        /// </summary>
        /// <returns></returns>
        private Job GetJob()
        {
            var job = new Job();

            job.DataType = IndexDataType.Job;
            job.AreaID = Convert.ToInt32(_doc.Get("areaid"));
            job.AreaLayer = _doc.Get("arealayer");
            job.CreateTime = DateTime.Parse(_doc.Get("createtime"));
            job.ID = Convert.ToInt64(_doc.Get("id"));
            job.Name = _doc.Get("name");
            job.Pic = _doc.Get("pic");
            job.UpdateTime = DateTime.Parse(_doc.Get("updatetime"));

            job.Latitude = float.Parse(_doc.Get("latitude"));
            job.Longitude = float.Parse(_doc.Get("longitude"));
            job.CategoryID = Convert.ToInt64(_doc.Get("categoryid"));
            job.Count = Convert.ToInt32(_doc.Get("count"));
            job.JobType = Convert.ToInt32(_doc.Get("jobtype"));
            job.MaxAge= Convert.ToInt32(_doc.Get("maxage"));
            job.MaxMonthly= Convert.ToInt32(_doc.Get("maxmonthly"));
            job.MerchantCertificateStatus= Convert.ToInt32(_doc.Get("merchantcertificatestatus"));
            job.MerchantID= Convert.ToInt64(_doc.Get("merchantid"));
            job.MerchantName = _doc.Get("merchantname");
            job.MinAge= Convert.ToInt32(_doc.Get("minage"));
            job.MinEducation= Convert.ToInt32(_doc.Get("mineducation")); 
            job.MinJobYearsType= Convert.ToInt32(_doc.Get("minjobyearstype"));
            job.MinMonthly= Convert.ToInt32(_doc.Get("minmonthly"));
            job.Sex= Convert.ToInt32(_doc.Get("sex"));
            job.Welfares= Convert.ToInt32(_doc.Get("welfares"));
            job.WordAddress = _doc.Get("wordaddress");

            return job;
        }
    }
}
