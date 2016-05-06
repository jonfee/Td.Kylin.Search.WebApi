using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using Td.Kylin.DataCache;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.Data;
using Td.Kylin.WebApi;
using Td.Kylin.WebApi.Filters;
using System.Linq;

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
    }
}
