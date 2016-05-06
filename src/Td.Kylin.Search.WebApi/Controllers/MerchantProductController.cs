using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.Data;
using Td.Kylin.WebApi;
using Td.Kylin.WebApi.Filters;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Td.Kylin.Search.WebApi.Controllers
{
    /// <summary>
    /// 附近购（商家）商品索引库接口
    /// </summary>
    [Route("v1/merchantproduct")]
    public class MerchantProductController : BaseController
    {
        [HttpPost("init")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin)]
        public async void Init()
        {
            await Task.Run(() =>
            {
                var list = MerchantProductProvider.GetAllProductIds();

                if (null != list)
                {
                    var cacheOpenAreas = CacheCollection.OpenAreaCache.Value();
                    
                    foreach (var item in list)
                    {
                        int areaID = AreaHelper.GetOpenAreaID(item.AreaLayer, cacheOpenAreas);

                        if (item.IsDelete)
                        {
                            Delete(areaID, item.ProductID);
                        }
                        else
                        {
                            Modify(item.ProductID);
                        }
                    }
                }
            }); 
        }
         
        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中新增附近购（商家）商品。
        * @apiSampleRequest /v1/merchantproduct/add
        * @api {Post} /v1/merchantproduct/add 向索引库中新增附近购（商家）商品
        * @apiName Add
        * @apiGroup MerchantProduct
        * @apiPermission Admin|Editor
        *
        * @apiParam {long} productID 商品ID
        *
        * @apiSuccessExample  正确输出：无
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpPost("add")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Add(long productID)
        {
            await Task.Run(() =>
            {
                var item = MerchantProductProvider.GetMerchantProduct(productID);
                IndexManager.Instance.Insert(item);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中删除附近购（商家）商品。
        * @apiSampleRequest /v1/merchantproduct/delete
        * @api {Post} /v1/merchantproduct/delete 向索引库中删除附近购（商家）商品
        * @apiName Delete
        * @apiGroup MerchantProduct
        * @apiPermission Admin|Editor
        *
        * @apiParam {int} areaID 商品所属区域ID（为0或为null时表示由系统检测并处理）
        * @apiParam {long} productID 商品ID
        *
        * @apiSuccessExample  正确输出：无
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpPost("delete")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Delete(int? areaID, long productID)
        {
            await Task.Run(() =>
            {
                if (!areaID.HasValue || areaID < 100000)
                {
                    var layer = MerchantProductProvider.GetProductAreaLayer(productID);

                    var cacheOpenAreas = CacheCollection.OpenAreaCache.Value();

                    areaID= AreaHelper.GetOpenAreaID(layer, cacheOpenAreas);
                }

                IndexManager.Instance.Delete(Enums.IndexDataType.MerchantProduct, areaID.Value, productID);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中更新附近购（商家）商品。
        * @apiSampleRequest /v1/merchantproduct/modify
        * @api {Post} /v1/merchantproduct/modify 向索引库中更新附近购（商家）商品
        * @apiName Modify
        * @apiGroup MerchantProduct
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
        public async void Modify(long productID)
        {
            await Task.Run(() =>
            {
                var item = MerchantProductProvider.GetMerchantProduct(productID);
                IndexManager.Instance.Modify(item);
            });
        }
    }
}
