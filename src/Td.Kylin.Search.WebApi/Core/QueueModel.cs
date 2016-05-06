using Td.Kylin.Search.WebApi.Enums;

namespace Td.Kylin.Search.WebApi.Core
{
    /// <summary>
    /// 索引数据模型
    /// </summary>
    public class QueueModel
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public IndexDataType DataType { get; set; }

        /// <summary>
        /// 索引操作类型
        /// </summary>
        public ActionMode ActionMode { get; set; }

        /// <summary>
        /// 主键ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID { get; set; }

        /// <summary>
        /// 数据对象
        /// </summary>
        public object Data { get; set; } 
    }
}
