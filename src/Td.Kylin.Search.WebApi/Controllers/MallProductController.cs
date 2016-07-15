using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.Search.WebApi.Data;
using Td.Kylin.Search.WebApi.IndexModel;
using Td.Kylin.Search.WebApi.WriterManager;
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
                var list = MallProductProvider.GetAllProductSKUList();

                if (null != list)
                {
                    foreach (var item in list)
                    {
                        Update(item);
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
        */
        [HttpPost("add")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Add(long productID)
        {
            await Task.Run(() =>
            {
                var list = MallProductProvider.GetMallProductList(productID);

                AreaIndexManager.Instance.Insert(list);
                MallProductIndexManager.Instance.Insert(list);
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

                AreaIndexManager.Instance.Delete(Enums.IndexDataType.MallProduct, areaID.Value, list);
                MallProductIndexManager.Instance.Delete(Enums.IndexDataType.MallProduct, areaID.Value, list);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中删除精品汇（B2C）商品规格。
        * @apiSampleRequest /v1/mallproduct/deletesku
        * @api {Post} /v1/mallproduct/deletesku 向索引库中删除精品汇（B2C）商品规格
        * @apiName DeleteSku
        * @apiGroup MallProduct
        * @apiPermission Admin|Editor
        *
        * @apiParam {int} areaID 商品所属区域ID（为0或为null时表示由系统检测并处理）
        * @apiParam {long} skuID 商品skuID
        *
        */
        [HttpPost("deletesku")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void DeleteSku(int? areaID, long skuID)
        {
            await Task.Run(() =>
            {
                if (!areaID.HasValue)
                {
                    areaID = MallProductProvider.GetSkuAreaID(skuID);
                }

                AreaIndexManager.Instance.Delete(Enums.IndexDataType.MallProduct, areaID.Value, skuID);
                MallProductIndexManager.Instance.Delete(Enums.IndexDataType.MallProduct, areaID.Value, skuID);
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
        */
        [HttpPost("modify")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Modify(long productID)
        {
            await Task.Run(() =>
            {
                var list = MallProductProvider.GetMallProductList(productID);

                AreaIndexManager.Instance.Modify(list);
                MallProductIndexManager.Instance.Modify(list);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中更新精品汇（B2C）商品。
        * @apiSampleRequest /v1/mallproduct/update
        * @api {Post} /v1/mallproduct/update 向索引库中更新精品汇（B2C）商品
        * @apiName Update
        * @apiGroup MallProduct
        * @apiPermission Admin|Editor
        *
        * @apiParamExample {object} item 精品汇（B2C）商品信息
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
        *             "AreaID": int              所属区域ID
        *             "AreaLayer": string        所属区域路径（如：440000,440300 表示：广东省深圳市）
        *             "Name": string             商品名称
        *             "Pic": string              商品图片（仅一张，不含文件服务器域名，如：mall/product/.../121.jpg）
        *             "CreateTime": datetime     发布时间
        *         }
        *
        */
        [HttpPost("update")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Update(MallProduct item)
        {
            await Task.Run(() =>
            {
                item.DataType = Enums.IndexDataType.MallProduct;
                item.Pic = (item.Pic ?? string.Empty).Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                item.Desc = string.Empty;
                item.UpdateTime = DateTime.Now;

                AreaIndexManager.Instance.Modify(item);
                MallProductIndexManager.Instance.Modify(item);
            });
        }
    }
}
