using System;
using Td.Kylin.Searcher.Enums;

namespace Td.Kylin.Searcher
{
    /// <summary>
    /// 接口配置
    /// </summary>
    public class ApiConfig
    {
        /// <summary>
        /// 操作的接口类型（选项：mallproduct|merchantproduct|merchant|job）
        /// </summary>
        public string DataTypeName { get; set; }

        /// <summary>
        /// 操作方式（选项：init|insert|modify|delete）
        /// </summary>
        public string ActionModeName { get; set; }

        /// <summary>
        /// 索引数据类型
        /// </summary>
        public IndexDataType DataType
        {
            get
            {
                return (IndexDataType)Enum.Parse(typeof(IndexDataType), DataTypeName);
            }
        }

        /// <summary>
        /// 索引操作方式
        /// </summary>
        public ActionMode ActionMode
        {
            get
            {
                return (ActionMode)Enum.Parse(typeof(ActionMode), ActionModeName);
            }
        }

        /// <summary>
        /// API地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求方式（post或get）
        /// </summary>
        public RequestMode RequestMode { get; set; }
    }
}
