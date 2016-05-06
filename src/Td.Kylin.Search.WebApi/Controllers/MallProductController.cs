using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.Search.WebApi.Data;
using Td.Kylin.Search.WebApi.IndexModel;
using Td.Kylin.WebApi;
using Td.Kylin.WebApi.Filters;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Td.Kylin.Search.WebApi.Controllers
{
    /// <summary>
    /// 精品汇（B2C）商品索引库接口
    /// </summary>
    [Route("v1/mallproduct")]
    public class MallProductController : BaseController
    {
        [HttpPost("init")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin)]
        public async void Init()
        {
            await Task.Run(() =>
            {
                var list = MallProductProvider.GetAllProductIds();

                if (null != list)
                {
                    foreach (var item in list)
                    {
                        if (item.IsDelete)
                        {
                            Delete(item.AreaID, item.ProductID);
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
        * @apiDescription 向索引库中新增精品汇（B2C）商品。
        * @apiSampleRequest /v1/mallproduct/add
        * @api {Post} /v1/mallproduct/add 向索引库中新增精品汇（B2C）商品
        * @apiName Add
        * @apiGroup MallProduct
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
                var list = MallProductProvider.GetMallProductList(productID);
                IndexManager.Instance.Insert<MallProduct>(list);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中删除精品汇（B2C）商品。
        * @apiSampleRequest /v1/mallproduct/delete
        * @api {Post} /v1/mallproduct/delete 向索引库中删除精品汇（B2C）商品
        * @apiName Delete
        * @apiGroup MallProduct
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
                if (!areaID.HasValue)
                {
                    areaID = MallProductProvider.GetProductAreaID(productID);
                }
                var list = MallProductProvider.GetMallProductSkuIds(productID);
                IndexManager.Instance.Delete(Enums.IndexDataType.MallProduct, areaID.Value, list);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中更新精品汇（B2C）商品。
        * @apiSampleRequest /v1/mallproduct/modify
        * @api {Post} /v1/mallproduct/modify 向索引库中更新精品汇（B2C）商品
        * @apiName Modify
        * @apiGroup MallProduct
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
                var list = MallProductProvider.GetMallProductList(productID);
                IndexManager.Instance.Modify<MallProduct>(list);
            });
        }
    }
}
