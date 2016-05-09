using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.Enums;
using Td.Kylin.Search.WebApi.IndexModel;
using Td.Kylin.WebApi;
using Td.Kylin.WebApi.Filters;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Td.Kylin.Search.WebApi.Controllers
{
    [Route("v1/search")]
    public class SearchController : BaseController
    {
        /**
        * @apiVersion 1.0.0
        * @apiDescription 搜索区域数据。
        * @apiSampleRequest /v1/search
        * @api {get} /v1/search 搜索区域数据
        * @apiName search
        * @apiGroup Search
        * @apiPermission Use
        *
        * @apiParam {long} areaID 区域ID
        * @apiParam {string} keyword 关键词
        * @apiParam {int} pageIndex 当前搜索页码
        * @apiParam {int} pageSize 每页显示数量
        *
        * @apiSuccessExample  正确输出：
        * Content:{
        *    Count:int   总记录数
        *    Data:[ //对象为以下4种对象类型（混合）
        *        //商家
        *        {
        *            "Mobile": string            手机号
        *            "IndustryID": long          行业ID
        *            "IndustryName": string      行业名称
        *            "LocationPlace": string     所在位置
        *            "Street": string            所在街道
        *            "Latitude": double          纬度
        *            "Longitude": double         经度
        *            "LinkMan": string           联系人
        *            "Phone": string             联系电话
        *            "CertificateStatus": int    认证类型（以2的N次方和表示）
        *            "BusinessBeginTime": string 开始营业时间（如：09:00）
        *            "BusinessEndTime": string   结束营业时间（如：22:00）
        *            "ID": long                  商家ID,
        *            "DataType": 3               数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）
        *            "AreaID": int               商家所属区域（一般为具体区县ID）
        *            "AreaLayer": string         商家所属区域路径（如：440000，440300，440301 表示：广东省深圳市罗湖区）
        *            "Name": string              商家名称
        *            "Pic": string               商家图片路径（仅一张，不含文件服务器域名，如：enterprise/.../1231.jpg）
        *            "CreateTime": datetime      创建时间      
        *            "UpdateTime": datetime      最后更新时间
        *            "Desc": string              描述（无实际意义，可忽略）
        *        },
        *        //招聘
        *        {
        *            "MerchantID": long                  商家ID
        *            "MerchantName": string              商家名称
        *            "MerchantCertificateStatus": int    商家认证类型（以2的N次方和表示）
        *            "CategoryID": long                  岗位类型ID
        *            "Count": int                        招聘人数
        *            "MinMonthly": int                   最低提供月薪
        *            "MaxMonthly": int                   最高提供月薪
        *            "Sex": int                          性别（1 男，2 女）
        *            "MinAge": int                       最小年龄限制
        *            "MaxAge": int                       最大年龄限制
        *            "MinEducation": int                 最低学历要求（枚举）
        *            "MinJobYearsType": int              最低工作年限
        *            "JobType": int                      工作性质（1 全职，2 兼职）
        *            "Welfares": int                     福利（枚举，以2的N次方和表示）
        *            "WordAddress": string               工作地点
        *            "Latitude": double                  工作地纬度
        *            "Longitude": double                 工作地经度
        *            "ID": long                          招聘信息ID
        *            "DataType": 4                       数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）
        *            "AreaID": int                       招聘所属区域（一般为具体区县ID）
        *            "AreaLayer": string                 招聘所属区域路径（如：440000,440300,440301 表示：广东省深圳市罗湖区）
        *            "Name": string                      岗位名称
        *            "Pic": string                       （空，可忽略）
        *            "CreateTime": datetime              发布时间
        *            "UpdateTime": datetime              最后更新时间
        *            "Desc": string                      描述（无实际意义，可忽略）
        *        },
        *        //精品汇（B2C）商品
        *        {
        *            "ProductID": long           商品ID
        *             "Specs": string            规格名称
        *             "SKU": string              规格SKU码
        *             "CategoryID": long,        分类ID
        *             "CategoryName": string,    分类名称
        *             "TagIDs": string           商品标签ID集，如："6278435570435817519,6278435570435817518"
        *             "TagNames": string         商品标签名称集，如："智能手机,国产"
        *             "MarketPrice": decimal,    市场价
        *             "SalePrice": decimal,      销售价
        *             "ID": long,                规格SKUID
        *             "DataType": 1              数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）             
        *             "AreaID": int              所属区域ID
        *             "AreaLayer": string        所属区域路径（如：440000,440300 表示：广东省深圳市）
        *             "Name": string             商品名称
        *             "Pic": string              商品图片（仅一张，不含文件服务器域名，如：mall/product/.../121.jpg）
        *             "CreateTime": datetime     发布时间
        *             "UpdateTime": datetime     最后更新时间
        *             "Desc": string             描述（无实际意义，可忽略）
        *         },
        *        //附近购（商家）商家
        *        {
        *           "MerchantID": long              所属商家ID
        *           "MerchantName": string          所属商家名称
        *           "SystemCategoryID": lont        商品所属系统分类ID
        *           "SystemCategoryName": string    商品所属系统分类名称
        *           "OriginalPrice": decimal        商品原价
        *           "SalePrice": decimal            销售价
        *           "Specification": string         规格
        *           "Latitude": double              商家所在位置纬度
        *           "Longitude": double             商家所在位置经度
        *           "ID": long                      商品ID
        *           "DataType": 2                   数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息） 
        *           "AreaID": int                   所属商家所在区域ID
        *           "AreaLayer": string             所属商家所在区域路径（如：440000,440300 表示：广东省深圳市）
        *           "Name": string                  商品名称
        *           "Pic": string                   商品图片（仅一张，不含文件服务器域名，如："micromall/goods/16/05/05/113349i8bwct8ayd.jpg"）
        *           "CreateTime": datetime          发布时间
        *           "UpdateTime": datetime          最后更新时间
        *           "Desc": string                  描述（无实际意义，可忽略）
        *        }
        *    ]
        * }
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpGet()]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Use)]
        public IActionResult Search(int areaID, string keyword, int pageIndex, int pageSize)
        {
            string[] fields = new[] { "name", "desc" };

            Occur[] flags = new Occur[] { Occur.SHOULD, Occur.SHOULD };

            Sort sort = new Sort(new SortField("updatetime", SortField.STRING, true));

            int count = 0;

            var list = SearchHelper.AreaSearch(areaID, keyword, fields, flags, sort, pageIndex, pageSize, out count);

            return Success(new
            {
                Count = count,
                Data = list
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 搜索精品汇商品数据。
        * @apiSampleRequest /v1/search/mallproduct
        * @api {get} /v1/search/mallproduct 搜索精品汇商品数据
        * @apiName MallProductSearch
        * @apiGroup Search
        * @apiPermission Use
        *
        * @apiParam {long} areaID 区域ID（可为null，为null时表示不限制区域）
        * @apiParam {string} keyword 关键词
        * @apiParam {int} pageIndex 当前搜索页码
        * @apiParam {int} pageSize 每页显示数量
        *
        * @apiSuccessExample  正确输出：
        * Content:{
        *    Count:int   总记录数
        *    Data:[
        *        {
        *            "ProductID": long           商品ID
        *             "Specs": string            规格名称
        *             "SKU": string              规格SKU码
        *             "CategoryID": long,        分类ID
        *             "CategoryName": string,    分类名称
        *             "TagIDs": string           商品标签ID集，如："6278435570435817519,6278435570435817518"
        *             "TagNames": string         商品标签名称集，如："智能手机,国产"
        *             "MarketPrice": decimal,    市场价
        *             "SalePrice": decimal,      销售价
        *             "ID": long,                规格SKUID
        *             "DataType": 1              数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）             
        *             "AreaID": int              所属区域ID
        *             "AreaLayer": string        所属区域路径（如：440000,440300 表示：广东省深圳市）
        *             "Name": string             商品名称
        *             "Pic": string              商品图片（仅一张，不含文件服务器域名，如：mall/product/.../121.jpg）
        *             "CreateTime": datetime     发布时间
        *             "UpdateTime": datetime     最后更新时间
        *             "Desc": string             描述（无实际意义，可忽略）
        *         }
        *    ]
        * }
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpGet("mallproduct")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Use)]
        public IActionResult MallProductSearch(int? areaID, string keyword, int pageIndex, int pageSize)
        {
            int count = 0;

            var list = SearchData<MallProduct>(IndexDataType.MallProduct, areaID, keyword, pageIndex, pageSize, out count);

            return Success(new
            {
                Count = count,
                Data = list
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 搜索附近购（商家）商品数据。
        * @apiSampleRequest /v1/search/merchantproduct
        * @api {get} /v1/search/merchantproduct 搜索附近购（商家）商品数据
        * @apiName MerchantProductSearch
        * @apiGroup Search
        * @apiPermission Use
        *
        * @apiParam {long} areaID 区域ID（可为null，为null时表示不限制区域）
        * @apiParam {string} keyword 关键词
        * @apiParam {int} pageIndex 当前搜索页码
        * @apiParam {int} pageSize 每页显示数量
        *
        * @apiSuccessExample  正确输出：
        * Content:{
        *    Count:int   总记录数
        *    Data:[
        *        {
        *           "MerchantID": long              所属商家ID
        *           "MerchantName": string          所属商家名称
        *           "SystemCategoryID": lont        商品所属系统分类ID
        *           "SystemCategoryName": string    商品所属系统分类名称
        *           "OriginalPrice": decimal        商品原价
        *           "SalePrice": decimal            销售价
        *           "Specification": string         规格
        *           "Latitude": double              商家所在位置纬度
        *           "Longitude": double             商家所在位置经度
        *           "ID": long                      商品ID
        *           "DataType": 2                   数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息） 
        *           "AreaID": int                   所属商家所在区域ID
        *           "AreaLayer": string             所属商家所在区域路径（如：440000,440300 表示：广东省深圳市）
        *           "Name": string                  商品名称
        *           "Pic": string                   商品图片（仅一张，不含文件服务器域名，如："micromall/goods/16/05/05/113349i8bwct8ayd.jpg"）
        *           "CreateTime": datetime          发布时间
        *           "UpdateTime": datetime          最后更新时间
        *           "Desc": string                  描述（无实际意义，可忽略）
        *        }
        *    ]
        * }
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpGet("merchantproduct")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Use)]
        public IActionResult MerchantProductSearch(int? areaID, string keyword, int pageIndex, int pageSize)
        {
            int count = 0;

            var list = SearchData<MerchantProduct>(IndexDataType.MerchantProduct, areaID, keyword, pageIndex, pageSize, out count);

            return Success(new
            {
                Count = count,
                Data = list
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 搜索商家数据。
        * @apiSampleRequest /v1/search/merchant
        * @api {get} /v1/search/merchant 搜索商家数据
        * @apiName MerchantSearch
        * @apiGroup Search
        * @apiPermission Use
        *
        * @apiParam {long} areaID 区域ID（可为null，为null时表示不限制区域）
        * @apiParam {string} keyword 关键词
        * @apiParam {int} pageIndex 当前搜索页码
        * @apiParam {int} pageSize 每页显示数量
        *
        * @apiSuccessExample  正确输出：
        * Content:{
        *    Count:int   总记录数
        *    Data:[
        *        {
        *            "Mobile": string            手机号
        *            "IndustryID": long          行业ID
        *            "IndustryName": string      行业名称
        *            "LocationPlace": string     所在位置
        *            "Street": string            所在街道
        *            "Latitude": double          纬度
        *            "Longitude": double         经度
        *            "LinkMan": string           联系人
        *            "Phone": string             联系电话
        *            "CertificateStatus": int    认证类型（以2的N次方和表示）
        *            "BusinessBeginTime": string 开始营业时间（如：09:00）
        *            "BusinessEndTime": string   结束营业时间（如：22:00）
        *            "ID": long                  商家ID,
        *            "DataType": 3               数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）
        *            "AreaID": int               商家所属区域（一般为具体区县ID）
        *            "AreaLayer": string         商家所属区域路径（如：440000，440300，440301 表示：广东省深圳市罗湖区）
        *            "Name": string              商家名称
        *            "Pic": string               商家图片路径（仅一张，不含文件服务器域名，如：enterprise/.../1231.jpg）
        *            "CreateTime": datetime      创建时间      
        *            "UpdateTime": datetime      最后更新时间
        *            "Desc": string              描述（无实际意义，可忽略）
        *        }
        *    ]
        * }
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpGet("merchant")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Use)]
        public IActionResult MerchantSearch(int? areaID, string keyword, int pageIndex, int pageSize)
        {
            int count = 0;

            var list = SearchData<Merchant>(IndexDataType.Merchant, areaID, keyword, pageIndex, pageSize, out count);

            return Success(new
            {
                Count = count,
                Data = list
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 搜索招聘数据。
        * @apiSampleRequest /v1/search/job
        * @api {get} /v1/search/job 搜索招聘数据
        * @apiName JobSearch
        * @apiGroup Search
        * @apiPermission Use
        *
        * @apiParam {long} areaID 区域ID（可为null，为null时表示不限制区域）
        * @apiParam {string} keyword 关键词
        * @apiParam {int} pageIndex 当前搜索页码
        * @apiParam {int} pageSize 每页显示数量
        *
        * @apiSuccessExample  正确输出：
        * Content:{
        *    Count:int   总记录数
        *    Data:[
        *        {
        *            "MerchantID": long                  商家ID
        *            "MerchantName": string              商家名称
        *            "MerchantCertificateStatus": int    商家认证类型（以2的N次方和表示）
        *            "CategoryID": long                  岗位类型ID
        *            "Count": int                        招聘人数
        *            "MinMonthly": int                   最低提供月薪
        *            "MaxMonthly": int                   最高提供月薪
        *            "Sex": int                          性别（1 男，2 女）
        *            "MinAge": int                       最小年龄限制
        *            "MaxAge": int                       最大年龄限制
        *            "MinEducation": int                 最低学历要求（枚举）
        *            "MinJobYearsType": int              最低工作年限
        *            "JobType": int                      工作性质（1 全职，2 兼职）
        *            "Welfares": int                     福利（枚举，以2的N次方和表示）
        *            "WordAddress": string               工作地点
        *            "Latitude": double                  工作地纬度
        *            "Longitude": double                 工作地经度
        *            "ID": long                          招聘信息ID
        *            "DataType": 4                       数据类型（1 精品汇商品，2 商家商品，3 商家，4 招聘信息）
        *            "AreaID": int                       招聘所属区域（一般为具体区县ID）
        *            "AreaLayer": string                 招聘所属区域路径（如：440000,440300,440301 表示：广东省深圳市罗湖区）
        *            "Name": string                      岗位名称
        *            "Pic": string                       （空，可忽略）
        *            "CreateTime": datetime              发布时间
        *            "UpdateTime": datetime              最后更新时间
        *            "Desc": string                      描述（无实际意义，可忽略）
        *        }
        *    ]
        * }
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpGet("job")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Use)]
        public IActionResult JobSearch(int? areaID, string keyword, int pageIndex, int pageSize)
        {
            int count = 0;

            var list = SearchData<Job>(IndexDataType.Job, areaID, keyword, pageIndex, pageSize, out count);

            return Success(new
            {
                Count = count,
                Data = list
            });
        }

        /// <summary>
        /// 搜索指定类型的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="areaID"></param>
        /// <param name="keyword"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private List<T> SearchData<T>(IndexDataType dataType, int? areaID, string keyword, int pageIndex, int pageSize, out int count) where T : BaseIndexModel
        {
            string indexPath = string.Empty;

            switch (dataType)
            {
                case IndexDataType.Job: indexPath = IndexConfiguration.GetJobPath(); break;
                case IndexDataType.MallProduct: indexPath = IndexConfiguration.GetMallProductPath(); break;
                case IndexDataType.Merchant: indexPath = IndexConfiguration.GetMerchantPath(); break;
                case IndexDataType.MerchantProduct: indexPath = IndexConfiguration.GetMerchantProductPath(); break;
            }

            List<string> fields = new List<string> { "name", "desc" };

            BooleanQuery bquery = new BooleanQuery();

            //分词器
            Analyzer analyzer = new StandardAnalyzer(IndexConfiguration.LuceneMatchVersion);

            //搜索条件
            Query kwdQuery = MultiFieldQueryParser.Parse(IndexConfiguration.LuceneMatchVersion, keyword, fields.ToArray(), new Occur[] { Occur.SHOULD, Occur.SHOULD }, analyzer);
            bquery.Add(kwdQuery, Occur.MUST);

            if (areaID.HasValue && areaID.Value > 0)
            {
                Query query = new QueryParser(IndexConfiguration.LuceneMatchVersion, "areaid", analyzer).Parse(areaID.Value.ToString());

                bquery.Add(query, Occur.MUST);
            }

            Sort sort = new Sort(new SortField("updatetime", SortField.STRING, true));

            count = 0;

            var list = SearchHelper.Search<T>(indexPath, bquery, sort, pageIndex, pageSize, out count);

            return list;
        }
    }
}
