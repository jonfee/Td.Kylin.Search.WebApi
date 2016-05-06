using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache;
using Td.Kylin.DataCache.CacheModel;

namespace Td.Kylin.Search.WebApi.Core
{
    public class AreaHelper
    {
        /// <summary>
        /// 从区域分析所属的开通区域ID
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public static int GetOpenAreaID(int areaID)
        {
            var area = CacheCollection.SystemAreaCache.Get(areaID);

            int openAreaID = 0;

            if (null != area)
            {
                openAreaID = GetOpenAreaID(area.Layer, null);
            }

            return openAreaID;
        }

        /// <summary>
        /// 从区域分析所属的开通区域ID
        /// </summary>
        /// <param name="arealayer"></param>
        /// <param name="openAreaList"></param>
        /// <returns></returns>
        public static int GetOpenAreaID(string arealayer, List<OpenAreaCacheModel> openAreaList = null)
        {
            int areaID = 0;

            if (null == openAreaList) openAreaList = CacheCollection.OpenAreaCache.Value();

            if (null != openAreaList)
            {
                var openAreas = openAreaList.Select(p => p.AreaID).ToList();

                foreach (var area in openAreas)
                {
                    if (arealayer.Contains(area.ToString()))
                    {
                        areaID = area;
                        break;
                    }
                }
            }

            return areaID;
        }
    }
}
