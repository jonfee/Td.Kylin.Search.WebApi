using System;
using Td.Kylin.Search.WebApi.Enums;

namespace Td.Kylin.Search.WebApi.IndexModel
{
    /// <summary>
    /// 索引数据模型基类
    /// </summary>
    public abstract class BaseIndexModel
    {
        /// <summary>
        /// 数据ID（一般为主键ID）
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public IndexDataType DataType { get; set; }

        /// <summary>
        /// 所属区域ID
        /// </summary>
        public int AreaID { get; set; }

        /// <summary>
        /// 所在区域的路径（如：110000|110100|110102）
        /// </summary>
        public string AreaLayer { get; set; }

        /// <summary>
        /// 标题/名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 数据描述（多属性组合而成）
        /// </summary>
        public string Desc { get; set; }
    }
}
