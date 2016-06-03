using System;

namespace Td.Kylin.Searcher
{
    /// <summary>
    /// 搜索引擎请求结果
    /// </summary>
    public class SearcherResult
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return StatusCode == System.Net.HttpStatusCode.OK; }
        }

        /// <summary>
        /// Http状态代码值
        /// </summary>
        public System.Net.HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// 内容/结果
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 运行时长
        /// </summary>
        public TimeSpan Timespan { get; set; }
    }
}
