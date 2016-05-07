using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.DataCache;
using Td.Kylin.Search.WebApi.Enums;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.Data
{
    /// <summary>
    /// 精品汇（B2C）商品数据服务
    /// </summary>
    public class MallProductProvider
    {
        /// <summary>
        /// 获取所有商品ID及是否被删除集合
        /// </summary>
        /// <returns></returns>
        public static List<MallProduct> GetAllProductSKUList()
        {
            using (var db = new DataContext())
            {
                var query = from sku in db.Mall_ProductSKU
                            join p in db.Mall_Product
                            on sku.ProductID equals p.ProductID
                            where sku.IsDelete == false
                            select new MallProduct
                            {
                                ID = sku.SkuID,
                                AreaID = p.AreaID,
                                AreaLayer = null,
                                CategoryID = p.CategoryID,
                                CategoryName = null,
                                CreateTime = p.CreateTime,
                                DataType = IndexDataType.MallProduct,
                                MarketPrice = sku.MarketPrice,
                                Name = p.Name,
                                Pic = p.MainPic,
                                ProductID = sku.ProductID,
                                SalePrice = sku.SalePrice,
                                SKU = sku.SKU,
                                Specs = sku.Specs,
                                TagIDs = p.TagIDs,
                                TagNames = null,
                                UpdateTime = p.UpdateTime
                            };

                var list = query.ToList();

                var areaList = CacheCollection.SystemAreaCache.Value();

                var categoryList = CacheCollection.B2CProductCategoryCache.Value();

                var tagList = CacheCollection.B2CProductCategoryTagCache.Value();

                foreach (var item in list)
                {
                    //区域层级
                    var area = areaList.FirstOrDefault(p => p.AreaID == item.AreaID);
                    if (null != area)
                    {
                        item.AreaLayer = area.Layer;
                    }
                    //类目名称
                    var category = categoryList.FirstOrDefault(p => p.CategoryID == item.CategoryID);
                    if (null != category)
                    {
                        item.CategoryName = category.Name;
                    }
                    //标签名称
                    if (!string.IsNullOrWhiteSpace(item.TagIDs))
                    {
                        string[] ids = item.TagIDs.Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries);

                        var tagNames = (
                            from p in tagList
                            where ids.Contains(p.TagID.ToString())
                            select p.TagName
                            ).ToList();
                        item.TagNames = string.Join(",", tagNames);
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// 获取商品所属的区域ID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static int GetProductAreaID(long productID)
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Mall_Product
                            where p.ProductID == productID
                            select p.AreaID;

                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// 根据商品ID获取其下所有规格的商品集合
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<MallProduct> GetMallProductList(long productID)
        {
            using (var db = new DataContext())
            {
                var query = from sku in db.Mall_ProductSKU
                            join p in db.Mall_Product
                            on sku.ProductID equals p.ProductID
                            where sku.ProductID == productID
                            select new MallProduct
                            {
                                ID = sku.SkuID,
                                AreaID = p.AreaID,
                                AreaLayer = null,
                                CategoryID = p.CategoryID,
                                CategoryName = null,
                                CreateTime = p.CreateTime,
                                DataType = IndexDataType.MallProduct,
                                MarketPrice = sku.MarketPrice,
                                Name = p.Name,
                                Pic = p.MainPic,
                                ProductID = sku.ProductID,
                                SalePrice = sku.SalePrice,
                                SKU = sku.SKU,
                                Specs = sku.Specs,
                                TagIDs = p.TagIDs,
                                TagNames = null,
                                UpdateTime = p.UpdateTime
                            };

                var list = query.ToList();

                var areaList = CacheCollection.SystemAreaCache.Value();

                var categoryList = CacheCollection.B2CProductCategoryCache.Value();

                var tagList = CacheCollection.B2CProductCategoryTagCache.Value();

                foreach (var item in list)
                {
                    //区域层级
                    var area = areaList.FirstOrDefault(p => p.AreaID == item.AreaID);
                    if (null != area)
                    {
                        item.AreaLayer = area.Layer;
                    }
                    //类目名称
                    var category = categoryList.FirstOrDefault(p => p.CategoryID == item.CategoryID);
                    if (null != category)
                    {
                        item.CategoryName = category.Name;
                    }
                    //标签名称
                    if (!string.IsNullOrWhiteSpace(item.TagIDs))
                    {
                        string[] ids = item.TagIDs.Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries);

                        var tagNames = (
                            from p in tagList
                            where ids.Contains(p.TagID.ToString())
                            select p.TagName
                            ).ToList();
                        item.TagNames = string.Join(",", tagNames);
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// 根据商品ID获取其下所有规格的商品SKUID集合
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<long> GetMallProductSkuIds(long productID)
        {
            using (var db = new DataContext())
            {
                var query = from sku in db.Mall_ProductSKU
                            where sku.ProductID == productID
                            select sku.SkuID;

                return query.ToList();
            }
        }
    }
}
