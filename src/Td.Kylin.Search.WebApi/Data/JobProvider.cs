using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.Data
{
    /// <summary>
    /// 招聘数据服务
    /// </summary>
    public class JobProvider
    {
        /// <summary>
        /// 获取所有未删除的招聘集合
        /// </summary>
        /// <returns></returns>
        public static List<Job> GetAllJobList()
        {
            using (var db = new DataContext())
            {
                var query = from j in db.Job_Recruitment
                            join m in db.Merchant_Account
                            on j.MerchantID equals m.MerchantID
                            where j.IsDelete == false
                            select new Job
                            {
                                AreaID = j.MerchantAreaID > 0 ? j.MerchantAreaID : m.AreaID,
                                AreaLayer = m.AreaLayer,
                                CategoryID = j.CategoryID,
                                Count = j.Count,
                                CreateTime = j.CreateTime,
                                DataType = Enums.IndexDataType.Job,
                                ID = j.RecruitmentID,
                                JobType = j.JobType,
                                Latitude = m.Latitude,
                                Longitude = m.Longitude,
                                MerchantID = m.MerchantID,
                                MaxAge = j.MaxAge,
                                MaxMonthly = j.MaxMonthly,
                                MerchantCertificateStatus = m.CertificateStatus,
                                MerchantName = m.Name,
                                MinAge = j.MinAge,
                                MinEducation = j.MinEducation,
                                MinJobYearsType = j.MinJobYearsType,
                                MinMonthly = j.MinMonthly,
                                Name = j.JobName,
                                Pic = null,
                                Sex = j.Sex,
                                UpdateTime = DateTime.Now,
                                Welfares = j.Welfares,
                                WordAddress = j.WordAddress
                            };

                var list= query.ToList();

                list.ForEach((item) =>
                {
                    var _areaid = item.AreaID.ToString();
                    if (_areaid.Length == 6)
                    {
                        _areaid = _areaid.Remove(4) + "00";
                    }
                    item.AreaID = int.Parse(_areaid);
                });

                return list;
            }
        }

        /// <summary>
        /// 获取招聘所属的区域层级
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public static string GetJobAreaLayer(long jobID)
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Job_Recruitment
                            join m in db.Merchant_Account
                            on p.MerchantID equals m.MerchantID
                            where p.RecruitmentID == jobID
                            select m.AreaLayer;

                return query.FirstOrDefault();
            }
        }

        /// <summary>
        ///  获取招聘信息
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        public static Job GetJob(long jobID)
        {
            using (var db = new DataContext())
            {
                var query = from j in db.Job_Recruitment
                            join m in db.Merchant_Account
                            on j.MerchantID equals m.MerchantID
                            where j.RecruitmentID == jobID
                            select new Job
                            {
                                AreaID = j.MerchantAreaID > 0 ? j.MerchantAreaID : m.AreaID,
                                AreaLayer = m.AreaLayer,
                                CategoryID = j.CategoryID,
                                Count = j.Count,
                                CreateTime = j.CreateTime,
                                DataType = Enums.IndexDataType.Job,
                                ID = j.RecruitmentID,
                                JobType = j.JobType,
                                Latitude = m.Latitude,
                                Longitude = m.Longitude,
                                MerchantID = m.MerchantID,
                                MaxAge = j.MaxAge,
                                MaxMonthly = j.MaxMonthly,
                                MerchantCertificateStatus = m.CertificateStatus,
                                MerchantName = m.Name,
                                MinAge = j.MinAge,
                                MinEducation = j.MinEducation,
                                MinJobYearsType = j.MinJobYearsType,
                                MinMonthly = j.MinMonthly,
                                Name = j.JobName,
                                Pic = null,
                                Sex = j.Sex,
                                UpdateTime = DateTime.Now,
                                Welfares = j.Welfares,
                                WordAddress = j.WordAddress
                            };

                var item = query.FirstOrDefault();

                if (null != item)
                {
                    var _areaid = item.AreaID.ToString();
                    if (_areaid.Length == 6)
                    {
                        _areaid = _areaid.Remove(4) + "00";
                    }
                    item.AreaID = int.Parse(_areaid);
                }

                return item;
            }
        }
    }
}
