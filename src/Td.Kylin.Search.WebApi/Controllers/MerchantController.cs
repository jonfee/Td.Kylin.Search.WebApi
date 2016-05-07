using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using Td.Kylin.DataCache;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.Data;
using Td.Kylin.WebApi;
using Td.Kylin.WebApi.Filters;
using System.Linq;
using Td.Kylin.Search.WebApi.IndexModel;
using System;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Td.Kylin.Search.WebApi.Controllers
{
    /// <summary>
    /// 商家索引库接口
    /// </summary>
    [Route("v1/merchant")]
    public class MerchantController : BaseController
    {
        [HttpPost("init")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin)]
        public async void Init()
        {
            await Task.Run(() =>
            {
                var Ids = MerchantProvider.GetAllMerchantIds();

                if (null != Ids)
                {
                    foreach (var id in Ids)
                    {
                        Modify(id);
                    }
                }
            });
        }

        /**
       * @apiVersion 1.0.0
       * @apiDescription 向索引库中新增商家。
       * @apiSampleRequest /v1/merchant/add
       * @api {Post} /v1/merchant/add 向索引库中新增商家
       * @apiName Add
       * @apiGroup Merchant
       * @apiPermission Admin|Editor
       *
       * @apiParam {long} merchantID 商家ID
       *
       * @apiSuccessExample  正确输出：无
       *
       * @apiErrorExample 错误输出: 无
       */
        [HttpPost("add")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Add(long merchantID)
        {
            await Task.Run(() =>
            {
                var item = MerchantProvider.GetMerchant(merchantID);
                IndexManager.Instance.Insert(item);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中新增商家。
        * @apiSampleRequest /v1/merchant/insert
        * @api {Post} /v1/merchant/insert 向索引库中新增商家
        * @apiName Insert
        * @apiGroup Merchant
        * @apiPermission Admin|Editor
        *
        * @apiParamExample {object} item 商家信息
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
        *            "AreaID": int               商家所属区域（一般为具体区县ID）
        *            "AreaLayer": string         商家所属区域路径（如：440000，440300，440301 表示：广东省深圳市罗湖区）
        *            "Name": string              商家名称
        *            "Pic": string               商家图片路径（仅一张，不含文件服务器域名，如：enterprise/.../1231.jpg）
        *            "CreateTime": datetime      创建时间
        *        }
        *
        * @apiSuccessExample  正确输出：无
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpPost("insert")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Insert(Merchant item)
        {
            await Task.Run(() =>
            {
                if (null != item)
                {
                    item.DataType = Enums.IndexDataType.Merchant;
                    item.Pic = (item.Pic ?? string.Empty).Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                    item.Desc = string.Empty;
                    item.UpdateTime = item.CreateTime;

                    IndexManager.Instance.Insert(item);
                }                
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中删除商家。
        * @apiSampleRequest /v1/merchant/delete
        * @api {Post} /v1/merchant/delete 向索引库中删除商家
        * @apiName Delete
        * @apiGroup Merchant
        * @apiPermission Admin|Editor
        *
        * @apiParam {int} areaID 商家所属区域ID（为0或为null时表示由系统检测并处理）
        * @apiParam {long} merchantID 商家ID
        *
        * @apiSuccessExample  正确输出：无
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpPost("delete")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Delete(int? areaID, long merchantID)
        {
            await Task.Run(() =>
            {
                if (!areaID.HasValue || areaID < 100000)
                {
                    var layer = MerchantProvider.GetAreaLayer(merchantID);

                    var cacheOpenAreas = CacheCollection.OpenAreaCache.Value();

                    areaID = AreaHelper.GetOpenAreaID(layer, cacheOpenAreas);
                }

                IndexManager.Instance.Delete(Enums.IndexDataType.Merchant, areaID.Value, merchantID);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中更新商家。
        * @apiSampleRequest /v1/merchant/modify
        * @api {Post} /v1/merchant/modify 向索引库中更新商家
        * @apiName Modify
        * @apiGroup Merchant
        * @apiPermission Admin|Editor
        *
        * @apiParam {long} productID 商品ID
        *
        * @apiSuccessExample  正确输出：无
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpPost("modify")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Modify(long merchantID)
        {
            await Task.Run(() =>
            {
                var item = MerchantProvider.GetMerchant(merchantID);
                IndexManager.Instance.Modify(item);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中更新商家。
        * @apiSampleRequest /v1/merchant/update
        * @api {Post} /v1/merchant/update 向索引库中更新商家
        * @apiName Update
        * @apiGroup Merchant
        * @apiPermission Admin|Editor
        *
        * @apiParamExample {object} item 商家信息
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
        *            "AreaID": int               商家所属区域（一般为具体区县ID）
        *            "AreaLayer": string         商家所属区域路径（如：440000，440300，440301 表示：广东省深圳市罗湖区）
        *            "Name": string              商家名称
        *            "Pic": string               商家图片路径（仅一张，不含文件服务器域名，如：enterprise/.../1231.jpg）
        *            "CreateTime": datetime      创建时间
        *        }
        *
        * @apiSuccessExample  正确输出：无
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpPost("update")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Update(Merchant item)
        {
            await Task.Run(() =>
            {
                if (null != item)
                {
                    item.DataType = Enums.IndexDataType.Merchant;
                    item.Pic = (item.Pic ?? string.Empty).Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                    item.Desc = string.Empty;
                    item.UpdateTime = DateTime.Now;

                    IndexManager.Instance.Modify(item);
                }
            });
        }
    }
}
