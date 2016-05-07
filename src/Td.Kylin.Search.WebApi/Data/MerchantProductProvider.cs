using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache;
using Td.Kylin.Search.WebApi.Enums;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.Data
{
    /// <summary>
    /// 附近购（商家）商品数据服务
    /// </summary>
    public class MerchantProductProvider
    {
        /// <summary>
        /// 获取所有未删除的商品集合
        /// </summary>
        /// <returns></returns>
        public static List<MerchantProduct> GetAllProductList()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.MerchGoods_Goods
                            join m in db.Merchant_Account
                            on p.MerchantID equals m.MerchantID
                            where p.IsDelete==false
                            select new MerchantProduct
                            {
                                ID = p.GoodsID,
                                AreaID = p.AreaID,
                                AreaLayer = p.AreaLayer,
                                CreateTime = p.CreateTime,
                                DataType = IndexDataType.MerchantProduct,
                                Name = p.Name,
                                Pic = p.Pic,
                                Latitude = m.Latitude,
                                Longitude = m.Longitude,
                                MerchantID = p.MerchantID,
                                MerchantName = m.Name,
                                OriginalPrice = p.OriginalPrice,
                                SalePrice = p.SalePrice,
                                Specification = p.Specification,
                                SystemCategoryID = p.SystemCategoryID,
                                SystemCategoryName = null,
                                UpdateTime = DateTime.Now
                            };

                var list = query.ToList();

                var categoryList = CacheCollection.MerchantProductSystemCategoryCache.Value();

                list.ForEach((item)=>{
                    if (null != item)
                    {
                        //类目名称
                        var category = categoryList.FirstOrDefault(p => p.CategoryID == item.SystemCategoryID);
                        if (null != category)
                        {
                            item.SystemCategoryName = category.Name;
                        }
                        //图片
                        var pic = (item.Pic ?? string.Empty).Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                        item.Pic = pic;
                    }
                });

                return list;
            }
        }

        /// <summary>
        /// 获取商品所属的区域层级
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static string GetProductAreaLayer(long productID)
        {
            using (var db = new DataContext())
            {
                var query = from p in db.MerchGoods_Goods
                            where p.GoodsID == productID
                            select p.AreaLayer;

                return query.FirstOrDefault();
            }
        }

        /// <summary>
        /// 根据商品ID获取商品
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static MerchantProduct GetMerchantProduct(long productID)
        {
            using (var db = new DataContext())
            {
                var query = from p in db.MerchGoods_Goods
                            join m in db.Merchant_Account
                            on p.MerchantID equals m.MerchantID
                            where p.GoodsID == productID
                            select new MerchantProduct
                            {
                                ID = p.GoodsID,
                                AreaID = p.AreaID,
                                AreaLayer = p.AreaLayer,
                                CreateTime = p.CreateTime,
                                DataType = IndexDataType.MerchantProduct,
                                Name = p.Name,
                                Pic = p.Pic,
                                Latitude = m.Latitude,
                                Longitude = m.Longitude,
                                MerchantID = p.MerchantID,
                                MerchantName = m.Name,
                                OriginalPrice = p.OriginalPrice,
                                SalePrice = p.SalePrice,
                                Specification = p.Specification,
                                SystemCategoryID = p.SystemCategoryID,
                                SystemCategoryName = null,
                                UpdateTime = DateTime.Now
                            };

                var item = query.FirstOrDefault();
                
                if (null != item)
                {
                    //类目名称
                    var category = CacheCollection.MerchantProductSystemCategoryCache.Get(item.SystemCategoryID);
                    if (null != category)
                    {
                        item.SystemCategoryName = category.Name;
                    }
                    //图片
                    var pic = (item.Pic ?? string.Empty).Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                    item.Pic = pic;
                }

                return item;
            }
        }
    }
}
