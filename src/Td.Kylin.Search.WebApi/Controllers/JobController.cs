﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Td.Kylin.Search.WebApi.Data;
using Td.Kylin.Search.WebApi.IndexModel;
using Td.Kylin.Search.WebApi.WriterManager;
using Td.Kylin.WebApi;
using Td.Kylin.WebApi.Filters;

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
                var list = JobProvider.GetAllJobList();

                if (null != list)
                {
                    //var cacheOpenAreas = CacheCollection.OpenAreaCache.Value();

                    //var openAreas = cacheOpenAreas.Select(p => p.AreaID).ToList();

                    foreach (var item in list)
                    {
                        Update(item);
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
        *
        */
        [HttpPost("add")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Add(long jobID)
        {
            await Task.Run(() =>
            {
                var item = JobProvider.GetJob(jobID);

                AreaIndexManager.Instance.Insert(item);
                JobIndexManager.Instance.Insert(item);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中新增商家招聘。
        * @apiSampleRequest /v1/job/insert
        * @api {Post} /v1/job/insert 向索引库中新增商家招聘
        * @apiName Insert
        * @apiGroup Job
        * @apiPermission Admin|Editor
        *
        * @apiParamExample {object} item 招聘信息
        *        {
        *            "MerchantID": long                  商家ID
        *            "MerchantName": string              商家名称
        *            "MerchantCertificateStatus": int    商家认证类型（以2的N次方和表示）
        *            "CategoryID": long                  岗位类型ID
        *            "Count": int                        招聘人数
        *            "MinMonthly": int                   最低提供月薪
        *            "MaxMonthly": int                   最高提供月薪
        *            "Sex": int                          性别（1 男，2 女）
        *            "MinAge": int                       最小年龄限制
        *            "MaxAge": int                       最大年龄限制
        *            "MinEducation": int                 最低学历要求（枚举）
        *            "MinJobYearsType": int              最低工作年限
        *            "JobType": int                      工作性质（1 全职，2 兼职）
        *            "Welfares": int                     福利（枚举，以2的N次方和表示）
        *            "WordAddress": string               工作地点
        *            "Latitude": double                  工作地纬度
        *            "Longitude": double                 工作地经度
        *            "ID": long                          招聘信息ID
        *            "AreaID": int                       招聘所属区域（一般为具体区县ID）
        *            "AreaLayer": string                 招聘所属区域路径（如：440000,440300,440301 表示：广东省深圳市罗湖区）
        *            "Name": string                      岗位名称
        *            "CreateTime": datetime              发布时间
        *        }
        *
        */
        [HttpPost("insert")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Insert(Job item)
        {
            await Task.Run(() =>
            {
                if (null != item)
                {
                    item.DataType = Enums.IndexDataType.Job;
                    item.Pic = string.Empty;
                    item.Desc = string.Empty;
                    item.UpdateTime = item.CreateTime;

                    var area = item.AreaID.ToString();
                    if (area.Length == 6)
                    {
                        item.AreaID = int.Parse(area.Remove(4) + "00");
                    }

                    AreaIndexManager.Instance.Insert(item);
                    JobIndexManager.Instance.Insert(item);
                }
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
        */
        [HttpPost("delete")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Delete(int? areaID, long jobID)
        {
            await Task.Run(() =>
            {
                int tempArea = 0;

                if (!areaID.HasValue)
                {
                    var _area = areaID.ToString();
                    if (_area.Length == 6)
                    {
                        tempArea = int.Parse(_area.Remove(4) + "00");
                    }
                }

                if (tempArea == 0)
                {
                    var job = JobProvider.GetJob(jobID);

                    if (job == null) return;

                    tempArea = job.AreaID;
                }

                AreaIndexManager.Instance.Delete(Enums.IndexDataType.Job, tempArea, jobID);
                JobIndexManager.Instance.Delete(Enums.IndexDataType.Job, tempArea, jobID);
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
        */
        [HttpPost("modify")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Modify(long jobID)
        {
            await Task.Run(() =>
            {
                var item = JobProvider.GetJob(jobID);

                AreaIndexManager.Instance.Modify(item);
                JobIndexManager.Instance.Modify(item);
            });
        }

        /**
        * @apiVersion 1.0.0
        * @apiDescription 向索引库中更新商家招聘。
        * @apiSampleRequest /v1/job/update
        * @api {Post} /v1/job/update 向索引库中更新商家招聘
        * @apiName Update
        * @apiGroup Job
        * @apiPermission Admin|Editor
        *
        * @apiParamExample {object} item 招聘信息
        *        {
        *            "MerchantID": long                  商家ID
        *            "MerchantName": string              商家名称
        *            "MerchantCertificateStatus": int    商家认证类型（以2的N次方和表示）
        *            "CategoryID": long                  岗位类型ID
        *            "Count": int                        招聘人数
        *            "MinMonthly": int                   最低提供月薪
        *            "MaxMonthly": int                   最高提供月薪
        *            "Sex": int                          性别（1 男，2 女）
        *            "MinAge": int                       最小年龄限制
        *            "MaxAge": int                       最大年龄限制
        *            "MinEducation": int                 最低学历要求（枚举）
        *            "MinJobYearsType": int              最低工作年限
        *            "JobType": int                      工作性质（1 全职，2 兼职）
        *            "Welfares": int                     福利（枚举，以2的N次方和表示）
        *            "WordAddress": string               工作地点
        *            "Latitude": double                  工作地纬度
        *            "Longitude": double                 工作地经度
        *            "ID": long                          招聘信息ID
        *            "AreaID": int                       招聘所属区域（一般为具体区县ID）
        *            "AreaLayer": string                 招聘所属区域路径（如：440000,440300,440301 表示：广东省深圳市罗湖区）
        *            "Name": string                      岗位名称
        *            "CreateTime": datetime              发布时间
        *        }
        *
        */
        [HttpPost("update")]
        [ApiAuthorization(Code = Kylin.WebApi.Models.Role.Admin | Kylin.WebApi.Models.Role.Editor)]
        public async void Update(Job item)
        {
            await Task.Run(() =>
            {
                if (null != item)
                {
                    item.DataType = Enums.IndexDataType.Job;
                    item.Pic = string.Empty;
                    item.Desc = string.Empty;
                    item.UpdateTime = DateTime.Now;

                    var area = item.AreaID.ToString();
                    if (area.Length == 6)
                    {
                        item.AreaID = int.Parse(area.Remove(4) + "00");
                    }

                    AreaIndexManager.Instance.Modify(item);
                    JobIndexManager.Instance.Modify(item);
                }
            });
        }
    }
}
