using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.Search.WebApi.IndexModel
{
    public class MallProductBaseInfo
    {
        public long ProductID { get; set; }

        public bool IsDelete { get; set; }

        public int AreaID { get; set; }
    }
}
