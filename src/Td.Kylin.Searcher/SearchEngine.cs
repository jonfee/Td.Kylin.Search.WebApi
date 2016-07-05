using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.AspNet.Utils;
using Td.Kylin.Search.WebApi.IndexModel;
using Td.Kylin.Searcher.IndexModel;

namespace Td.Kylin.Searcher
{
    /// <summary>
    /// 搜索引擎操作类
    /// </summary>
    public class SearchEngine
    {
        /// <summary>
        /// 初始化一个<seealso cref="SearchEngine"/>实例
        /// </summary>
        public SearchEngine()
        {
            if (string.IsNullOrWhiteSpace(ApiConfigRoot.PartnerID))
            {
                throw new ArgumentNullException(nameof(ApiConfigRoot.PartnerID));
            }

            if (string.IsNullOrWhiteSpace(ApiConfigRoot.Secret))
            {
                throw new ArgumentNullException(nameof(ApiConfigRoot.Secret));
            }
        }

        /// <summary>
        /// 初始化索引文件
        /// </summary>
        /// <param name="dataType"><seealso cref="IndexDataType"/>成员</param>
        public async Task<SearcherResult> Init(IndexDataType dataType)
        {
            var config = ApiConfigRoot.Configs.FirstOrDefault(p => p.DataType == dataType && p.ActionMode == ActionMode.Init);

            if (null == config || string.IsNullOrWhiteSpace(config.Url))
                return new SearcherResult
                {
                    Content = "接口配置信息无效",
                    Exception = null,
                    StatusCode = 0,
                    Timespan = TimeSpan.Zero
                };

            return await DoWork(null, config);
        }

        /// <summary>
        /// 根据数据类型及ID，添加数据索引
        /// </summary>
        /// <param name="dataType"><seealso cref="IndexDataType"/>成员</param>
        /// <param name="dataID"></param>
        public async Task<SearcherResult> Add(IndexDataType dataType, long dataID)
        {
            var config = ApiConfigRoot.Configs.FirstOrDefault(p => p.DataType == dataType && p.ActionMode == ActionMode.InsertByID);

            string parameterName = "id";

            switch (dataType)
            {
                case IndexDataType.Job: parameterName = "jobID"; break;
                case IndexDataType.MallProduct: parameterName = "productID"; break;
                case IndexDataType.Merchant: parameterName = "merchantID"; break;
                case IndexDataType.MerchantProduct: parameterName = "productID"; break;
            }

            Dictionary<string, string> dicParameter = new Dictionary<string, string>();
            dicParameter.Add(parameterName, dataID.ToString());

            return await DoWork(dicParameter, config);
        }

        /// <summary>
        /// 根据数据对象信息，添加数据索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<SearcherResult> Add<T>(T item) where T : BaseIndexModel
        {
            IndexDataType dataType = default(IndexDataType);

            Type type = item.GetType();

            if (type == typeof(Job))
            {
                dataType = IndexDataType.Job;
            }
            else if (type == typeof(Merchant))
            {
                dataType = IndexDataType.Merchant;
            }
            else if (type == typeof(MerchantProduct))
            {
                dataType = IndexDataType.MerchantProduct;
            }
            else if (type == typeof(MallProduct))
            {
                dataType = IndexDataType.MallProduct;
            }

            var config = ApiConfigRoot.Configs.FirstOrDefault(p => p.DataType == dataType && p.ActionMode == ActionMode.InsertByEntity);

            var dicParameter = DicMapper.ToMap(item);

            return await DoWork(dicParameter, config);
        }

        /// <summary>
        /// 根据数据类型及ID，编辑数据索引
        /// </summary>
        /// <param name="dataType"><seealso cref="IndexDataType"/>成员</param>
        /// <param name="dataID"></param>
        public async Task<SearcherResult> Modify(IndexDataType dataType, long dataID)
        {
            var config = ApiConfigRoot.Configs.FirstOrDefault(p => p.DataType == dataType && p.ActionMode == ActionMode.ModifyByID);

            string parameterName = "id";

            switch (dataType)
            {
                case IndexDataType.Job: parameterName = "jobID"; break;
                case IndexDataType.MallProduct: parameterName = "productID"; break;
                case IndexDataType.Merchant: parameterName = "merchantID"; break;
                case IndexDataType.MerchantProduct: parameterName = "productID"; break;
            }

            Dictionary<string, string> dicParameter = new Dictionary<string, string>();
            dicParameter.Add(parameterName, dataID.ToString());

            return await DoWork(dicParameter, config);
        }

