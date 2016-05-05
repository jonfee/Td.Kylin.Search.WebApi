using System;

namespace Td.Kylin.Search.WebApi.IndexModel
{
    /// <summary>
    /// 商家商品
    /// </summary>
    public class MerchantProduct : BaseIndexModel
    {
        /// <summary>
        /// 商家ID
        /// </summary>
        public long MerchantID { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// 所属系统商品分类（MerchantGoods_SystemCategory.CategoryID）
        /// </summary>
        public long SystemCategoryID { get; set; }

        /// <summary>
        /// 所属系统分类名称
        /// </summary>
        public string SystemCategoryName { get; set; }

        /// <summary>
        /// 原价/市场价
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 现价/销售价
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        ///  商品规格
        /// </summary>
        public string Specification { get; set; }

        ///<summary>
        ///纬度
        ///</summary>
        public Single Latitude { get; set; }

        ///<summary>
        ///经度
        ///</summary>
        public Single Longitude { get; set; }
    }
}
