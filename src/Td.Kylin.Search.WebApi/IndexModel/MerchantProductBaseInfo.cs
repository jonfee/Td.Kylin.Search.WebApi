using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.Search.WebApi.IndexModel
{
    public class MerchantProductBaseInfo
    {
        public long ProductID { get; set; }

        public bool IsDelete { get; set; }

        public string AreaLayer { get; set; }
    }
}
