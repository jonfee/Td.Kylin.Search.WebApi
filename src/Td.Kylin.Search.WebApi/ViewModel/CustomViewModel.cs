using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.Search.WebApi.IndexModel;

namespace Td.Kylin.Search.WebApi.ViewModel
{
    public class CustomViewModel
    {
        public ViewData<Merchant> Merchant { get; set; }

        public ViewData<Job> Job { get; set; }

        public ViewData<MallProduct> MallProduct { get; set; }

        public ViewData<MerchantProduct> MerchantProduct { get; set; }
    }

    public class ViewData<T> where T : BaseIndexModel
    {
        public int Count { get; set; }

        public List<T> Data { get; set; }
    }
}
