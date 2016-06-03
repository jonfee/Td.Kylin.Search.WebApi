using System;
using Td.Kylin.Searcher.IndexModel;

namespace Td.Kylin.Search.WebApi.IndexModel
{
    /// <summary>
    /// 职位
    /// </summary>
    public class Job : BaseIndexModel
    {
        /// <summary>
        /// 所属商家ID
        /// </summary>
        public long MerchantID { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string MerchantName { get; set; }

        ///<summary>
        ///商家认证状态（枚举：法人已认证，营业执照已认证等，以2的N次方定义枚举并累计状态值）
        ///</summary>
        public int MerchantCertificateStatus { get; set; }

        /// <summary>
        /// 职位类别ID
        /// </summary>
        public long CategoryID { get; set; }

        /// <summary>
        /// 招聘人数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 最少提供月薪
        /// </summary>
        public int MinMonthly { get; set; }

        /// <summary>
        /// 最大提供月薪
        /// </summary>
        public int MaxMonthly { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 最小年龄限制
        /// </summary>
        public int MinAge { get; set; }

        /// <summary>
        /// 最大年龄限制
        /// </summary>
        public int MaxAge { get; set; }

        /// <summary>
        /// 学历要求
        /// </summary>
        public int MinEducation { get; set; }

        /// <summary>
        /// 工作经验要求
        /// </summary>
        public int MinJobYearsType { get; set; }

        /// <summary>
        /// 工作性质（全职|兼职）
        /// </summary>
        public int JobType { get; set; }

        /// <summary>
        /// 福利待遇（枚举：五险一金|房补等，以2的N次方定义枚举）
        /// </summary>
        public int Welfares { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        public string WordAddress { get; set; }

        ///<summary>
        ///纬度
        ///</summary>
        public Single Latitude { get; set; }

        ///<summary>
        ///经度
        ///</summary>
        public Single Longitude { get; set; }
    }
}
