using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Td.Kylin.WebApi;
using Td.Kylin.WebApi.Filters;
using Td.Kylin.Search.WebApi.Data;
using Td.Kylin.Search.WebApi.Core;
using Td.Kylin.DataCache;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Td.Kylin.Search.WebApi.Controllers
{
    [Route("v1/job")]
    public class JobController : BaseController
    {
        [HttpPost("init")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin)]
        public async void Init()
        {
            await Task.Run(() =>
            {
                var list = JobProvider.GetAllJobIds();

                if (null != list)
                {
                    var cacheOpenAreas = CacheCollection.OpenAreaCache.Value();

                    var openAreas = cacheOpenAreas.Select(p => p.AreaID).ToList();

                    foreach (var item in list)
                    {
                        int areaID = AreaHelper.GetOpenAreaID(item.AreaLayer, cacheOpenAreas);

                        if (item.IsDelete)
                        {
                            Delete(areaID, item.JobID);
                        }
                        else
                        {
                            Modify(item.JobID);
                        }
                    }
                }
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中新增商家招聘。
        * @apiSampleRequest /v1/job/add
        * @api {Post} /v1/job/add 向索引库中新增商家招聘
        * @apiName Add
        * @apiGroup Job
        * @apiPermission Admin|Editor
        *
        * @apiParam {long} jobID 招聘ID
        *
        * @apiSuccessExample  正确输出：无
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpPost("add")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Add(long jobID)
        {
            await Task.Run(() =>
            {
                var item = JobProvider.GetJob(jobID);
                IndexManager.Instance.Insert(item);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中删除商家招聘。
        * @apiSampleRequest /v1/job/delete
        * @api {Post} /v1/job/delete 向索引库中删除商家招聘
        * @apiName Delete
        * @apiGroup Job
        * @apiPermission Admin|Editor
        *
        * @apiParam {int} areaID 招聘所属区域ID（为0或为null时表示由系统检测并处理）
        * @apiParam {long} jobID 招聘ID
        *
        * @apiSuccessExample  正确输出：无
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpPost("delete")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Delete(int? areaID, long jobID)
        {
            await Task.Run(() =>
            {
                if (!areaID.HasValue || areaID < 100000)
                {
                    var layer = JobProvider.GetJobAreaLayer(jobID);

                    var cacheOpenAreas = CacheCollection.OpenAreaCache.Value();

                    areaID = AreaHelper.GetOpenAreaID(layer, cacheOpenAreas);
                }

                IndexManager.Instance.Delete(Enums.IndexDataType.Job, areaID.Value, jobID);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中更新商家招聘。
        * @apiSampleRequest /v1/job/modify
        * @api {Post} /v1/job/modify 向索引库中更新商家招聘
        * @apiName Modify
        * @apiGroup Job
        * @apiPermission Admin|Editor
        *
        * @apiParam {long} jobID 招聘ID
        *
        * @apiSuccessExample  正确输出：无
        *
        * @apiErrorExample 错误输出: 无
        */
        [HttpPost("modify")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Modify(long jobID)
        {
            await Task.Run(() =>
            {
                var item = JobProvider.GetJob(jobID);
                IndexManager.Instance.Modify(item);
            });
        }
    }
}
