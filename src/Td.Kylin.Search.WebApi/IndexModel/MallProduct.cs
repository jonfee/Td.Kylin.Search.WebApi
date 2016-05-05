namespace Td.Kylin.Search.WebApi.IndexModel
{
    /// <summary>
    /// 精品汇商品
    /// </summary>
    public class MallProduct : BaseIndexModel
    {
        ///<summary>
        ///关联商品ID
        ///</summary>
        public long ProductID { get; set; }

        ///<summary>
        ///规格名称
        ///</summary>
        public string Specs { get; set; }

        ///<summary>
        ///SKU码
        ///</summary>
        public string SKU { get; set; }

        ///<summary>
        /// 所属类目
        ///</summary>
        public long CategoryID { get; set; }

        /// <summary>
        /// 所属类目名称
        /// </summary>
        public string CategoryName { get; set; }

        ///<summary>
        ///标签ID集合（多个时用英文逗号分隔）
        ///</summary>
        public string TagIDs { get; set; }

        /// <summary>
        /// 标签名称（多个时用英文逗号分隔）
        /// </summary>
        public string TagNames { get; set; }

        ///<summary>
		///市场价
		///</summary>
		public decimal MarketPrice { get; set; }

        ///<summary>
		///销售价
		///</summary>
		public decimal SalePrice { get; set; }
    }
}
