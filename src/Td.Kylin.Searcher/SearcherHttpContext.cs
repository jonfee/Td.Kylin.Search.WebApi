using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Td.AspNet.Utils;

namespace Td.Kylin.Searcher
{
    /// <summary>
    /// 搜索Http请求上下文
    /// </summary>
    internal class SearcherHttpContext : IDisposable
    {
        private HttpClient httpClient;

        public SearcherHttpContext()
        {
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
        }

        public void Dispose()
        {
            httpClient.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// HttpGet
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="lbsLongitude"></param>
        /// <param name="lbsLatitude"></param>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="partnerId"></param>
        /// <param name="secretKey"></param>
        public SearcherResult DoGet(int areaID,double lbsLongitude,double lbsLatitude, string url, Dictionary<string, string> parameters, string partnerId, string secretKey)
        {
            SearcherResult result = new SearcherResult();

            IDictionary<string, string> txtParams;
            if (parameters != null)
            {
                txtParams = new Dictionary<string, string>(parameters);
            }
            else
            {
                txtParams = new Dictionary<string, string>();
            }

            txtParams.Add("PartnerId", partnerId);
            txtParams.Add("Timestamp", DateTime.Now.ToUniversalTime().Ticks.ToString());
            txtParams.Add("Sign", Strings.SignRequest(txtParams, secretKey));
            txtParams.Add("LBSArea", areaID.ToString());
            txtParams.Add("LBSLongitude", lbsLongitude.ToString());
            txtParams.Add("LBSLatitude", lbsLatitude.ToString());

            url = Strings.BuildGetUrl(url, txtParams);

            Stopwatch timer = new Stopwatch();
            timer.Start();

            try
            {

                //await异步等待回应
                var task = httpClient.GetAsync(url);
                HttpResponseMessage response = task.Result;
                var data = response.Content.ReadAsStringAsync().Result;

                result = new SearcherResult
                {
                    Content = data,
                    StatusCode = response.StatusCode,
                    Timespan = timer.Elapsed
                };
            }
            catch (HttpRequestException hre)
            {
                result.Exception = hre;
                result.Content = hre.StackTrace;
                result.StatusCode = 0;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.Content = ex.StackTrace;
                result.StatusCode = 0;
            }
            finally
            {
                timer.Stop();
                result.Timespan = timer.Elapsed;
            }

            return result;
        }

        /// <summary>
        /// HttpPost
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="lbsLongitude"></param>
        /// <param name="lbsLatitude"></param>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="partnerid"></param>
        /// <param name="secretKey"></param>
        public SearcherResult DoPost(int areaID, double lbsLongitude, double lbsLatitude, string url, Dictionary<string, string> parameters, string partnerid, string secretKey)
        {
            SearcherResult result = new SearcherResult();

            IDictionary<string, string> txtParams;
            if (parameters != null)
            {
                txtParams = new Dictionary<string, string>(parameters);
            }
            else
            {
                txtParams = new Dictionary<string, string>();
            }

            IDictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams.Add("PartnerId", partnerid);
            urlParams.Add("Timestamp", DateTime.Now.ToUniversalTime().Ticks.ToString());
            urlParams.Add("LBSArea", areaID.ToString());
            urlParams.Add("LBSLongitude", lbsLongitude.ToString());
            urlParams.Add("LBSLatitude", lbsLatitude.ToString());

            foreach (var item in urlParams)
            {
                txtParams.Add(item.Key, item.Value);
            }

            var sign = Strings.SignRequest(txtParams, secretKey);
            urlParams.Add("Sign", sign);

            url = Strings.BuildGetUrl(url, urlParams);
            HttpContent content = new FormUrlEncodedContent(parameters);

            Stopwatch timer = new Stopwatch();
            timer.Start();

            try
            {

                //await异步等待回应
                var task = httpClient.PostAsync(url, content);
                HttpResponseMessage response = task.Result;
                var data = response.Content.ReadAsStringAsync().Result;

                result = new SearcherResult
                {
                    Content = data,
                    StatusCode = response.StatusCode
                };
            }
            catch (HttpRequestException hre)
            {
                result.Content = hre.StackTrace;
                result.StatusCode = 0;
            }
            catch (Exception ex)
            {
                result.Content = ex.StackTrace;
                result.StatusCode = 0;
            }
            finally
            {
                timer.Stop();
                result.Timespan = timer.Elapsed;
            }

            return result;
        }
    }
}
