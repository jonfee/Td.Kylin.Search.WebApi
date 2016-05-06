using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.Data
{
    /// <summary>
    /// 商家数据服务
    /// </summary>
    public class MerchantProvider
    {
        /// <summary>
        /// 获取所有商家ID
        /// </summary>
        /// <returns></returns>
        public static List<long> GetAllMerchantIds()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.Merchant_Account
                            select p.MerchantID;

                return query.ToList();
            }
        }

        /// <summary>
        /// 获取商家所在区域的层级
        /// </summary>
        /// <param name="merchantID"></param>
        /// <returns></returns>
        public static string GetAreaLayer(long merchantID)
        {
            using (var db = new DataContext())
            {
                var quer = from p in db.Merchant_Account
                           select p.AreaLayer;

                return quer.FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取商家信息
        /// </summary>
        /// <param name="merchantID"></param>
        /// <returns></returns>
        public static Merchant GetMerchant(long merchantID)
        {
            using (var db = new DataContext())
            {
                var query = from m in db.Merchant_Account
                            join c in db.Merchant_Config
                            on m.MerchantID equals c.MerchantID
                            where m.MerchantID == merchantID
                            select new Merchant
                            {
                                AreaID = m.AreaID,
                                AreaLayer = m.AreaLayer,
                                BusinessBeginTime = c.BusinessBeginTime,
                                BusinessEndTime = c.BusinessEndTime,
                                CertificateStatus = m.CertificateStatus,
                                CreateTime = m.CreateTime,
                                DataType = Enums.IndexDataType.Merchant,
                                ID = m.MerchantID,
                                IndustryID = m.IndustryID,
                                IndustryName = null,
                                Latitude = m.Latitude,
                                LinkMan = m.LinkMan,
                                LocationPlace = m.LocationPlace,
                                Longitude = m.Longitude,
                                Mobile = m.Mobile,
                                Name = m.Name,
                                Phone = m.Phone,
                                Pic = m.Pics,
                                Street = m.Street,
                                UpdateTime = DateTime.Now
                            };

                var item= query.FirstOrDefault();

                if (null != item)
                {
                    //图片
                    var pic = (item.Pic ?? string.Empty).Split(new[] { ',', '|' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                    item.Pic = pic;
                    //行业名称
                    var industry = CacheCollection.MerchantIndustryCache.Get(item.IndustryID);
                    if (null != industry)
                    {
                        item.IndustryName = industry.Name;
                    }
                }

                return item;
            }
        }
    }
}
