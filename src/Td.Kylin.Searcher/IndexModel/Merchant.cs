using System;

namespace Td.Kylin.Searcher.IndexModel
{
    /// <summary>
    /// 商家
    /// </summary>
    public class Merchant : BaseIndexModel
    {
        ///<summary>
        ///手机号码（唯一）
        ///</summary>
        public string Mobile { get; set; }

        ///<summary>
        ///所属行业ID
        ///</summary>
        public long IndustryID { get; set; }

        /// <summary>
        /// 所属行业名称
        /// </summary>
        public string IndustryName { get; set; }

        ///<summary>
        ///商户位置
        ///</summary>
        public string LocationPlace { get; set; }

        ///<summary>
        ///街道详细地址
        ///</summary>
        public string Street { get; set; }

        ///<summary>
        ///纬度
        ///</summary>
        public Single Latitude { get; set; }

        ///<summary>
        ///经度
        ///</summary>
        public Single Longitude { get; set; }

        ///<summary>
        ///联系人
        ///</summary>
        public string LinkMan { get; set; }

        ///<summary>
        ///商户电话
        ///</summary>
        public string Phone { get; set; }

        ///<summary>
        ///商家认证状态（枚举：法人已认证，营业执照已认证等，以2的N次方定义枚举并累计状态值）
        ///</summary>
        public int CertificateStatus { get; set; }

        ///<summary>
        ///营业开始时间（格式：07:30）
        ///</summary>
        public string BusinessBeginTime { get; set; }
        ///<summary>
        ///营业结束时间（格式：23:00）
        ///</summary>
        public string BusinessEndTime { get; set; }
    }
}