        /// <summary>
        /// 根据数据对象信息，编辑数据索引
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<SearcherResult> Modify<T>(T item) where T : BaseIndexModel
        {
            IndexDataType dataType = default(IndexDataType);

            Type type = item.GetType();

            if (type == typeof(Job))
            {
                dataType = IndexDataType.Job;
            }
            else if (type == typeof(Merchant))
            {
                dataType = IndexDataType.Merchant;
            }
            else if (type == typeof(MerchantProduct))
            {
                dataType = IndexDataType.MerchantProduct;
            }
            else if (type == typeof(MallProduct))
            {
                dataType = IndexDataType.MallProduct;
            }

            var config = ApiConfigRoot.Configs.FirstOrDefault(p => p.DataType == dataType && p.ActionMode == ActionMode.ModifyByEntity);

            var dicParameter = DicMapper.ToMap(item);

            return await DoWork(dicParameter, config);
        }

        /// <summary>
        /// 根据数据类型及ID，删除数据索引
        /// </summary>
        /// <param name="dataType"><seealso cref="IndexDataType"/>成员</param>
        /// <param name="dataID">数据ID</param>
        /// <returns></returns>
        public async Task<SearcherResult> Delete(IndexDataType dataType, long dataID)
        {
            var config = ApiConfigRoot.Configs.FirstOrDefault(p => p.DataType == dataType && p.ActionMode == ActionMode.Delete);

            string parameterName = "id";

            switch (dataType)
            {
                case IndexDataType.Job: parameterName = "jobID"; break;
                case IndexDataType.MallProduct: parameterName = "productID"; break;
                case IndexDataType.Merchant: parameterName = "merchantID"; break;
                case IndexDataType.MerchantProduct: parameterName = "productID"; break;
            }

            Dictionary<string, string> dicParameter = new Dictionary<string, string>();
            dicParameter.Add(parameterName, dataID.ToString());

            return await DoWork(dicParameter, config);
        }

        /// <summary>
        /// 执行请求
        /// </summary>
        /// <param name="parameters">参数集</param>
        /// <param name="config"><seealso cref="ApiConfig"/>Api配置</param>
        /// <returns></returns>
        private async Task<SearcherResult> DoWork(Dictionary<string, string> parameters, ApiConfig config)
        {
            return await DoWork(0, 0, 0, parameters, config);
        }

        /// <summary>
        /// 执行请求
        /// </summary>
        /// <param name="areaID">区域ID</param>
        /// <param name="lbsLatitude">位置纬度</param>
        /// <param name="lbsLongitude">位置经度</param>
        /// <param name="parameters">参数集</param>
        /// <param name="config"><seealso cref="ApiConfig"/>Api配置</param>
        /// <returns></returns>
        private async Task<SearcherResult> DoWork(int areaID, double lbsLongitude, double lbsLatitude, Dictionary<string, string> parameters, ApiConfig config)
        {
            return await Task.Run(() =>
            {
                if (null == config || string.IsNullOrWhiteSpace(config.Url))
                    return new SearcherResult
                    {
                        Content = "接口配置信息无效",
                        Exception = null,
                        StatusCode = 0,
                        Timespan = TimeSpan.Zero
                    };

                using (var client = new SearcherHttpContext())
                {
                    if (config.RequestMode == Enums.RequestMode.Post)
                    {
                        return client.DoPost(areaID, lbsLongitude, lbsLatitude, config.Url, parameters, ApiConfigRoot.PartnerID, ApiConfigRoot.Secret);
                    }
                    else
                    {
                        return client.DoGet(areaID, lbsLongitude, lbsLatitude, config.Url, parameters, ApiConfigRoot.PartnerID, ApiConfigRoot.Secret);
                    }
                }
            });
        }
    }
}
